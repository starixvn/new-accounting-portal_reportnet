using Fanex.Data.Repository;
using LeagueWinLoss.Entities;
using System.Data;

namespace LeagueWinLoss.Repositories
{
    public interface LeagueWinLossRepository
    {
        Task<List<LeagueWinLossEntity>> GetLeagueWinLoss(ICriteria criteria);

        Task<List<BettypeWinLossEntity>> GetBetTypes(ICriteria criteria);

        Task<List<MatchEntity>> GetMatches(ICriteria criteria);

        Task<List<SiteEntity>> GetSites(ICriteria criteria);

        Task<List<SuperEntity>> GetSupers(ICriteria criteria);

        Task<List<VirtualRateEntity>> GetVirtualRate(ICriteria criteria);

        Task<List<SportEntity>> GetSport(ICriteria criteria);

        Task<List<SiteFilterEntity>> GetSiteFilter(ICriteria criteria);

        Task<int> GetPermissionProfileID(ICriteria criteria);
    }

    public class LeagueWinLossRepositoryImp(IDynamicRepository repository) : LeagueWinLossRepository
    {
        public async Task<List<LeagueWinLossEntity>> GetLeagueWinLoss(ICriteria criteria)
        {
            var leagueWinLoss = new List<LeagueWinLossEntity>();

            var dataSet = await repository.FetchDataSetAsync(criteria);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                var winLossTable = dataSet.Tables[0];
                foreach (DataRow row in winLossTable.Rows)
                {
                    var entity = new LeagueWinLossEntity
                    {
                        SportType = Convert.ToInt32(row["SportType"]),
                        Sport = Convert.ToString(row["Sport"]),
                        LeagueName = Convert.ToString(row["LeagueName"]),
                        MemberWinLoss = Convert.ToDecimal(row["MemberWinLoss"]),
                        SMAWinLoss = Convert.ToDecimal(row["SMAWinLoss"]),
                        ExtraPT = Convert.ToDecimal(row["ExtraPT"]),
                        Commission = Convert.ToDecimal(row["Commission"]),
                        RealIncome = Convert.ToDecimal(row["RealInCome"]),
                        VirtualWinLoss = Convert.ToDecimal(row["VirtualIncome"]),
                        RealTurnover = Convert.ToDecimal(row["RealTurnover"]),
                        VirtualTurnover = Convert.ToDecimal(row["VirtualTurnover"]),
                        IsSabaSport = Convert.ToInt32(row["IsSabaSport"]),
                        LeagueId = Convert.ToString(row["LeagueId"])
                    };


                    entity.MatchCount = row["MatchCount"] == DBNull.Value ? null : Convert.ToInt32(row["MatchCount"]);

                    leagueWinLoss.Add(entity);
                }
            }

            return leagueWinLoss;
        }


        public async Task<List<BettypeWinLossEntity>> GetBetTypes(ICriteria criteria)
        {
            var bettypeWinLoss = new List<BettypeWinLossEntity>();

            var dataSet = await repository.FetchDataSetAsync(criteria);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                var winLossTable = dataSet.Tables[0];

                foreach (DataRow row in winLossTable.Rows)
                {
                    bettypeWinLoss.Add(new BettypeWinLossEntity
                    {
                        Bettype = Convert.ToInt32(row["BetTypeID"]),
                        BettypeName = Convert.ToString(row["BetTypeName"]),
                        SiteType = Convert.ToString(row["SiteType"]),
                        MemberWinLoss = Convert.ToDecimal(row["MemberWinLoss"]),
                        SMAWinLoss = Convert.ToDecimal(row["SMAWinLoss"]),
                        ExtraPT = Convert.ToDecimal(row["ExtraPT"]),
                        Commission = Convert.ToDecimal(row["Commission"]),
                        RealIncome = Convert.ToDecimal(row["RealInCome"]),
                        VirtualWinLoss = Convert.ToDecimal(row["VirtualIncome"]),
                        RealTurnover = Convert.ToDecimal(row["RealTurnover"]),
                        VirtualTurnover = Convert.ToDecimal(row["VirtualTurnover"]),
                        IsHedge = Convert.ToInt32(row["IsHedge"]) == 1,
                        TraderName = Convert.ToString(row["TraderName"])
                    });
                }
            }

