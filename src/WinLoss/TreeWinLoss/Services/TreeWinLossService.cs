using Fanex.Data.Repository;
using TreeWinLoss.Criteria;


namespace TreeWinLoss.Services
{
    using AutoMapper;
    using TreeWinLoss.Entities;
    using TreeWinLoss.Model;
    using Utilities.LinqDynamicSum;

    public interface TreeWinLossService
    {
        Task<IEnumerable<TreeWinLossProductModel>> GetProduct();

        Task<IEnumerable<SportModel>> GetSport();

        Task<TreeWinLossModel> GetTreeWinLossByProduct(TreeWinLossByProductCriteria criteria);
    }

    public class TreeWinLossServiceImp : TreeWinLossService
    {
        private readonly IDynamicRepository repository;

        private readonly IMapper mapper;

        public TreeWinLossServiceImp(IDynamicRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
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


        //public TreeWinLoss GetTreeWinLossByLevel(GetTreeWinLossByLevelCriteria criteria)
        //{
        //    criteria.Products = criteria.ProductSelection;

        //    var winLossByComm = GetTreeWinLossDateRangeByLevel(criteria);
        //    winLossByComm.IsShowNotAvailableTurnover = false;


        //    return winLossByComm;
        //}

        //public VirtualRateInfo GetVirtualRate(int custId)
        //{
        //    VirtualRateInfo virtualRateInfo = new VirtualRateInfo();
        //    VirtualRateInfo virtualRateItem = new VirtualRateInfo
        //    {
        //        Date = "Current"
        //    };
        //    GetVirtualRateCriteria getVirtualRateCriteria = new GetVirtualRateCriteria
        //    {
        //        CustId = custId
        //    };
        //    GetVirtualRateHistoryCriteria criteria = new GetVirtualRateHistoryCriteria
        //    {
        //        CustId = custId
        //    };

        //    var virtualRateBySportAndLiveStatus = GetVirtualRateBySportAndLiveStatus(getVirtualRateCriteria);
        //    virtualRateItem.ReportRows = virtualRateBySportAndLiveStatus.ReportRows;
        //    virtualRateItem.CalculateNumberMergeRows();
        //    virtualRateInfo.ReportRows.Add(virtualRateItem);

        //    var virtualRateHistoryLog = GetVirtualRateHistory(criteria);
        //    foreach (var historyLogItem in virtualRateHistoryLog.ReportRows)
        //    {
        //        historyLogItem.CalculateNumberMergeRows();
        //        virtualRateInfo.ReportRows.Add(historyLogItem);
        //    }

        //    return virtualRateInfo;
        //}

        //public TreeWinLossVirtualRate GetAllVirtualRate(TreeWinLoss report)
        //{
        //    var custIdEnableVirtualRate = string.Join(",", report.ChildsInfo.Where(x => x.VirtualRate != "N/A" || x.VirtualRate != "Disabled").Select(x => x.Id));
        //    var getAllVirtualRateCriteria = new GetAllVirtualRateCriteria
        //    {
        //        CustIDs = custIdEnableVirtualRate
        //    };

        //    var allVirtualRate = GetAllVirtualRate(getAllVirtualRateCriteria);

        //    var treeWinLossVirtualRate = new TreeWinLossVirtualRate();
        //    treeWinLossVirtualRate.SMAVirtualRate = allVirtualRate;
        //    treeWinLossVirtualRate.NumberUserEnabledVirtualRate = allVirtualRate.ReportRows.Count;
        //    treeWinLossVirtualRate.EnabledVirtualRateUser = MapEnabledVirtualRateUserData(report.ChildsInfo.Where(x => x.VirtualRate != "N/A" || x.VirtualRate != "Disabled").ToList());

        //    return treeWinLossVirtualRate;
        //}

        private List<TreeWinLossItemModel> CalculateAllTotal(
            IEnumerable<WinLossRawItem> rawData,
            TreeWinLossByProductCriteria criteria)
        {
            var allTotal = new List<TreeWinLossItemModel>();

            var rawTotal = rawData.DynamicSum();

            var total = mapper.Map<TreeWinLossItemModel>(rawTotal);
            total.Name = string.Empty;

            var grandTotal = mapper.Map<TreeWinLossItemModel>(rawTotal);
            grandTotal.Name = "Total";

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


        //private TreeWinLoss GetTreeWinLossDateRangeByLevel(GetTreeWinLossByLevelCriteria criteria)
        //{
        //    switch (criteria.RoleId)
        //    {
        //        case 5:
        //            return GetTreeWinlossData(
        //                new GetTreeWinLossByComCriteria
        //                {
        //                    Currency = criteria.Currency,
        //                    FromDate = criteria.FromDate,
        //                    ToDate = criteria.ToDate,
        //                    ProductSelection = criteria.ProductSelection,
        //                    Products = criteria.Products,
        //                    IsNormalMode = criteria.IsNormalMode,
        //                    SportType = criteria.SportType
        //                },
        //            criteria.RoleId);

        //        case 4:
        //            return GetTreeWinlossData(
        //                new GetTreeWinLossBySuperCriteria
        //                {
        //                    SRecommend = criteria.SRecommend,
        //                    FromDate = criteria.FromDate,
        //                    ToDate = criteria.ToDate,
        //                    ProductSelection = criteria.ProductSelection,
        //                    Products = criteria.Products,
        //                    IsNormalMode = criteria.IsNormalMode,
        //                    SportType = criteria.SportType
        //                },
        //            criteria.RoleId);

        //        case 3:
        //            return GetTreeWinlossData(
        //                new GetTreeWinLossByMasterCriteria
        //                {
        //                    MRecommend = criteria.MRecommend,
        //                    FromDate = criteria.FromDate,
        //                    ToDate = criteria.ToDate,
        //                    ProductSelection = criteria.ProductSelection,
        //                    Products = criteria.Products,
        //                    IsNormalMode = criteria.IsNormalMode,
        //                    SportType = criteria.SportType
        //                },
        //            criteria.RoleId);

        //        case 2:
        //            return GetTreeWinlossData(
        //                new GetTreeWinLossByAgentCriteria
        //                {
        //                    Recommend = criteria.Recommend,
        //                    FromDate = criteria.FromDate,
        //                    ToDate = criteria.ToDate,
        //                    ProductSelection = criteria.ProductSelection,
        //                    Products = criteria.Products,
        //                    IsNormalMode = criteria.IsNormalMode,
        //                    SportType = criteria.SportType
        //                },
        //            criteria.RoleId);
        //    }

        //    return new TreeWinLoss();
        //}

        //private TreeWinLoss GetTreeWinlossData(CriteriaBase criteria, int roleId)
        //{
        //    var treeWinLoss = new TreeWinLoss
        //    {
        //        ChildsInfo = new List<TreeWinLossInfo>(),
        //        TotalInfo = new List<TreeWinLossInfo>()
        //    };

        //    using (var objectDb = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
        //    {
        //        IDataReader reader = objectDb.ExecuteReader(criteria);
        //        var totalWinLoss = new TreeWinLossInfo
        //        {
        //            Name = Labels.Total
        //        };

        //        while (reader.Read())
        //        {
        //            var winLoss = MapTreeWinLossData(reader, roleId);
        //            winLoss = TreeWinLossUtil.RoundTreeWinLossInfo(winLoss);
        //            treeWinLoss.ChildsInfo.Add(winLoss);
        //            TreeWinLossUtil.AddToTotal(winLoss, ref totalWinLoss);
        //        }

        //        treeWinLoss.TotalInfo.Add(totalWinLoss);
        //        reader.Close();
        //    }

        //    return treeWinLoss;
        //}


        //private VirtualRateInfo GetVirtualRateBySportAndLiveStatus(GetVirtualRateCriteria criteria)
        //{
        //    VirtualRateInfo virtualRateBySportAndLiveStatus = new VirtualRateInfo();
        //    bool enableVirtualRate = false;
        //    using (var objectDb = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
        //    {
        //        IDataReader reader = objectDb.ExecuteReader(criteria);
        //        while (reader.Read())
        //        {
        //            enableVirtualRate = Converter.ToDecimal(reader["EnableVirtualrate"]) == 1;
        //        }

        //        if (enableVirtualRate)
        //        {
        //            if (reader.NextResult())
        //            {
        //                VirtualRateInfo virtualRateItem = null;
        //                string preLiveAndSportType = string.Empty;
        //                virtualRateBySportAndLiveStatus.EnableVirtualRate = true;
        //                while (reader.Read())
        //                {
        //                    int sportId = Converter.ToInt(reader["sporttype"]);
        //                    bool isLive = Converter.ToBool(reader["isLive"]);
        //                    int bettypeId = Converter.ToInt(reader["bettype"]);
        //                    decimal virtualRate = Converter.ToDecimal(reader["virtualMultiplier"]);
        //                    string liveAndSportType = string.Join(
        //                        string.Empty,
        //                        Converter.ToString2(reader["sporttype"]),
        //                        Converter.ToString2(reader["isLive"]));

        //                    if (liveAndSportType != preLiveAndSportType)
        //                    {
        //                        if (virtualRateItem != null)
        //                        {
        //                            virtualRateBySportAndLiveStatus.ReportRows.Add(virtualRateItem);
        //                        }

        //                        string liveStatusTitle = isLive ? Labels.Live : Labels.Non_Live;
        //                        virtualRateItem = new VirtualRateInfo
        //                        {
        //                            Sport = GetSportName(sportId),
        //                            LiveStatus = liveStatusTitle
        //                        };
        //                        preLiveAndSportType = liveAndSportType;
        //                    }

        //                    virtualRateItem.VirtualRateByBettype.Add(new VirtualRateByBettypeInfo
        //                    {
        //                        BettypeName = CacheRef.GetBettype(bettypeId),
        //                        VirtualRate = virtualRate - 1
        //                    });
        //                }

        //                if (virtualRateItem != null)
        //                {
        //                    virtualRateBySportAndLiveStatus.ReportRows.Add(virtualRateItem);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            virtualRateBySportAndLiveStatus.ReportRows.Add(new VirtualRateInfo
        //            {
        //                Sport = "Virtual Rate: Disabled",
        //                EnableVirtualRate = false
        //            });
        //        }

        //        reader.Close();
        //    }

        //    return virtualRateBySportAndLiveStatus;
        //}

        //private VirtualRateInfo GetVirtualRateHistory(GetVirtualRateHistoryCriteria criteria)
        //{
        //    VirtualRateInfo virtualRateInfo = new VirtualRateInfo();

        //    using (var objectDb = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
        //    {
        //        IDataReader reader = objectDb.ExecuteReader(criteria);
        //        while (reader.Read())
        //        {
        //            int actionId = Converter.ToInt(reader["ActionID"]);
        //            string trackingValue = Converter.ToString2(reader["TrackingValue"]);
        //            XElement xmlTrackingValue = XElement.Parse(trackingValue);
        //            var virturalRateHistoryItem = GetVirtualRateLogHistory(actionId, xmlTrackingValue);
        //            if (virturalRateHistoryItem.ReportRows.Count != 0)
        //            {
        //                virturalRateHistoryItem.ChangedBy = Converter.ToString2(reader["Username"]);
        //                virturalRateHistoryItem.Date = Converter.ToDateTime(reader["logdate"]).ToLongFormatedDateWithSlash();
        //                virtualRateInfo.ReportRows.Add(virturalRateHistoryItem);
        //            }
        //        }

        //        reader.Close();
        //    }

        //    return virtualRateInfo;
        //}

        //private void InitBettype()
        //{
        //    this._bettype.Add(0, BetTypeConstants.Handicap);
        //    this._bettype.Add(1, BetTypeConstants.OverUnder2);
        //    this._bettype.Add(2, BetTypeConstants.OddEvenBetType);
        //    this._bettype.Add(3, BetTypeConstants._1stHandicap);
        //    this._bettype.Add(4, BetTypeConstants._1stOverUnder);
        //    this._bettype.Add(5, BetTypeConstants._1stOddEvenBetType);
        //    this._bettype.Add(6, BetTypeConstants.MoneyLineBettype);
        //    this._bettype.Add(7, BetTypeConstants._1stMoneyLineBettype);
        //    this._bettype.Add(8, BetTypeConstants.SetXGameYWinner);
        //}

        //private string GetSportName(int sportId)
        //{
        //    string sportName = sportId == 99 ? "Other Sports" : CacheRef.GetSportName(sportId);
        //    return sportName;
        //}

        //private List<VirtualRateByBettypeInfo> InitVirtualRateByBettypeInfo()
        //{
        //    var virtualRateByBettype = new List<VirtualRateByBettypeInfo>(this._bettype.Count);
        //    for (int i = 0; i < _bettype.Count; i++)
        //    {
        //        var virtualRateByBettypeItem = new VirtualRateByBettypeInfo
        //        {
        //            BettypeName = CacheRef.GetBettype(_bettype[i])
        //        };

        //        virtualRateByBettype.Add(virtualRateByBettypeItem);
        //    }

        //    return virtualRateByBettype;
        //}


        //private VirtualRateInfo GetVirtualRateLogHistory(int actionId, XElement xmlTrackingValue)
        //{
        //    string preLiveAndSportType = string.Empty;
        //    VirtualRateInfo virtualRateHistory = new VirtualRateInfo();
        //    VirtualRateInfo virtualRateUpdatedItem = null;
        //    if (actionId == 806 || actionId == 2802)
        //    {
        //        bool enabledVirtualRate = Converter.ToInt(xmlTrackingValue.Attribute("EnableVirtualrate").Value) == 1;
        //        virtualRateUpdatedItem = new VirtualRateInfo
        //        {
        //            Sport = string.Format("Virtual Rate: {0}", enabledVirtualRate ? "Enabled" : "Disabled")
        //        };
        //    }
        //    else
        //    {
        //        foreach (XElement element in xmlTrackingValue.Nodes())
        //        {
        //            int sportId = Converter.ToInt(element.Attribute("SportType").Value);
        //            int bettypeId = Converter.ToInt(element.Attribute("BetType").Value);
        //            bool isLive = Converter.ToInt(element.Attribute("IsLive").Value) == 1;
        //            decimal virtualRate = Converter.ToDecimal(element.Attribute("VirtualMultiplier").Value);
        //            string liveAndSportType = string.Join(string.Empty, sportId, isLive);

        //            if (liveAndSportType != preLiveAndSportType)
        //            {
        //                if (virtualRateUpdatedItem != null)
        //                {
        //                    virtualRateHistory.ReportRows.Add(virtualRateUpdatedItem);
        //                }

        //                string liveStatusTitle = isLive ? Labels.Live : Labels.Non_Live;
        //                virtualRateUpdatedItem = new VirtualRateInfo
        //                {
        //                    Sport = GetSportName(sportId),
        //                    LiveStatus = liveStatusTitle
        //                };
        //                preLiveAndSportType = liveAndSportType;
        //            }

        //            virtualRateUpdatedItem.VirtualRateByBettype.Add(new VirtualRateByBettypeInfo
        //            {
        //                BettypeName = CacheRef.GetBettype(bettypeId),
        //                VirtualRate = virtualRate - 1
        //            });
        //        }
        //    }

        //    if (virtualRateUpdatedItem != null)
        //    {
        //        virtualRateHistory.ReportRows.Add(virtualRateUpdatedItem);
        //    }

        //    return virtualRateHistory;
        //}


        //private SMAVirtualRate MapSMAVirtualRateData(IDataReader reader)
        //{
        //    SMAVirtualRate report = new SMAVirtualRate();
        //    string preUserName = string.Empty;
        //    string preSportAndLiveStatus = string.Empty;
        //    SMAVirtualRate virtualRateByUser = null;
        //    SMAVirtualRate virtualRateBySportAndLiveStatus = null;
        //    while (reader.Read())
        //    {
        //        string userName = Converter.ToString2(reader["username"]);
        //        int sportId = Converter.ToInt(reader["sporttype"]);
        //        int bettypeId = Converter.ToInt(reader["bettype"]);
        //        bool isLive = Converter.ToBool(reader["islive"]);
        //        decimal virtualRate = Converter.ToDecimal(reader["virtualMultiplier"]);
        //        string sportAndLiveStatus = string.Join(string.Empty, sportId, isLive);
        //        string liveStatusTitle = isLive ? "Live" : "None-Live";
        //        int bettypeIndex = _bettype.FirstOrDefault(x => x.Value == bettypeId).Key;

        //        if (userName != preUserName)
        //        {
        //            if (virtualRateBySportAndLiveStatus != null)
        //            {
        //                virtualRateByUser.ReportRows.Add(virtualRateBySportAndLiveStatus);
        //                virtualRateBySportAndLiveStatus = null;
        //            }

        //            if (virtualRateByUser != null)
        //            {
        //                report.ReportRows.Add(virtualRateByUser);
        //            }

        //            virtualRateByUser = new SMAVirtualRate
        //            {
        //                UserName = userName
        //            };

        //            preUserName = userName;
        //        }

        //        if (sportAndLiveStatus != preSportAndLiveStatus)
        //        {
        //            if (virtualRateBySportAndLiveStatus != null)
        //            {
        //                virtualRateByUser.ReportRows.Add(virtualRateBySportAndLiveStatus);
        //            }

        //            virtualRateBySportAndLiveStatus = new SMAVirtualRate
        //            {
        //                Sport = string.Format("{0} ({1})", GetSportName(sportId), liveStatusTitle),
        //                VirtualRateByBettypeInfo = InitVirtualRateByBettypeInfo()
        //            };
        //            preSportAndLiveStatus = sportAndLiveStatus;
        //        }

        //        virtualRateBySportAndLiveStatus.VirtualRateByBettypeInfo[bettypeIndex].VirtualRate = virtualRate - 1;
        //    }

        //    if (virtualRateBySportAndLiveStatus != null)
        //    {
        //        virtualRateByUser.ReportRows.Add(virtualRateBySportAndLiveStatus);
        //    }

        //    if (virtualRateByUser != null)
        //    {
        //        report.ReportRows.Add(virtualRateByUser);
        //    }

        //    return report;
        //}

        //private TreeWinLossInfo MapTreeWinLossData(IDataReader reader, int roleId)
        //{
        //    string recommend = string.Empty;
        //    switch (roleId)
        //    {
        //        case 2:
        //            recommend = "custid";
        //            break;

        //        case 3:
        //            recommend = "recommend";
        //            break;

        //        case 4:
        //            recommend = "mrecommend";
        //            break;

        //        case 5:
        //            recommend = "srecommend";
        //            break;

        //        default:
        //            break;
        //    }

        //    var winLoss = new TreeWinLossInfo()
        //    {
        //        Id = Converter.ToInt(reader[recommend]),
        //        Name = Converter.ToString2(reader["username"]),
        //        Turnover = Converter.ToDecimal(reader["turnover"]),
        //        WinLoss = Converter.ToDecimal(reader["winloss"]),
        //        Comm = Converter.ToDecimal(reader["commission"]),
        //        WinLossTotal = Converter.ToDecimal(reader["total"]),
        //        BaseRMTurnover = Converter.ToDecimal(reader["turnoverRM"]),
        //        BaseRMVirtualTurnover = Converter.ToDecimal(reader["Virtual_Turnover"]),
        //        BaseRMTotal = Converter.ToDecimal(reader["totalRM"]),
        //        BaseRMVirtualTotal = Converter.ToDecimal(reader["virtual_total"]),
        //        BaseRMTOVariancePlus = Converter.ToDecimal(reader["TOVariance+"]),
        //        BaseRMTOVarianceMinus = Converter.ToDecimal(reader["TOVariance-"]),
        //        BaseRMWLVarianceMinus = Converter.ToDecimal(reader["WLVariance-"]),
        //        BaseRMWLVariancePlus = Converter.ToDecimal(reader["WLVariance+"]),
        //        VirtualRate = Converter.ToString2(reader["VirtualRate"]),
        //        ExtraAmount = Converter.ToDecimal(reader["ExtraAmount"]),
        //        BaseRMWinlostVarianceTotal = Converter.ToDecimal(reader["WLVarianceTotal"]),
        //        BaseRMTOVarianceAbsoluteTotal = Converter.ToDecimal(reader["TOVarianceTotal"])
        //    };

        //    return winLoss;
        //}

        //private Dictionary<string, int> MapEnabledVirtualRateUserData(List<TreeWinLossInfo> enabledVirtualRateUser)
        //{
        //    int index = 0;
        //    Dictionary<string, int> enabledVirtualRateUserDictionary = new Dictionary<string, int>();
        //    foreach (var user in enabledVirtualRateUser)
        //    {
        //        enabledVirtualRateUserDictionary.Add(user.Name, index);
        //        index++;
        //    }

        //    return enabledVirtualRateUserDictionary;
        //}

        //private SMAVirtualRate GetAllVirtualRate(GetAllVirtualRateCriteria criteria)
        //{
        //    SMAVirtualRate virtualRate = new SMAVirtualRate();

        //    using (var objectDb = ObjectDbFactory.CreateInstance(criteria.GetSettingKey()))
        //    {
        //        IDataReader reader = objectDb.ExecuteReader(criteria);

        //        while (reader.Read())
        //        {
        //            virtualRate = MapSMAVirtualRateData(reader);
        //        }

        //        reader.Close();
        //    }

        //    return virtualRate;
        //}
    }
}
