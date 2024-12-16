import angular from "angular";
import moment from "moment";
(function (angular) {
    "use strict";

    var templateSetting = {
        "Main": {
            mainView: "product",
            product: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "Daily2hSoccerSportTypeController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/soccer-minor/sport-type",
                mainParameters: { sheetName: "Soccer Minor Bet Types", type: "soccer-minor-sport-type", mainColumnText: "Sport Type", mainColumnWidth: 210 },
                getTitleFunction: "getMainTitle"
            },
            betType: {
                templateUrl: "app/house-win-loss/shared/three-fanex-grid-with-title.html",
                controller: "Daily2hSoccerBetTypeFullController",
                service: "soccerMinorBetTypeService",
                serviceBaseUrl: "api/house-win-loss-by-products/soccer-minor/bet-type",
                mainParameters: { sheetName: "Soccer Minor Bet Types", type: "soccer-minor-bet-type", mainColumnText: "Bet Type", mainColumnWidth: 270 },
                getTitleFunction: "getSoccerMinorBetTypeTitle"
            },
            matchDetail: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "Daily2hSoccerMatchDetailController",
                service: "sportsbookMatchDetailService",
                serviceBaseUrl: function (data) {
                    return data.betType === 10
                        ? 'api/house-win-loss-by-products/soccer-minor/outright-match-detail'
                        : 'api/house-win-loss-by-products/soccer-minor/match-detail';
                },
                mainParameters: { sheetName: "Soccer Minor Bet Types", sportType: 1 },
                getTitleFunction: "getMatchDetailTitleLive"
            },
            site: {
                templateUrl: "app/house-win-loss/shared/fanex-grid-with-title.html",
                controller: "SiteController",
                service: "remoteDataService",
                serviceBaseUrl: "api/house-win-loss-by-products/soccer-minor/site",
                mainParameters: { sheetName: "Soccer Minor Bet Types", sportType: 1, type: "soccer-minor-site" },
                getTitleFunction: "getSiteTitleLive"
            }
        }
    };
    angular
        .module("app.houseWinlossApp")
        .constant("templateSetting", templateSetting);
})(angular);