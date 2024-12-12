using AutoMapper;
using Fanex.Data.Repository;
using Utilities.DateTimes;
using System.Xml.Linq;
using TreeWinLoss.Constants;
using TreeWinLoss.Criteria;
using TreeWinLoss.Entities;
using TreeWinLoss.Model;
using Utilities.LinqDynamicSum;
using Utilities.RemotingRef;


namespace TreeWinLoss.Services
{
    public interface TreeWinLossService
    {
        Task<IEnumerable<TreeWinLossProductModel>> GetProduct();

        Task<IEnumerable<SportModel>> GetSport();

        Task<TreeWinLossModel> GetTreeWinLossByProduct(TreeWinLossByProductCriteria criteria);

        Task<TreeWinLossModel> GetTreeWinLossByLevel(TreeWinLossByLevelCriteria criteria);

        Task<VirtualRateModel> GetVirtualRate(int custId);

        Task<TreeWinLossVirtualRateModel> GetAllVirtualRate(TreeWinLossModel report);
    }

    public class TreeWinLossServiceImp : TreeWinLossService
    {
        private readonly IDynamicRepository repository;

        private readonly IMapper mapper;

        private readonly CriteriaFactory criteriaFactory;

        private readonly RemotingRefService remotingRefService;

        private readonly Dictionary<int, int> bettypes = new();

        private const int MaxCustIds = 5000;

        public TreeWinLossServiceImp(
            IDynamicRepository repository,
            IMapper mapper,
            CriteriaFactory criteriaFactory,
            RemotingRefService remotingRefService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.criteriaFactory = criteriaFactory;
            this.remotingRefService = remotingRefService;
            InitBettype();
        }

        public async Task<IEnumerable<TreeWinLossProductModel>> GetProduct()
        {
            var criteria = new TreeWinLossListProductCriteria();
            var products = await repository.FetchAsync<TreeWinLossProductModel>(criteria);

            return products;
        }

        public async Task<IEnumerable<SportModel>> GetSport()
        {
            var criteria = new SportCriteria();
            var sports = await repository.FetchAsync<SportModel>(criteria);

            return sports;
        }

        public async Task<TreeWinLossModel> GetTreeWinLossByProduct(TreeWinLossByProductCriteria criteria)
        {
            criteria.Products = criteria.ProductSelection;

            var treeWinLoss = new TreeWinLossModel
            {
                WinLosses = [],
                Total = [],
                IsShowNotAvailableTurnover = false
            };

            var rawData = await repository.FetchAsync<WinLossRawItem>(criteria);

            if (rawData.Any())
            {
                var winLosses = mapper.Map<List<TreeWinLossItemModel>>(rawData);

                var allTotal = CalculateAllTotal(rawData, criteria);

                treeWinLoss.WinLosses = winLosses;
                treeWinLoss.Total = allTotal;
            }

            return treeWinLoss;
        }

        public async Task<TreeWinLossModel> GetTreeWinLossByLevel(TreeWinLossByLevelCriteria criteria)
        {
            criteria.Products = criteria.ProductSelection;

            var treeWinLossByLevelCriteria = criteriaFactory.CreateCriteria(criteria);

            var result = await GetTreeWinlossByLevelData(treeWinLossByLevelCriteria);
            result.IsShowNotAvailableTurnover = false;

            return result;
        }

        public async Task<VirtualRateModel> GetVirtualRate(int custId)
        {
            var virtualRateInfo = new VirtualRateModel();
            var virtualRateItem = new VirtualRateModel
            {
                Date = "Current"
            };
            var getVirtualRateCriteria = new VirtualRateCriteria
            {
                CustId = custId
            };
            var criteria = new VirtualRateHistoryCriteria
            {
                CustId = custId
            };

            var virtualRateBySportAndLiveStatus = await GetVirtualRateBySportAndLiveStatus(getVirtualRateCriteria);
            virtualRateItem.ReportRows = virtualRateBySportAndLiveStatus.ReportRows;
            virtualRateItem.CalculateNumberMergeRows();
            virtualRateInfo.ReportRows.Add(virtualRateItem);

            var virtualRateHistoryLog = await GetVirtualRateHistory(criteria);
            foreach (var historyLogItem in virtualRateHistoryLog.ReportRows)
            {
                historyLogItem.CalculateNumberMergeRows();
                virtualRateInfo.ReportRows.Add(historyLogItem);
            }

            return virtualRateInfo;
        }

