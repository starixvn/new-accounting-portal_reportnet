using System;
using System.Collections.Generic;
using System.Linq;

namespace Nap_Reports.HouseWinLoss.Soccer.BaseService
{
    public static class GroupConstants
    {
        public enum MainBetType
        {
            Handicap = 1,
            FirstHandicap = 7,
            OverUnder2 = 3,
            FirstOverUnder = 8
        }

        public enum BetTypeGroup1
        {
            Handicap = 1,
            OneXTwoBetType = 5,
            DoubleChanceBetType = 24,
            DrawNoBetBetType = 25,
            ThreeWayyHandicap = 28,
            HomeNoBetBetType = 121,
            AwayNoBetBetType = 122,
            DrawNoDrawBetType = 123,
            MyanmarHandicap = 301
        }

        public enum BetTypeGroup2
        {
            OverUnder2 = 3,
            TotalGoal = 6,
            OddEvenBetType = 2,
            MyanmarOverUnder = 302
        }

        public enum BetTypeGroup3
        {
            FirstHandicap = 7,
            First1X2BetType = 15,
            FirstDoubleChange = 151,
            FirstMyanmarHandicap = 303
        }

        public static List<int> BetTypeGroup123()
        {
            var group1Arr = (int[])Enum.GetValues(typeof(BetTypeGroup1));
            var group2Arr = (int[])Enum.GetValues(typeof(BetTypeGroup2));
            var group3Arr = (int[])Enum.GetValues(typeof(BetTypeGroup3));
            var betTypeGroup123 = group1Arr.Concat(group2Arr).Concat(group3Arr);

            return betTypeGroup123.ToList();
        }
    }
}