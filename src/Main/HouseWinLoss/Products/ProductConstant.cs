namespace HouseWinLoss.Products
{
    public static class ProductConstant
    {
        public const string RM = "RM";

        public const string OtherSports = "Other Sports";

        public const string Soccer = "Soccer";

        public static string[] NameSportsbook =>
        [
            "Soccer",
            "Other Sports",
            "Parlay",
            "SB Adjustment",
            "SABA Soccer",
            "SABA Soccer PinGoal"
        ];

        public static string[] SoccerPermissionProducts =>
        [
            "Soccer",
            "Parlay",
            "RM",
            "SB Adjustment",
            "SABA Soccer",
            "SABA Soccer PinGoal"
        ];

        public static string[] OtherSportsPermissionProducts =>
        [
            "Other Sports",
            "Parlay"
        ];
    }
}