        public async Task<TreeWinLossVirtualRateModel> GetAllVirtualRate(TreeWinLossModel report)
        {
            var custIdEnableVirtualRate = string.Join(",", report.WinLosses.Where(x => x.VirtualRate != "N/A" || x.VirtualRate != "Disabled").Select(x => x.Id));
            var getAllVirtualRateCriteria = new AllVirtualRateCriteria
            {
                CustIDs = custIdEnableVirtualRate
            };

            var allVirtualRate = await GetAllVirtualRate(getAllVirtualRateCriteria);

            var treeWinLossVirtualRate = new TreeWinLossVirtualRateModel
            {
                SMAVirtualRate = allVirtualRate,
                NumberUserEnabledVirtualRate = allVirtualRate.ReportRows.Count,
                EnabledVirtualRateUser = MapEnabledVirtualRateUserData(report.WinLosses.Where(x => x.VirtualRate != "N/A" || x.VirtualRate != "Disabled").ToList())
            };

            return treeWinLossVirtualRate;
        }

        private List<TreeWinLossItemModel> CalculateAllTotal(
            IEnumerable<WinLossRawItem> rawData,
            TreeWinLossByProductCriteria criteria)
        {
            var allTotal = new List<TreeWinLossItemModel>();

            var winLossData = mapper.Map<List<TreeWinLossItemModel>>(rawData);

            var total = winLossData.DynamicSum();
            total.Name = string.Empty;

            var tempTotal = winLossData.DynamicSum();
            var grandTotal = mapper.Map<TreeWinLossItemModel>(tempTotal);

            var rmTotal = new TreeWinLossItemModel
            {
                Name = "8RM",
                BaseRMTotal = criteria.RMTotal,
                BaseRMVirtualTotal = criteria.RMVirtualTotal,
                BaseRMTurnover = criteria.RMTurnover,
                BaseRMVirtualTurnover = criteria.RMTurnover
            };

            var barmTotal = new TreeWinLossItemModel
            {
                Name = "BARM",
                BaseRMTotal = criteria.BARMTotal,
                BaseRMVirtualTotal = criteria.BARMVirtualTotal,
                BaseRMTurnover = criteria.BARMTurnover,
                BaseRMVirtualTurnover = criteria.BARMTurnover
            };

            grandTotal.Name = "Total";
            grandTotal.BaseRMTotal = grandTotal.BaseRMTotal + rmTotal.BaseRMTotal + barmTotal.BaseRMTotal;

            allTotal.Add(total);
            allTotal.Add(rmTotal);
            allTotal.Add(barmTotal);
            allTotal.Add(grandTotal);

            return allTotal;
        }

        private async Task<TreeWinLossModel> GetTreeWinlossByLevelData(CriteriaBase criteria)
        {
            var treeWinLoss = new TreeWinLossModel
            {
                WinLosses = [],
                Total = []
            };

            var rawData = await repository.FetchAsync<WinLossRawItem>(criteria);

            if (rawData != null && rawData.Any())
            {
                var winlosses = mapper.Map<List<TreeWinLossItemModel>>(rawData);

                var total = winlosses.DynamicSum();

                treeWinLoss.WinLosses = winlosses;
                treeWinLoss.Total.Add(total);
            }

            return treeWinLoss;
        }


        private async Task<VirtualRateModel> GetVirtualRateBySportAndLiveStatus(VirtualRateCriteria criteria)
        {
            var virtualRateBySportAndLiveStatus = new VirtualRateModel();
            var enableVirtualRate = false;

            var rawData = await repository.FetchMultipleAsync<SummaryVirtualRate, DetailVirtualRate>(criteria);

            var summaryVirtualRate = rawData.Item1;
            var detailVirtualRate = rawData.Item2;

            if (summaryVirtualRate.Any())
            {
                enableVirtualRate = summaryVirtualRate.FirstOrDefault().EnableVirtualRate == 1;
            }

            if (enableVirtualRate)
            {
                if (detailVirtualRate.Any())
                {
                    VirtualRateModel virtualRateItem = null;
                    var preLiveAndSportType = string.Empty;
                    virtualRateBySportAndLiveStatus.EnableVirtualRate = true;
                    foreach (var detailItem in detailVirtualRate)
                    {
                        var sportId = detailItem.SportType;
                        var isLive = detailItem.IsLive;
                        var bettypeId = detailItem.BetType;
                        var virtualRate = detailItem.VirtualMultiplier;
                        var liveAndSportType = string.Join(string.Empty, detailItem.SportType, detailItem.IsLive);

                        if (liveAndSportType != preLiveAndSportType)
                        {
                            if (virtualRateItem != null)
                            {
                                virtualRateBySportAndLiveStatus.ReportRows.Add(virtualRateItem);
                            }

                            var liveStatusTitle = isLive ? "Live" : "Non-Live";
                            virtualRateItem = new VirtualRateModel
                            {
                                Sport = GetSportName(sportId),
                                LiveStatus = liveStatusTitle
                            };

                            preLiveAndSportType = liveAndSportType;
                        }

                        virtualRateItem.VirtualRateByBettype.Add(new VirtualRateByBettypeModel
                        {
                            BettypeName = remotingRefService.GetBetTypeName(bettypeId),
                            VirtualRate = virtualRate - 1
                        });
                    }

                    if (virtualRateItem != null)
                    {
                        virtualRateBySportAndLiveStatus.ReportRows.Add(virtualRateItem);
                    }
                }
            }
            else
            {
                virtualRateBySportAndLiveStatus.ReportRows.Add(new VirtualRateModel
                {
                    Sport = "Virtual Rate: Disabled",
                    EnableVirtualRate = false
                });
            }

            return virtualRateBySportAndLiveStatus;
        }

