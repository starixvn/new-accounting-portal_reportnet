// --------------------------------------------------------------------------
// <copyright file="BetTypePerformanceItem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.5.17">Created</change>
// </history>

namespace SoccerPerformanceDetail.ViewModel
{
    using SoccerPerformanceDetail.Model;
    using System;

    /// <summary>
    /// Bet type performance view model.
    /// </summary>
    [Serializable]
    public class BetTypePerformanceItem
    {
        public BetTypePerformanceItem()
        {
        }

        public BetTypePerformanceItem(BetTypePerformanceInfo betTypeData)
        {
            if (betTypeData != null && betTypeData.BetType.HasValue)
            {
                this.BetTypeId = betTypeData.BetType.Value;
                this.BetTypeName = betTypeData.TypeName;
                this.NoOfTraders = Convert.ToInt32(betTypeData.TraderCount);
                this.NoOfLeagues = Convert.ToInt32(betTypeData.LeagueCount);
                this.NoOfMatches = Convert.ToInt32(betTypeData.MatchCount);
                this.Turnover = Convert.ToDecimal(betTypeData.Turnover);
                this.WinLoss = Convert.ToDecimal(betTypeData.WinLoss);
                this.Margin = betTypeData.Margin;
                this.TurnoverReal = Convert.ToDecimal(betTypeData.TurnoverReal);
                this.WinLossReal = Convert.ToDecimal(betTypeData.WinLossReal);
                this.MarginReal = betTypeData.MarginReal;
                this.BetCount = Convert.ToInt32(betTypeData.BetCount);                
                this.IsLive = betTypeData.IsLive;
                this.FlagForViewReport = betTypeData.FlagForViewReport;
            }
        }

        public int BetTypeId { get; set; }

        public string BetTypeName { get; set; }

        public int NoOfTraders { get; set; }

        public int NoOfLeagues { get; set; }

        public int NoOfMatches { get; set; }

        public decimal Turnover { get; set; }

        public decimal WinLoss { get; set; }

        public decimal? Margin { get; set; }

        public decimal TurnoverReal { get; set; }

        public decimal WinLossReal { get; set; }

        public decimal? MarginReal { get; set; }

        public int BetCount { get; set; }     

        public bool IsLive { get; set; }

        public int FlagForViewReport { get; set; }
    }
}