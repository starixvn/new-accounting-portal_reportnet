using AutoMapper;
using Fanex.Data.Repository;
using Nap_Reports.Utilities.DateTimes;
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

        private readonly Dictionary<int, int> bettypes = new Dictionary<int, int>();

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
                WinLosses = new List<TreeWinLossItemModel>(),
                Total = new List<TreeWinLossItemModel>(),
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
            VirtualRateModel virtualRateInfo = new VirtualRateModel();
            VirtualRateModel virtualRateItem = new VirtualRateModel
            {
                Date = "Current"
            };
            VirtualRateCriteria getVirtualRateCriteria = new VirtualRateCriteria
            {
                CustId = custId
            };
            VirtualRateHistoryCriteria criteria = new VirtualRateHistoryCriteria
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

            var treeWinLossVirtualRate = new TreeWinLossVirtualRateModel();
            treeWinLossVirtualRate.SMAVirtualRate = allVirtualRate;
            treeWinLossVirtualRate.NumberUserEnabledVirtualRate = allVirtualRate.ReportRows.Count;
            treeWinLossVirtualRate.EnabledVirtualRateUser = MapEnabledVirtualRateUserData(report.WinLosses.Where(x => x.VirtualRate != "N/A" || x.VirtualRate != "Disabled").ToList());

            return treeWinLossVirtualRate;
        }

        private List<TreeWinLossItemModel> CalculateAllTotal(
            IEnumerable<WinLossRawItem> rawData,
            TreeWinLossByProductCriteria criteria)
        {
            var allTotal = new List<TreeWinLossItemModel>();
            
            var winLossData = mapper.Map<List<TreeWinLossItemModel>>(rawData);
            var tempTotal = winLossData.DynamicSum();

            var total = mapper.Map<TreeWinLossItemModel>(tempTotal);

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
            grandTotal.BaseRMTotal += rmTotal.BaseRMTotal + barmTotal.BaseRMTotal;
            grandTotal.BaseRMVirtualTotal += rmTotal.BaseRMVirtualTotal + barmTotal.BaseRMVirtualTotal;
            grandTotal.BaseRMTurnover += rmTotal.BaseRMTurnover + barmTotal.BaseRMTurnover;
            grandTotal.BaseRMVirtualTurnover += rmTotal.BaseRMVirtualTurnover + barmTotal.BaseRMVirtualTurnover;

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
                WinLosses = new List<TreeWinLossItemModel>(),
                Total = new List<TreeWinLossItemModel>()
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
            VirtualRateModel virtualRateBySportAndLiveStatus = new VirtualRateModel();
            bool enableVirtualRate = false;
            
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
                        string preLiveAndSportType = string.Empty;
                        virtualRateBySportAndLiveStatus.EnableVirtualRate = true;
                    foreach (var detailItem in detailVirtualRate)
                        {
                            int sportId = detailItem.SportType;
                            bool isLive = detailItem.IsLive;
                            int bettypeId = detailItem.BetType;
                            decimal virtualRate = detailItem.VirtualMultiplier;
                            string liveAndSportType = string.Join(string.Empty, detailItem.SportType, detailItem.IsLive);

                            if (liveAndSportType != preLiveAndSportType)
                            {
                                if (virtualRateItem != null)
                                {
                                    virtualRateBySportAndLiveStatus.ReportRows.Add(virtualRateItem);
                                }

                                string liveStatusTitle = isLive ? "Live" : "Non-Live";
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
            VirtualRateModel virtualRateInfo = new VirtualRateModel();

            var rawData = await repository.FetchAsync<HistoryVirtualRate>(criteria);
            
                foreach (var historyVirtualRateItem in rawData)
            {
                int actionId = historyVirtualRateItem.ActionId;
                string trackingValue = historyVirtualRateItem.TrackingValue;
                XElement xmlTrackingValue = XElement.Parse(trackingValue);
                var virturalRateHistoryItem = GetVirtualRateLogHistory(actionId, xmlTrackingValue);
                if (virturalRateHistoryItem.ReportRows.Count != 0)
                {
                    virturalRateHistoryItem.ChangedBy = historyVirtualRateItem.UserName;
                    virturalRateHistoryItem.Date = historyVirtualRateItem.LogDate.ToLongFormatedDateWithSlash();
                    virtualRateInfo.ReportRows.Add(virturalRateHistoryItem);
                }
            }


            return virtualRateInfo;
        }

        private void InitBettype()
        {
            this.bettypes.Add(0, BetTypeConstants.Handicap);
            this.bettypes.Add(1, BetTypeConstants.OverUnder2);
            this.bettypes.Add(2, BetTypeConstants.OddEvenBetType);
            this.bettypes.Add(3, BetTypeConstants.FirstHalfHandicap);
            this.bettypes.Add(4, BetTypeConstants.FirstHalfOverUnder);
            this.bettypes.Add(5, BetTypeConstants.FirstHalfOddEvenBetType);
            this.bettypes.Add(6, BetTypeConstants.MoneyLineBettype);
            this.bettypes.Add(7, BetTypeConstants.FirstHalfMoneyLineBettype);
            this.bettypes.Add(8, BetTypeConstants.SetXWinnerBettype);
        }

        private string GetSportName(int sportId)
        {
            string sportName = sportId == 99 ? "Other Sports" : remotingRefService.GetSportName(sportId);
            return sportName;
        }

        private List<VirtualRateByBettypeModel> InitVirtualRateByBettypeInfo()
        {
            var virtualRateByBettype = new List<VirtualRateByBettypeModel>(this.bettypes.Count);
            for (int i = 0; i < bettypes.Count; i++)
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
            string preLiveAndSportType = string.Empty;
            VirtualRateModel virtualRateHistory = new VirtualRateModel();
            VirtualRateModel virtualRateUpdatedItem = null;
            if (actionId == 806 || actionId == 2802)
            {
                bool enabledVirtualRate = Convert.ToInt32(xmlTrackingValue.Attribute("EnableVirtualrate").Value) == 1;
                virtualRateUpdatedItem = new VirtualRateModel
                {
                    Sport = string.Format("Virtual Rate: {0}", enabledVirtualRate ? "Enabled" : "Disabled")
                };
            }
            else
            {
                foreach (XElement element in xmlTrackingValue.Nodes())
                {
                    int sportId = Convert.ToInt32(element.Attribute("SportType").Value);
                    int bettypeId = Convert.ToInt32(element.Attribute("BetType").Value);
                    bool isLive = Convert.ToInt32(element.Attribute("IsLive").Value) == 1;
                    decimal virtualRate = Convert.ToDecimal(element.Attribute("VirtualMultiplier").Value);
                    string liveAndSportType = string.Join(string.Empty, sportId, isLive);

                    if (liveAndSportType != preLiveAndSportType)
                    {
                        if (virtualRateUpdatedItem != null)
                        {
                            virtualRateHistory.ReportRows.Add(virtualRateUpdatedItem);
                        }

                        string liveStatusTitle = isLive ? "Live" : "Non-Live";
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
            SMAVirtualRateModel report = new SMAVirtualRateModel();
            string preUserName = string.Empty;
            string preSportAndLiveStatus = string.Empty;
            SMAVirtualRateModel virtualRateByUser = null;
            SMAVirtualRateModel virtualRateBySportAndLiveStatus = null;
            foreach (var detailItem in rawData)
            {
                string userName = detailItem.UserName;
                int sportId = detailItem.SportType;
                int bettypeId = detailItem.BetType;
                bool isLive = detailItem.IsLive;
                decimal virtualRate = detailItem.VirtualMultiplier;
                string sportAndLiveStatus = string.Join(string.Empty, sportId, isLive);
                string liveStatusTitle = isLive ? "Live" : "None-Live";
                int bettypeIndex = bettypes.FirstOrDefault(x => x.Value == bettypeId).Key;

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
                        Sport = string.Format("{0} ({1})", GetSportName(sportId), liveStatusTitle),
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
            int index = 0;
            Dictionary<string, int> enabledVirtualRateUserDictionary = new Dictionary<string, int>();
            foreach (var user in enabledVirtualRateUser)
            {
                enabledVirtualRateUserDictionary.Add(user.Name, index);
                index++;
            }

            return enabledVirtualRateUserDictionary;
        }

        private async Task<SMAVirtualRateModel> GetAllVirtualRate(AllVirtualRateCriteria criteria)
        {
            SMAVirtualRateModel virtualRate = new SMAVirtualRateModel();

            var rawData = await repository.FetchAsync<DetailVirtualRate>(criteria);

            virtualRate = MapSMAVirtualRateData(rawData);

            return virtualRate;
        }
    }
}
