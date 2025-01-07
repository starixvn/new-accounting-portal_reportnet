import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .factory('titleService', titleService);

    titleService.$inject = ['soccerConstants'];
    function titleService(soccerConstants) {
        var service = {
            getMainTitle: getMainTitle,
            getProductTitle: getProductTitle,
            getProductTitleLiveNonLive: getProductTitleLiveNonLive,
            getProductTitleLive: getProductTitleLive,
            getSoccerLiveNonLiveTitle: getSoccerLiveNonLiveTitle,
            getSoccerMajorMinorTitle: getSoccerMajorMinorTitle,
            getSoccerBetTypeTitle: getSoccerBetTypeTitle,
            getSportsbookBetTypeTitle: getSportsbookBetTypeTitle,
            getBetTypeLiveNonLiveTitle: getBetTypeLiveNonLiveTitle,
            getCasinoProductTitle: getCasinoProductTitle,
            getSportTypeTitle: getSportTypeTitle,
            getSportTypeTitleLive: getSportTypeTitleLive,
            getLeagueTitle: getLeagueTitle,
            getSiteTitle: getSiteTitle,
            getSiteTitleLive: getSiteTitleLive,
            getMatchDetailTitle: getMatchDetailTitle,
            getMatchDetailTitleLive: getMatchDetailTitleLive,
            getSoccerMinorBetTypeTitle: getSoccerMinorBetTypeTitle,
            getProductBetTypeTitle: getProductBetTypeTitle,
            getProductBetTypeLiveTitle: getProductBetTypeLiveTitle
        };
        return service;

        function getMainTitle(data) {
            return 'Main';
        }

        function getProductTitle(data) {
            return data.productName;
        }

        function getProductTitleLiveNonLive(data, isLast) {
            return isLast
                ? data.productName + ' Live/Non-Live '
                : data.productName;
        }

        function getProductTitleLive(data) {
            return data.productName
                + getLiveText(data.isLive);
        }

        function getSoccerLiveNonLiveTitle(data, isLast) {
            var sabaSoccerTitle = "";
            
            if(data.productName === "Soccer"){
                sabaSoccerTitle = 'SOCCER';
            }

            if(data.productName === "SABA Soccer"){
                sabaSoccerTitle = 'SABA SOCCER';
            }

            if(data.productName === "SABA Soccer PinGoal"){
                sabaSoccerTitle = 'SABA SOCCER PinGoal';
            }


            return isLast
                ? sabaSoccerTitle + ' LIVE/NON-LIVE'
                : sabaSoccerTitle;
        }

        function getSoccerMajorMinorTitle(data) {
            if (data.isLive === soccerConstants.LiveNonLive) {
                return data.productName + ' ' + "MAJOR/MINOR";
            }
            else {
                var liveNonLive = data.isLive === soccerConstants.Live ? "LIVE" : "NON-LIVE";
                return data.productName + ' ' + liveNonLive;
            }
        }

        function getSoccerBetTypeTitle(data) {
            var betTypeNameMajorMinor = data.betTypeNameMajorMinor;
            var majorMinor = betTypeNameMajorMinor.includes('Major') ? "-MAJOR" : "-MINOR";
            if (data.isLive === soccerConstants.LiveNonLive) {
                return data.betTypeNameMajorMinor
            }
            else {
                var liveNonLive = data.isLive === soccerConstants.Live ? '"LIVE"' : '"NON-LIVE"';
                return data.betTypeNameMajorMinor + ' ' + liveNonLive;
            }
        }

        function getSportsbookBetTypeTitle(data) {
            var sportType = getSportTypeTitle(data);
            var product = getProductTitleLive(data);
            var viewingFromSportType = sportType !== undefined;
            return viewingFromSportType
                        ? sportType
                        : product;
        }

        function getBetTypeLiveNonLiveTitle(data) {
            return data.isLive === undefined
                ? data.productName
                : data.productName + getLiveText(data.isLive);
        }

        function getCasinoProductTitle(data) {
            return data.casinoProductName;
        }

        function getSportTypeTitle(data) {
            return data.sportTypeName;
        }

        function getLeagueTitle(data) {
            return data.leagueName;
        }

        function getSportTypeTitleLive(data) {
            return data.sportTypeName
                + getLiveText(data.isLive);
        }

        function getSiteTitle(data) {
            var site = data.siteType === undefined ? "ALL" : data.siteType;
            return data.betTypeName
                + ' | '
                + site
                + ' Site';
        }

        function getSiteTitleLive(data) {
            return data.betTypeName
                + getLiveText(data.isLive)
                + '| '
                + data.siteType + ' Site';
        }

        function getMatchDetailTitle(data) {
            return data.betTypeName;
        }

        function getMatchDetailTitleLive(data) {
            var isLiveText = data.isLive === 2 ? '' : (data.isLive === 1 ? ' "Live"' : ' "Non-Live"');
            return data.betTypeName + isLiveText;
        }

        function getLiveText(isLive) {
            var isLiveValue = parseInt(isLive);
            var isLiveText = isLiveValue === 2 ? '' : (isLiveValue === 1 ? ' "Live"' : ' "Non-Live"');

            return isLiveText;
        }

        function getSoccerMinorBetTypeTitle(data) {
            return data.sportTypeName;
        }

        function getProductBetTypeTitle(data) {
            return data.isDrilldownFromMain ? data.productName + ' | ' + data.siteType + ' SITE': data.sportTypeName + ' | ' + data.siteType + ' SITE';
        }

        function getProductBetTypeLiveTitle(data) {
            return data.isDrilldownFromMain ? data.productName + ' | ' + data.siteType + ' SITE' : data.nameDrilldown + ' | ' + data.siteType + ' SITE';
        }
    }
})(angular);