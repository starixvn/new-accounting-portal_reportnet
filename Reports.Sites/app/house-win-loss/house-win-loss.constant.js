import angular from "angular";
import moment from "moment";

(function (angular) {
    "use strict";

    var templateSetting = {
        "Main": {
            mainView: "product",
            product: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "ProductController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/product",
                mainParameters: { sheetName: "House Win Loss by Products", permissions: productPermissions },
                getTitleFunction: "getMainTitle"
            }
        },
        "Soccer": {
            mainView: "soccerLiveNonLive",
            productBetType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "ProductBetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/product-bettype",
                mainParameters: { sheetName: "House Win Loss by Products", permissions: productPermissions },
                getTitleFunction: "getProductBetTypeLiveTitle"
            },
            soccerLiveNonLive: {
                templateUrl: "app/house-win-loss/shared/two-fanex-grid-with-title.html",
                controller: "SoccerLiveNonLiveController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/soccer/live-nonlive",
                mainParameters: { sheetName: "House Win Loss by Products" },
                getTitleFunction: "getSoccerLiveNonLiveTitle"
            },
            soccerMajorMinor: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SoccerMajorMinorController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/soccer/major-minor",
                mainParameters: { sheetName: "House Win Loss by Products", type: "soccer-major-minor", mainColumnText: "Major/Minor" },
                getTitleFunction: "getSoccerMajorMinorTitle"
            },
            soccerBetTypeFull: {
                templateUrl: "app/house-win-loss/shared/three-fanex-grid-with-title.html",
                controller: "SoccerBetTypeFullController",
                service: "soccerBetTypeFullService",
                serviceBaseUrl: "api/house-win-loss-by-products/soccer/bettype",
                mainParameters: { sheetName: "House Win Loss by Products", type: "soccer-bet-type", mainColumnText: "Bet Type", mainColumnWidth: 270 },
                getTitleFunction: "getSoccerBetTypeTitle"
            },
            soccerBetType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: function (data) {
                    var isMajor = data.betTypeNameMajorMinor === "Soccer-Major Bet Types" ? 1 : 0;
                    var isSumBetType = isMajor === 1 && data.isLive === 2;
                    var isGroupBetType = isMajor === 1 && data.isLive === 0;

                    if (isSumBetType) {
                        return "api/house-win-loss-by-products/soccer/sum-bettype";
                    }
                    else if (isGroupBetType) {
                        return "api/house-win-loss-by-products/soccer/group-bettype";
                    }
                    else {
                        return "api/house-win-loss-by-products/soccer/bettype";
                    }
                },
                mainParameters: function (data) {
                    var isMajor = data.betTypeNameMajorMinor.includes('Major') ? 1 : 0;
                    return {
                        isMajor: isMajor,
                        type: "soccer-bet-type",
                        mainColumnText: "Bet Type",
                        mainColumnWidth: 210,
                        sheetName: "House Win Loss by Products"
                    };
                },
                getTitleFunction: "getSoccerBetTypeTitle"
            },
            matchDetail: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SportsbookMatchDetailController",
                service: "sportsbookMatchDetailService",
                serviceBaseUrl: function (data) {
                    return data.betType === 10
                        ? 'api/house-win-loss-by-products/sportsbook/outright-match-detail'
                        : 'api/house-win-loss-by-products/sportsbook/match-detail';
                },
                mainParameters: { sheetName: "House Win Loss by Products" },
                getTitleFunction: "getMatchDetailTitleLive"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/sportsbook/site",
                mainParameters: { sheetName: "House Win Loss by Products", type: "sportsbook-site" },
                getTitleFunction: "getSiteTitleLive"
            }
        },
        "SABA Soccer": {
            mainView: "soccerLiveNonLive",
            productBetType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "ProductBetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/product-bettype",
                mainParameters: { sheetName: "House Win Loss by Products", permissions: productPermissions, isSabaSport: 1 },
                getTitleFunction: "getProductBetTypeLiveTitle"
            },
            soccerLiveNonLive: {
                templateUrl: "app/house-win-loss/shared/two-fanex-grid-with-title.html",
                controller: "SoccerLiveNonLiveController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/soccer/live-nonlive",
                mainParameters: { sheetName: "House Win Loss by Products", isSabaSport: 1 },
                getTitleFunction: "getSoccerLiveNonLiveTitle"
            },
            soccerMajorMinor: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SoccerMajorMinorController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/soccer/major-minor",
                mainParameters: { sheetName: "House Win Loss by Products", type: "soccer-major-minor", mainColumnText: "Major/Minor", isSabaSport: 1 },
                getTitleFunction: "getSoccerMajorMinorTitle"
            },
            soccerBetTypeFull: {
                templateUrl: "app/house-win-loss/shared/three-fanex-grid-with-title.html",
                controller: "SoccerBetTypeFullController",
                service: "soccerBetTypeFullService",
                serviceBaseUrl: "api/house-win-loss-by-products/soccer/bettype",
                mainParameters: { sheetName: "House Win Loss by Products", type: "soccer-bet-type", mainColumnText: "Bet Type", mainColumnWidth: 270, isSabaSport: 1 },
                getTitleFunction: "getSoccerBetTypeTitle"
            },
            soccerBetType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: function (data) {
                    var isMajor = data.betTypeNameMajorMinor === "SABA Soccer-Major Bet Types" ? 1 : 0;
                    var isSumBetType = isMajor === 1 && data.isLive === 2;
                    var isGroupBetType = isMajor === 1 && data.isLive === 0;

                    if (isSumBetType) {
                        return "api/house-win-loss-by-products/soccer/sum-bettype";
                    }
                    else if (isGroupBetType) {
                        return "api/house-win-loss-by-products/soccer/group-bettype";
                    }
                    else {
                        return "api/house-win-loss-by-products/soccer/bettype";
                    }
                },
                mainParameters: function (data) {
                    var isMajor = data.betTypeNameMajorMinor === "SABA Soccer-Major Bet Types" ? 1 : 0;
                    return {
                        isMajor: isMajor,
                        type: "soccer-bet-type",
                        mainColumnText: "Bet Type",
                        mainColumnWidth: 210,
                        sheetName: "House Win Loss by Products",
                        isSabaSport: 1
                    };
                },
                getTitleFunction: "getSoccerBetTypeTitle"
            },
            matchDetail: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SportsbookMatchDetailController",
                service: "sportsbookMatchDetailService",
                serviceBaseUrl: function (data) {
                    return data.betType === 10
                        ? 'api/house-win-loss-by-products/sportsbook/outright-match-detail'
                        : 'api/house-win-loss-by-products/sportsbook/match-detail';
                },
                mainParameters: { sheetName: "House Win Loss by Products", isSabaSport: 1 },
                getTitleFunction: "getMatchDetailTitleLive"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/sportsbook/site",
                mainParameters: { sheetName: "House Win Loss by Products", type: "sportsbook-site", isSabaSport: 1 },
                getTitleFunction: "getSiteTitleLive"
            }
        },
        "SABA Soccer PinGoal": {
            mainView: "soccerLiveNonLive",
            productBetType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "ProductBetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/product-bettype",
                mainParameters: { sheetName: "House Win Loss by Products", permissions: productPermissions, isSabaSport: 3  },
                getTitleFunction: "getProductBetTypeLiveTitle"
            },
            soccerLiveNonLive: {
                templateUrl: "app/house-win-loss/shared/two-fanex-grid-with-title.html",
                controller: "SoccerLiveNonLiveController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/soccer/live-nonlive",
                mainParameters: { sheetName: "House Win Loss by Products", isSabaSport: 3 },
                getTitleFunction: "getSoccerLiveNonLiveTitle"
            },
            soccerMajorMinor: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SoccerMajorMinorController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/soccer/major-minor",
                mainParameters: { sheetName: "House Win Loss by Products", type: "soccer-major-minor", mainColumnText: "Major/Minor", isSabaSport: 3 },
                getTitleFunction: "getSoccerMajorMinorTitle"
            },
            soccerBetTypeFull: {
                templateUrl: "app/house-win-loss/shared/three-fanex-grid-with-title.html",
                controller: "SoccerBetTypeFullController",
                service: "soccerBetTypeFullService",
                serviceBaseUrl: "api/house-win-loss-by-products/soccer/bettype",
                mainParameters: { sheetName: "House Win Loss by Products", type: "soccer-bet-type", mainColumnText: "Bet Type", mainColumnWidth: 270, isSabaSport: 3 },
                getTitleFunction: "getSoccerBetTypeTitle"
            },
            soccerBetType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: function (data) {
                    var isMajor = data.betTypeNameMajorMinor === "SABA Soccer PinGoal-Major Bet Types" ? 1 : 0;
                    var isSumBetType = isMajor === 1 && data.isLive === 2;
                    var isGroupBetType = isMajor === 1 && data.isLive === 0;

                    if (isSumBetType) {
                        return "api/house-win-loss-by-products/soccer/sum-bettype";
                    }
                    else if (isGroupBetType) {
                        return "api/house-win-loss-by-products/soccer/group-bettype";
                    }
                    else {
                        return "api/house-win-loss-by-products/soccer/bettype";
                    }
                },
                mainParameters: function (data) {
                    var isMajor = data.betTypeNameMajorMinor === "SABA Soccer PinGoal-Major Bet Types" ? 1 : 0;
                    return {
                        isMajor: isMajor,
                        type: "soccer-bet-type",
                        mainColumnText: "Bet Type",
                        mainColumnWidth: 210,
                        sheetName: "House Win Loss by Products",
                        isSabaSport: 3
                    };
                },
                getTitleFunction: "getSoccerBetTypeTitle"
            },
            matchDetail: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SportsbookMatchDetailController",
                service: "sportsbookMatchDetailService",
                serviceBaseUrl: function (data) {
                    return data.betType === 10
                        ? 'api/house-win-loss-by-products/sportsbook/outright-match-detail'
                        : 'api/house-win-loss-by-products/sportsbook/match-detail';
                },
                mainParameters: { sheetName: "House Win Loss by Products", isSabaSport: 3 },
                getTitleFunction: "getMatchDetailTitleLive"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/sportsbook/site",
                mainParameters: { sheetName: "House Win Loss by Products", type: "sportsbook-site", isSabaSport: 3 },
                getTitleFunction: "getSiteTitleLive"
            }
        },
        "Metric Gaming - Superlive": {
            mainView: "liveNonLive",
            productBetType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "ProductBetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/product-bettype",
                mainParameters: { sheetName: "House Win Loss by Products", permissions: productPermissions, isSabaSport: 2 },
                getTitleFunction: "getProductBetTypeTitle"
            },
            liveNonLive: {
                templateUrl: "app/house-win-loss/shared/two-fanex-grid-with-title.html",
                controller: "MetricGamingLiveNonLiveController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/metric-gaming-superlive/live-non-live",
                mainParameters: { sheetName: "House Win Loss by Products" },
                getTitleFunction: "getProductTitleLiveNonLive"
            },
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/metric-gaming-superlive/bet-type",
                mainParameters: { sheetName: "House Win Loss by Products", type: "metric-gaming-bet-type", mainColumnText: "Bet Type", mainColumnWidth: 220 },
                getTitleFunction: "getSportsbookBetTypeTitle"
            },
            matchDetail: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SportsbookMatchDetailController",
                service: "sportsbookMatchDetailService",
                serviceBaseUrl: "api/house-win-loss-by-products/metric-gaming-superlive/match-detail",
                mainParameters: { sheetName: "House Win Loss by Products" },
                getTitleFunction: "getMatchDetailTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/metric-gaming-superlive/site",
                mainParameters: { sheetName: "House Win Loss by Products", type: "metric-gaming-superlive-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Other Sports": {
            mainView: "liveNonLive",
            productBetType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "ProductBetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/product-bettype",
                mainParameters: { sheetName: "House Win Loss by Products", permissions: productPermissions },
                getTitleFunction: "getProductBetTypeLiveTitle"
            },
            liveNonLive: {
                templateUrl: "app/house-win-loss/shared/two-fanex-grid-with-title.html",
                controller: "OtherSportsLiveNonLiveController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/other-sports/live-non-live",
                mainParameters: { sheetName: "House Win Loss by Products", aliasProductID: -4 },
                getTitleFunction: "getProductTitleLiveNonLive"
            },
            sportType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SportTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/other-sports/sport-type",
                mainParameters: { sheetName: "House Win Loss by Products", type: "other-sports-sport-type", mainColumnText: "Sport Type" },
                getTitleFunction: "getProductTitleLive"
            },
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/other-sports/bet-type",
                mainParameters: { sheetName: "House Win Loss by Products", type: "other-sports-bet-type", mainColumnText: "Bet Type" },
                getTitleFunction: "getSportsbookBetTypeTitle"
            },
            matchDetail: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SportsbookMatchDetailController",
                service: "sportsbookMatchDetailService",
                serviceBaseUrl: function (data) {
                    return data.betType === 10
                        ? 'api/house-win-loss-by-products/sportsbook/outright-match-detail'
                        : 'api/house-win-loss-by-products/sportsbook/match-detail';
                },
                mainParameters: { sheetName: "House Win Loss by Products" },
                getTitleFunction: "getMatchDetailTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/sportsbook/site",
                mainParameters: { sheetName: "House Win Loss by Products", type: "sportsbook-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Parlay": {
            mainView: "betType",
            productBetType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "ProductBetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/product-bettype",
                mainParameters: { sheetName: "House Win Loss by Products", permissions: productPermissions, isSabaSport: 2 },
                getTitleFunction: "getProductBetTypeTitle"
            },
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "ParlayBetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/parlay/bet-type",
                mainParameters: { sheetName: "House Win Loss by Products", type: "parlay-bet-type", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/parlay/site",
                mainParameters: { sheetName: "House Win Loss by Products" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Live Casino": {
            mainView: "casinoProduct",
            casinoProduct: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "CasinoProductController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", type: "universal-second-level", mainColumnText: "3rd Party Product" },
                getTitleFunction: "getProductTitle"
            },
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-bettype", mainColumnText: "Bet Type" },
                getTitleFunction: "getCasinoProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Casino": {
            mainView: "casinoProduct",
            casinoProduct: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "CasinoProductController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", type: "universal-second-level", mainColumnText: "3rd Party Product" },
                getTitleFunction: "getProductTitle"
            },
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-bettype", mainColumnText: "Bet Type" },
                getTitleFunction: "getCasinoProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Number Game": {
            mainView: "sportType",
            sportType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "NGSportTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", type: "universal-second-level", mainColumnText: "Sport Type" },
                getTitleFunction: "getProductTitle"
            },
            league: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "NGLeagueController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", type: "number-game-drill-down", mainColumnText: "League" },
                getTitleFunction: "getSportTypeTitle"
            },
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "NGBetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", type: "number-game-drill-down", mainColumnText: "Bet Type" },
                getTitleFunction: "getLeagueTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", type: "number-game-drill-down" },
                getTitleFunction: "getSiteTitle"
            },
            matchDetail: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "MatchDetailController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/number-game/match-detail",
                mainParameters: { sheetName: "House Win Loss by Products" },
                getTitleFunction: "getMatchDetailTitle"
            }
        },
        "Virtual Sports": {
            mainView: "sportType",
            sportType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SportTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", type: "universal-second-level", mainColumnText: "Sport Type" },
                getTitleFunction: "getProductTitle"
            },
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-bettype", mainColumnText: "Bet Type" },
                getTitleFunction: "getSportTypeTitle"
            },
            matchDetail: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "MatchDetailController",
                service: "remoteDataService",
                serviceBaseUrl: function (data) {
                    return data.sportType >= 181 && data.sportType <= 185
                           ? "api/house-win-loss-by-products/virtual-sports/racing-match-detail"
                           : "api/house-win-loss-by-products/virtual-sports/sportsbook-match-detail";
                },
                mainParameters: { sheetName: "House Win Loss by Products" },
                getTitleFunction: "getMatchDetailTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Bettrade": {
            mainView: "liveNonLive",
            liveNonLive: {
                templateUrl: "app/house-win-loss/shared/two-fanex-grid-with-title.html",
                controller: "CashOutLiveNonLiveController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/cash-out/live-non-live",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2 },
                getTitleFunction: "getProductTitleLiveNonLive"
            },
            matchDetail: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "CashOutMatchDetailController",
                service: "sportsbookMatchDetailService",
                serviceBaseUrl: "api/house-win-loss-by-products/cash-out/match-detail",
                mainParameters: { sheetName: "House Win Loss by Products", bettype: -201 },
                getTitleFunction: "getMatchDetailTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", bettype: -201, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Racing": {
            mainView: "sportType",
            sportType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SportTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", type: "universal-second-level", mainColumnText: "Sport Type" },
                getTitleFunction: "getProductTitle"
            },
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-bettype", mainColumnText: "Bet Type" },
                getTitleFunction: "getSportTypeTitle"
            },
            matchDetail: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "MatchDetailController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/racing/match-detail",
                mainParameters: { sheetName: "House Win Loss by Products" },
                getTitleFunction: "getMatchDetailTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Bingo": {
            mainView: "betType",
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-second-level", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "RNG Keno": {
            mainView: "betType",
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-second-level", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Colossus Bets": {
            mainView: "betType",
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-second-level", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "NG2": {
            mainView: "sportType",
            sportType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SportTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-second-level", mainColumnText: "Sport Type" },
                getTitleFunction: "getProductTitle"
            },
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-bettype", mainColumnText: "Bet Type" },
                getTitleFunction: "getSportTypeTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitleLive"
            }
        },
        "Fishing World": {
            mainView: "betType",
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-second-level", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Cash Out": {
            mainView: "liveNonLive",
            liveNonLive: {
                templateUrl: "app/house-win-loss/shared/two-fanex-grid-with-title.html",
                controller: "CashOutLiveNonLiveController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/cash-out/live-non-live",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2 },
                getTitleFunction: "getProductTitleLiveNonLive"
            },
            matchDetail: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "CashOutMatchDetailController",
                service: "sportsbookMatchDetailService",
                serviceBaseUrl: "api/house-win-loss-by-products/cash-out/match-detail",
                mainParameters: { sheetName: "House Win Loss by Products", bettype: -25 },
                getTitleFunction: "getMatchDetailTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", bettype: -25, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Lottery": {
            mainView: "betType",
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-second-level", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Virtual Sports 2": {
            mainView: "sportType",
            sportType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SportTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", type: "universal-second-level", mainColumnText: "Sport Type" },
                getTitleFunction: "getProductTitle"
            },
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-bettype", mainColumnText: "Bet Type" },
                getTitleFunction: "getSportTypeTitle"
            },
            matchDetail: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "MatchDetailController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/virtual-sports-betradar/match-detail",
                mainParameters: { sheetName: "House Win Loss by Products" },
                getTitleFunction: "getMatchDetailTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Lucky 3": {
            mainView: "betType",
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-bet-type", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-site" },
                getTitleFunction: "getSiteTitleLive"
            }
        },
        "Bullfighting": {
            mainView: "betType",
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", type: "universal-second-level", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            },
            matchDetail: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "MatchDetailController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/bullfighting/match-detail",
                mainParameters: { sheetName: "House Win Loss by Products" },
                getTitleFunction: "getMatchDetailTitle"
            }
        },
        "Exchange": {
            mainView: "betType",
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-second-level", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Poker Games": {
            mainView: "betType",
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-second-level", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Bet Builder": {
            mainView: "sportType",
            productBetType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "ProductBetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/product-bettype",
                mainParameters: { sheetName: "House Win Loss by Products", permissions: productPermissions, isSabaSport: 2 },
                getTitleFunction: "getProductBetTypeTitle"
            },
            sportType: {
                templateUrl: "app/house-win-loss/shared/two-fanex-grid-with-title.html",
                controller: "BetBuilderSportTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/bet-builder/sport-type",
                mainParameters: { sheetName: "House Win Loss by Products", mainColumnText: "Sport Type" },
                getTitleFunction: "getProductTitle"
            },
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/bet-builder/bettype",
                mainParameters: { sheetName: "House Win Loss by Products", mainColumnText: "Bet Type" },
                getTitleFunction: "getSportsbookBetTypeTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/bet-builder/site",
                mainParameters: { sheetName: "House Win Loss by Products", bettype: 38 },
                getTitleFunction: "getSiteTitle"
            },
            matchDetail: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "CashOutMatchDetailController",
                service: "sportsbookMatchDetailService",
                serviceBaseUrl: 'api/house-win-loss-by-products/bet-builder/match-detail',
                mainParameters: { sheetName: "House Win Loss by Products", bettype: 38 },
                getTitleFunction: "getMatchDetailTitle"
            }
        },
        "Saba Promotion": {
            mainView: "betType",
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-second-level", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Virtual Games": {
            mainView: "betType",
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-second-level", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Indo 4D": {
            mainView: "betType",
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", type: "universal-second-level", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Togel 4D": {
            mainView: "betType",
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-second-level", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Table Game": {
            mainView: "betType",
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-second-level", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "SABA.games": {
            mainView: "sportType",
            sportType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SportTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", type: "universal-second-level", productId: 42, mainColumnText: "Sport Type" },
                getTitleFunction: "getProductTitle"
            },
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-bettype", mainColumnText: "Bet Type" },
                getTitleFunction: "getSportTypeTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Funky Games": {
            mainView: "betType",
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-second-level", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Virtual Sports 3": {
            mainView: "betType",
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-second-level", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Sports Lottery": {
            mainView: "betType",
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-second-level", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Vgaming": {
            mainView: "sportType",
            sportType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SportTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", type: "universal-second-level", productId: 66, mainColumnText: "Sport Type" },
                getTitleFunction: "getProductTitle"
            },
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-bettype", mainColumnText: "Bet Type" },
                getTitleFunction: "getSportTypeTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Bitcoin": {
            mainView: "betType",
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-second-level", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            },
        },
        "FGG": {
            mainView: "betType",
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-second-level", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            },
        },
        "Player Tips": {
            mainView: "betType",
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-second-level", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Virtual Sports 3": {
            mainView: "sportType",
            sportType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SportTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", type: "universal-second-level", mainColumnText: "Sport Type" },
                getTitleFunction: "getProductTitle"
            },
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-bettype", mainColumnText: "Bet Type" },
                getTitleFunction: "getSportTypeTitle"
            },
            matchDetail: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "MatchDetailController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/virtual-sports-betradar/match-detail",
                mainParameters: { sheetName: "House Win Loss by Products" },
                getTitleFunction: "getMatchDetailTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Virtual Sports 4": {
            mainView: "sportType",
            sportType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SportTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", type: "universal-second-level", mainColumnText: "Sport Type" },
                getTitleFunction: "getProductTitle"
            },
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-bettype", mainColumnText: "Bet Type" },
                getTitleFunction: "getSportTypeTitle"
            },
            matchDetail: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "MatchDetailController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/virtual-sports-betradar/match-detail",
                mainParameters: { sheetName: "House Win Loss by Products" },
                getTitleFunction: "getMatchDetailTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        },
        "Saba Coins": {
            mainView: "betType",
            betType: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-second-level", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/simple-site-grid",
                mainParameters: { sheetName: "House Win Loss by Products", isLive: 2, type: "universal-thirdparty-site" },
                getTitleFunction: "getSiteTitle"
            }
        }
    };
    angular
        .module("app.houseWinlossApp")
        .constant("templateSetting", templateSetting);
})(angular);