        private async Task<VirtualRateModel> GetVirtualRateHistory(VirtualRateHistoryCriteria criteria)
        {
            var virtualRateInfo = new VirtualRateModel();

            var rawData = await repository.FetchAsync<HistoryVirtualRate>(criteria);

            foreach (var historyVirtualRateItem in rawData)
            {
                var actionId = historyVirtualRateItem.ActionId;
                var trackingValue = historyVirtualRateItem.TrackingValue;
                var xmlTrackingValue = XElement.Parse(trackingValue);
                var virtualRateHistoryItem = GetVirtualRateLogHistory(actionId, xmlTrackingValue);
                if (virtualRateHistoryItem.ReportRows.Count != 0)
                {
                    virtualRateHistoryItem.ChangedBy = historyVirtualRateItem.UserName;
                    virtualRateHistoryItem.Date = historyVirtualRateItem.LogDate.ToLongFormatedDateWithSlash();
                    virtualRateInfo.ReportRows.Add(virtualRateHistoryItem);
                }
            }


            return virtualRateInfo;
        }

        private void InitBettype()
        {
            bettypes.Add(0, BetTypeConstants.Handicap);
            bettypes.Add(1, BetTypeConstants.OverUnder2);
            bettypes.Add(2, BetTypeConstants.OddEvenBetType);
            bettypes.Add(3, BetTypeConstants.FirstHalfHandicap);
            bettypes.Add(4, BetTypeConstants.FirstHalfOverUnder);
            bettypes.Add(5, BetTypeConstants.FirstHalfOddEvenBetType);
            bettypes.Add(6, BetTypeConstants.MoneyLineBettype);
            bettypes.Add(7, BetTypeConstants.FirstHalfMoneyLineBettype);
            bettypes.Add(8, BetTypeConstants.SetXGameYWinner);
        }

        private string GetSportName(int sportId)
        {
            var sportName = sportId == 99 ? "Other Sports" : remotingRefService.GetSportName(sportId);
            return sportName;
        }

        private List<VirtualRateByBettypeModel> InitVirtualRateByBettypeInfo()
        {
            var virtualRateByBettype = new List<VirtualRateByBettypeModel>(bettypes.Count);
            for (var i = 0; i < bettypes.Count; i++)
            {
                var virtualRateByBettypeItem = new VirtualRateByBettypeModel
                {
                    BettypeName = remotingRefService.GetBetTypeName(bettypes[i])
                };

                virtualRateByBettype.Add(virtualRateByBettypeItem);
            }

            return virtualRateByBettype;
        }

        private VirtualRateModel GetVirtualRateLogHistory(int actionId, XElement xmlTrackingValue)
        {
            var preLiveAndSportType = string.Empty;
            var virtualRateHistory = new VirtualRateModel();
            VirtualRateModel virtualRateUpdatedItem = null;
            if (actionId == 806 || actionId == 2802)
            {
                var enabledVirtualRate = Convert.ToInt32(xmlTrackingValue.Attribute("EnableVirtualrate").Value) == 1;
                virtualRateUpdatedItem = new VirtualRateModel
                {
                    Sport = $"Virtual Rate: {(enabledVirtualRate ? "Enabled" : "Disabled")}"
                };
            }
            else
            {
                foreach (XElement element in xmlTrackingValue.Nodes())
                {
                    var sportId = Convert.ToInt32(element.Attribute("SportType").Value);
                    var bettypeId = Convert.ToInt32(element.Attribute("BetType").Value);
                    var isLive = Convert.ToInt32(element.Attribute("IsLive").Value) == 1;
                    var virtualRate = Convert.ToDecimal(element.Attribute("VirtualMultiplier").Value);
                    var liveAndSportType = string.Join(string.Empty, sportId, isLive);

                    if (liveAndSportType != preLiveAndSportType)
                    {
                        if (virtualRateUpdatedItem != null)
                        {
                            virtualRateHistory.ReportRows.Add(virtualRateUpdatedItem);
                        }

                        var liveStatusTitle = isLive ? "Live" : "Non-Live";
                        virtualRateUpdatedItem = new VirtualRateModel
                        {
                            Sport = GetSportName(sportId),
                            LiveStatus = liveStatusTitle
                        };
                        preLiveAndSportType = liveAndSportType;
                    }

                    virtualRateUpdatedItem.VirtualRateByBettype.Add(new VirtualRateByBettypeModel
                    {
                        BettypeName = remotingRefService.GetBetTypeName(bettypeId),
                        VirtualRate = virtualRate - 1
                    });
                }
            }

            if (virtualRateUpdatedItem != null)
            {
                virtualRateHistory.ReportRows.Add(virtualRateUpdatedItem);
            }

            return virtualRateHistory;
        }

