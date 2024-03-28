import angular from "angular";
import moment from "moment";

(function (angular) {
    "use strict";

    var cricketProductId = 50;

    var templateSetting = {
        "Main": {
            mainView: "product",
            product: {
                templateUrl: "app/cn88-win-loss/shared/fanex-grid-with-title.html",
                controller: "ProductController",
                service: "remoteDataService",
                serviceBaseUrl: "api/cn88-win-loss-by-products/product",
                mainParameters: { sheetName: "CN88 WinLoss", permissions: productPermissions },
                getTitleFunction: "getMainTitle"
            }
        },
        "Soccer": {
            mainView: "soccerLiveNonLive",
            soccerLiveNonLive: {
                templateUrl: "app/cn88-win-loss/shared/two-fanex-grid-with-title.html",
                controller: "SoccerLiveNonLiveController",
                service: "remoteDataService",
                serviceBaseUrl: "api/cn88-win-loss-by-products/soccer/live-nonlive",
                mainParameters: { sheetName: "CN88 WinLoss" },
                getTitleFunction: "getSoccerLiveNonLiveTitle"
            },
            soccerMajorMinor: {
                templateUrl: "app/cn88-win-loss/shared/fanex-grid-with-title.html",
                controller: "SoccerMajorMinorController",
                service: "remoteDataService",
                serviceBaseUrl: "api/cn88-win-loss-by-products/soccer/major-minor",
                mainParameters: { sheetName: "CN88 WinLoss", type: "soccer-major-minor", mainColumnText: "Major/Minor" },
                getTitleFunction: "getSoccerMajorMinorTitle"
            },
            soccerBetTypeFull: {
                templateUrl: "app/cn88-win-loss/shared/three-fanex-grid-with-title.html",
                controller: "SoccerBetTypeFullController",
                service: "soccerBetTypeFullService",
                serviceBaseUrl: "api/cn88-win-loss-by-products/soccer/bettype",
                mainParameters: { sheetName: "CN88 WinLoss", type: "soccer-bet-type", mainColumnText: "Bet Type", mainColumnWidth: 270 },
                getTitleFunction: "getSoccerBetTypeTitle"
            },
            soccerBetType: {
                templateUrl: "app/cn88-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: function (data) {
                    var isMajor = data.betTypeNameMajorMinor === "Soccer-Major Bet Types";
                    var isSumBetType = isMajor && data.isLive === 2;
                    var isGroupBetType = isMajor && data.isLive === 0;

                    if (isSumBetType) {
                        return "api/cn88-win-loss-by-products/soccer/sum-bettype";
                    }
                    else if (isGroupBetType) {
                        return "api/cn88-win-loss-by-products/soccer/group-bettype";
                    }
                    else {
                        return "api/cn88-win-loss-by-products/soccer/bettype";
                    }
                },
                mainParameters: function (data) {
                    var isMajor = data.betTypeNameMajorMinor === "Soccer-Major Bet Types";
                    return {
                        isMajor: isMajor,
                        type: "soccer-bet-type",
                        mainColumnText: "Bet Type",
                        mainColumnWidth: 210,
                        sheetName: "CN88 WinLoss"
                    };
                },
                getTitleFunction: "getSoccerBetTypeTitle"
            },
            matchDetail: {
                templateUrl: "app/cn88-win-loss/shared/fanex-grid-with-title.html",
                controller: "SportsbookMatchDetailController",
                service: "sportsbookMatchDetailService",
                serviceBaseUrl: function (data) {
                    return data.betType === 10
                        ? 'api/cn88-win-loss-by-products/sportsbook/outright-match-detail'
                        : 'api/cn88-win-loss-by-products/sportsbook/match-detail';
                },
                mainParameters: { sheetName: "CN88 WinLoss" },
                getTitleFunction: "getMatchDetailTitleLive"
            }
        },
        "Other Sports": {
            mainView: "liveNonLive",
            liveNonLive: {
                templateUrl: "app/cn88-win-loss/shared/two-fanex-grid-with-title.html",
                controller: "OtherSportsLiveNonLiveController",
                service: "remoteDataService",
                serviceBaseUrl: "api/cn88-win-loss-by-products/other-sports/live-non-live",
                mainParameters: { sheetName: "CN88 WinLoss" },
                getTitleFunction: "getProductTitleLiveNonLive"
            },
            sportType: {
                templateUrl: "app/cn88-win-loss/shared/fanex-grid-with-title.html",
                controller: "SportTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/cn88-win-loss-by-products/other-sports/sport-type",
                mainParameters: { sheetName: "CN88 WinLoss", type: "other-sports-sport-type", mainColumnText: "Sport Type" },
                getTitleFunction: "getProductTitleLive"
            },
            betType: {
                templateUrl: "app/cn88-win-loss/shared/fanex-grid-with-title.html",
                controller: function (data) {
                    return data.sportType === cricketProductId
                    ? 'BetTypeNewCricketController'
                    : 'BetTypeController';
                },
                service: "remoteDataService",
                serviceBaseUrl: function (data) {
                    return data.sportType === cricketProductId
                    ? 'api/cn88-win-loss-by-products/other-sports/new-cricket/bet-type'
                    : 'api/cn88-win-loss-by-products/other-sports/bet-type';
                },
                mainParameters: { sheetName: "CN88 WinLoss", type: "other-sports-bet-type", mainColumnText: "Bet Type" },
                getTitleFunction: "getSportsbookBetTypeTitle"
            },
            matchDetail: {
                templateUrl: "app/cn88-win-loss/shared/fanex-grid-with-title.html",
                controller: "SportsbookMatchDetailController",
                service: "sportsbookMatchDetailService",
                serviceBaseUrl: function (data) {
                    return data.betType === 10
                        ? 'api/cn88-win-loss-by-products/sportsbook/outright-match-detail'
                        : 'api/cn88-win-loss-by-products/sportsbook/match-detail';
                },
                mainParameters: { sheetName: "CN88 WinLoss" },
                getTitleFunction: "getMatchDetailTitle"
            }
        },
        "Parlay": {
            mainView: "betType",
            betType: {
                templateUrl: "app/cn88-win-loss/shared/fanex-grid-with-title.html",
                controller: "ParlayBetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/cn88-win-loss-by-products/parlay/bet-type",
                mainParameters: { sheetName: "CN88 WinLoss", type: "parlay-bet-type", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            }
        },
        "Number Game": {
            mainView: "sportType",
            sportType: {
                templateUrl: "app/cn88-win-loss/shared/fanex-grid-with-title.html",
                controller: "NGSportTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/cn88-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "CN88 WinLoss", productId: 3, type: "universal-sport-type", mainColumnText: "Sport Type" },
                getTitleFunction: "getProductTitle"
            },
            league: {
                templateUrl: "app/cn88-win-loss/shared/fanex-grid-with-title.html",
                controller: "NGLeagueController",
                service: "remoteDataService",
                serviceBaseUrl: "api/cn88-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "CN88 WinLoss", productId: 3, type: "number-game-league", mainColumnText: "League" },
                getTitleFunction: "getSportTypeTitle"
            },
            betType: {
                templateUrl: "app/cn88-win-loss/shared/fanex-grid-with-title.html",
                controller: "NGBetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/cn88-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "CN88 WinLoss", productId: 3, type: "number-game-bet-type", mainColumnText: "Bet Type" },
                getTitleFunction: "getLeagueTitle"
            },
            matchDetail: {
                templateUrl: "app/cn88-win-loss/shared/fanex-grid-with-title.html",
                controller: "MatchDetailController",
                service: "remoteDataService",
                serviceBaseUrl: "api/cn88-win-loss-by-products/number-game/match-detail",
                mainParameters: { sheetName: "CN88 WinLoss" },
                getTitleFunction: "getMatchDetailTitle"
            }
        },
        "Virtual Sports": {
            mainView: "sportType",
            sportType: {
                templateUrl: "app/cn88-win-loss/shared/fanex-grid-with-title.html",
                controller: "SportTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/cn88-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "CN88 WinLoss", type: "virtual-sports-sport-type", mainColumnText: "Sport Type" },
                getTitleFunction: "getProductTitle"
            },
            betType: {
                templateUrl: "app/cn88-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/cn88-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "CN88 WinLoss", type: "virtual-sports-bet-type", mainColumnText: "Bet Type" },
                getTitleFunction: "getSportTypeTitle"
            },
            matchDetail: {
                templateUrl: "app/cn88-win-loss/shared/fanex-grid-with-title.html",
                controller: "MatchDetailController",
                service: "remoteDataService",
                serviceBaseUrl: function (data) {
                    return data.sportType >= 181 && data.sportType <= 185
                           ? "api/cn88-win-loss-by-products/virtual-sports/racing-match-detail"
                           : "api/cn88-win-loss-by-products/virtual-sports/sportsbook-match-detail";
                },
                mainParameters: { sheetName: "CN88 WinLoss" },
                getTitleFunction: "getMatchDetailTitle"
            }
        },
        "RNG Keno": {
            mainView: "betType",
            betType: {
                templateUrl: "app/cn88-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/cn88-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "CN88 WinLoss", productId: 8, isLive: 2, type: "universal-bet-type", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            }
        },
        "Virtual Sports 2": {
            mainView: "sportType",
            sportType: {
                templateUrl: "app/cn88-win-loss/shared/fanex-grid-with-title.html",
                controller: "SportTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/cn88-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "CN88 WinLoss", productId: 27, type: "virtual-sports-betradar-sport-type", mainColumnText: "Sport Type" },
                getTitleFunction: "getProductTitle"
            },
            betType: {
                templateUrl: "app/cn88-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/cn88-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "CN88 WinLoss", productId: 27, type: "virtual-sports-betradar-bet-type", mainColumnText: "Bet Type" },
                getTitleFunction: "getSportTypeTitle"
            },
            matchDetail: {
                templateUrl: "app/cn88-win-loss/shared/fanex-grid-with-title.html",
                controller: "MatchDetailController",
                service: "remoteDataService",
                serviceBaseUrl: "api/cn88-win-loss-by-products/virtual-sports-betradar/match-detail",
                mainParameters: { sheetName: "CN88 WinLoss" },
                getTitleFunction: "getMatchDetailTitle"
            }
        },
        "Lottery": {
            mainView: "betType",
            betType: {
                templateUrl: "app/cn88-win-loss/shared/fanex-grid-with-title.html",
                controller: "BetTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/cn88-win-loss-by-products/simple-grid",
                mainParameters: { sheetName: "CN88 WinLoss", productId: 28, isLive: 2, type: "universal-bet-type", mainColumnText: "Bet Type" },
                getTitleFunction: "getProductTitle"
            }
        }
    };
    angular
        .module("app.cn88WinlossApp")
        .constant("templateSetting", templateSetting);
})(angular);