            return bettypeWinLoss;
        }

        public async Task<List<MatchEntity>> GetMatches(ICriteria criteria)
        {
            var leagueWinLoss = new List<MatchEntity>();

            var dataSet = await repository.FetchDataSetAsync(criteria);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                var winLossTable = dataSet.Tables[0];

                foreach (DataRow row in winLossTable.Rows)
                {
                    leagueWinLoss.Add(new MatchEntity
                    {
                        MatchID = Convert.ToInt32(row["MatchID"]),
                        MatchCode = Convert.ToString(row["MatchCode"]),
                        LiveIndicator = Convert.ToInt32(row["LiveIndicator"]) == 1,
                        MatchEvent = Convert.ToString(row["MatchEvent"]),
                        MemberWinLoss = Convert.ToDecimal(row["MemberWinLoss"]),
                        SMAWinLoss = Convert.ToDecimal(row["SMAWinLoss"]),
                        ExtraPT = Convert.ToDecimal(row["ExtraPT"]),
                        Commission = Convert.ToDecimal(row["Commission"]),
                        RealIncome = Convert.ToDecimal(row["RealInCome"]),
                        VirtualWinLoss = Convert.ToDecimal(row["VirtualIncome"]),
                        RealTurnover = Convert.ToDecimal(row["RealTurnover"]),
                        VirtualTurnover = Convert.ToDecimal(row["VirtualTurnover"]),
                        IsHedge = Convert.ToInt32(row["IsHedge"]) == 1,
                        IsOutright = Convert.ToInt32(row["IsOutright"]) == 1
                    });
                }
            }

            return leagueWinLoss;
        }

        public async Task<List<SiteEntity>> GetSites(ICriteria criteria)
        {
            var leagueWinLoss = new List<SiteEntity>();

            var dataSet = await repository.FetchDataSetAsync(criteria); 

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                var winLossTable = dataSet.Tables[0];
               
                foreach (DataRow row in winLossTable.Rows)
                {
                    leagueWinLoss.Add(new SiteEntity
                    {
                        SiteID = Convert.ToInt32(row["SiteID"]),
                        SiteName = Convert.ToString(row["SiteName"]),
                        MemberWinLoss = Convert.ToDecimal(row["MemberWinLoss"]),
                        SMAWinLoss = Convert.ToDecimal(row["SMAWinLoss"]),
                        ExtraPT = Convert.ToDecimal(row["ExtraPT"]),
                        Commission = Convert.ToDecimal(row["Commission"]),
                        RealIncome = Convert.ToDecimal(row["RealInCome"]),
                        VirtualWinLoss = Convert.ToDecimal(row["VirtualIncome"]),
                        RealTurnover = Convert.ToDecimal(row["RealTurnover"]),
                        VirtualTurnover = Convert.ToDecimal(row["VirtualTurnover"])
                    });
                }
            }

            return leagueWinLoss;
        }

        public async Task<List<SuperEntity>> GetSupers(ICriteria criteria)
        {
            var leagueWinLoss = new List<SuperEntity>();

            var dataSet = await repository.FetchDataSetAsync(criteria);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                var winLossTable = dataSet.Tables[0];
             
                foreach (DataRow row in winLossTable.Rows)
                {
                    leagueWinLoss.Add(new SuperEntity
                    {
                        SuperID = Convert.ToInt32(row["SuperID"]),
                        SuperName = Convert.ToString(row["SuperName"]),
                        MemberWinLoss = Convert.ToDecimal(row["MemberWinLoss"]),
                        SMAWinLoss = Convert.ToDecimal(row["SMAWinLoss"]),
                        ExtraPT = Convert.ToDecimal(row["ExtraPT"]),
                        Commission = Convert.ToDecimal(row["Commission"]),
                        RealIncome = Convert.ToDecimal(row["RealInCome"]),
                        VirtualWinLoss = Convert.ToDecimal(row["VirtualIncome"]),
                        RealTurnover = Convert.ToDecimal(row["RealTurnover"]),
                        VirtualTurnover = Convert.ToDecimal(row["VirtualTurnover"]),
                        HasVR = Convert.ToBoolean(row["HasVR"])
                    });
                }
            }

            return leagueWinLoss;
        }

        public async Task<List<VirtualRateEntity>> GetVirtualRate(ICriteria criteria)
        {
            var leagueWinLoss = new List<VirtualRateEntity>();
            
            var dataSet = await repository.FetchDataSetAsync(criteria);


            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                var winLossTable = dataSet.Tables[0];
                
                foreach (DataRow row in winLossTable.Rows)
                {
                    leagueWinLoss.Add(new VirtualRateEntity
                    {
                        Sport = Convert.ToString(row["sport"]),
                        BetType = Convert.ToString(row["bettype"]),
                        LiveStatus = Convert.ToString(row["islive"]),
                        VirtualRate = Convert.ToDecimal(row["virtualMultiplier"])
                    });
                }
            }

            return leagueWinLoss;
        }

        public async Task<List<SportEntity>> GetSport(ICriteria criteria)
        {
            var sport = new List<SportEntity>();
            
            var dataSet = await repository.FetchDataSetAsync(criteria);


            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                var sportTable = dataSet.Tables[0];
                foreach (DataRow row in sportTable.Rows)
                {
                    sport.Add(new SportEntity
                    {
                        SportGroupType = Convert.ToInt32(row["SportGroupType"]),
                        SportGroupName = Convert.ToString(row["SportGroupName"]),
                        SportType = Convert.ToInt32(row["SportType"]),
                        SportName = Convert.ToString(row["SportName"]),
                        BetTypeGroupName = Convert.ToString(row["BetGroupName"]),
                        Bettype = Convert.ToInt32(row["Bettype"]),
                        BettypeName = Convert.ToString(row["BettypeName"])
                    });
                }
            }

            return sport;
        }

        public async Task<List<SiteFilterEntity>> GetSiteFilter(ICriteria criteria)
        {
            var sites = new List<SiteFilterEntity>();

            var dataSet = await repository.FetchDataSetAsync(criteria);

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                var siteTable = dataSet.Tables[0];
                foreach (DataRow row in siteTable.Rows)
                {
                    sites.Add(new SiteFilterEntity
                    {
                        SiteGroup = Convert.ToString(row["SiteGroup"]),
                        SiteName = Convert.ToString(row["SiteName"]),
                        SiteLabel = Convert.ToString(row["SiteLabel"])
                    });
                }
            }

            return sites;
        }

        public async Task<int> GetPermissionProfileID(ICriteria criteria)
        {
            var dataSet = await repository.FetchDataSetAsync(criteria);

            if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                var firstRow = dataSet.Tables[0].Rows[0];
                var permissionProfileId = Convert.ToInt32(firstRow["PermisionProfileID"]);

                return permissionProfileId;
            }

            return 0;
        }
    }
}