        private SMAVirtualRateModel MapSMAVirtualRateData(IEnumerable<DetailVirtualRate> rawData)
        {
            var report = new SMAVirtualRateModel();
            var preUserName = string.Empty;
            var preSportAndLiveStatus = string.Empty;
            SMAVirtualRateModel virtualRateByUser = null;
            SMAVirtualRateModel virtualRateBySportAndLiveStatus = null;
            foreach (var detailItem in rawData)
            {
                var userName = detailItem.UserName;
                var sportId = detailItem.SportType;
                var bettypeId = detailItem.BetType;
                var isLive = detailItem.IsLive;
                var virtualRate = detailItem.VirtualMultiplier;
                var sportAndLiveStatus = string.Join(string.Empty, sportId, isLive);
                var liveStatusTitle = isLive ? "Live" : "None-Live";
                var bettypeIndex = bettypes.FirstOrDefault(x => x.Value == bettypeId).Key;

                if (userName != preUserName)
                {
                    if (virtualRateBySportAndLiveStatus != null)
                    {
                        virtualRateByUser.ReportRows.Add(virtualRateBySportAndLiveStatus);
                        virtualRateBySportAndLiveStatus = null;
                    }

                    if (virtualRateByUser != null)
                    {
                        report.ReportRows.Add(virtualRateByUser);
                    }

                    virtualRateByUser = new SMAVirtualRateModel
                    {
                        UserName = userName
                    };

                    preUserName = userName;
                }

                if (sportAndLiveStatus != preSportAndLiveStatus)
                {
                    if (virtualRateBySportAndLiveStatus != null)
                    {
                        virtualRateByUser.ReportRows.Add(virtualRateBySportAndLiveStatus);
                    }

                    virtualRateBySportAndLiveStatus = new SMAVirtualRateModel
                    {
                        Sport = $"{GetSportName(sportId)} ({liveStatusTitle})",
                        VirtualRateByBettypeInfo = InitVirtualRateByBettypeInfo()
                    };
                    preSportAndLiveStatus = sportAndLiveStatus;
                }

                virtualRateBySportAndLiveStatus.VirtualRateByBettypeInfo[bettypeIndex].VirtualRate = virtualRate - 1;
            }

            if (virtualRateBySportAndLiveStatus != null)
            {
                virtualRateByUser.ReportRows.Add(virtualRateBySportAndLiveStatus);
            }

            if (virtualRateByUser != null)
            {
                report.ReportRows.Add(virtualRateByUser);
            }

            return report;
        }

        private Dictionary<string, int> MapEnabledVirtualRateUserData(List<TreeWinLossItemModel> enabledVirtualRateUser)
        {
            var index = 0;
            var enabledVirtualRateUserDictionary = new Dictionary<string, int>();
            foreach (var user in enabledVirtualRateUser)
            {
                enabledVirtualRateUserDictionary.Add(user.Name, index);
                index++;
            }

            return enabledVirtualRateUserDictionary;
        }

        private async Task<SMAVirtualRateModel> GetAllVirtualRate(AllVirtualRateCriteria criteria)
        {
            var virtualRate = new SMAVirtualRateModel();
            var custIds = criteria.CustIDs.Split(",");
            var detailVirtualRate = new List<DetailVirtualRate>();
            while (custIds.Length > 0)
            {
                var tempCustIds = custIds.Take(MaxCustIds);
                var request = new AllVirtualRateCriteria
                {
                    CustIDs = string.Join(",", tempCustIds)
                };
                var rawData = await repository.FetchAsync<DetailVirtualRate>(request);
                detailVirtualRate.AddRange(rawData);
                custIds = custIds.Length > MaxCustIds ? custIds.Skip(MaxCustIds).Take(custIds.Length - MaxCustIds).ToArray() : [];
            }

            virtualRate = MapSMAVirtualRateData(detailVirtualRate);

            return virtualRate;
        }
    }
}
