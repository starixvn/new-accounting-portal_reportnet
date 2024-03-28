import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .controller('BetTypeController', BetTypeController);

    BetTypeController.$inject = ['$scope', '$element', 'houseWinlossService', 'fanexGridService', 'showHideColumnService'];
    function BetTypeController($scope, $element, houseWinlossService, fanexGridService, showHideColumnService) {
        var pane = $scope.pane;
        var showHideColumnBuilder = new showHideColumnService(pane);
        var tab = $scope.tab;
        var gridElement = $element.find('.freegrid');
        const betradarVirtualSportsMixParlayBettype = 2799;
        $scope.show = showHideColumnBuilder.toggle;
        $scope.generate = generate;
        $scope.generateSite = generateSite;
        $scope.generateMatchDetail = generateMatchDetail;
        $scope.createDrilldownLink = createDrilldownLink;
        $scope.createDrilldownLinks = createDrilldownLinks;
        $scope.renderGrid = renderGrid;
        $scope.exportExcel = exportExcel;
        

        function generate(viewSetting) {
            $scope.viewSetting = viewSetting;
            pane.service.getData(viewSetting, pane.data).then(function (data) {
                showHideColumnBuilder.build(gridElement, data);

                var canViewMatchDetail = pane.data.endDate.diff(pane.data.startDate, 'days') <= 3;
                if (canViewMatchDetail) {
                    createDrilldownLinks(data.bodyRows, pane.data.isLive);
                }

                renderGrid(data);
                pane.compileAll();
            });
        }

        function createDrilldownLinks(rows, isLive) {
            for (var i = 0; i < rows.length; i++) {
                createDrilldownLink(rows[i], isLive);
            }
        }

        function createDrilldownLink(betTypeRow, isLive) {
            var betTypeName = betTypeRow.Columns[0].Value;
            var betTypeId = betTypeRow.Columns[0].CustomAttr;

            if (isLive === null || isLive === undefined) {
                isLive = betTypeName.includes('Live') === true ? 1 : 0;
            }

            var isFreePlay = betTypeName === 'Colossus Bets (Free Play)';

            var template = houseWinlossService.getViewSetting(pane.data.productName, 'matchDetail', pane.data);
            var hasDetailDrilldown = angular.isDefined(template) && parseInt(betTypeId) !== betradarVirtualSportsMixParlayBettype;

            if (betTypeRow.SubRows && hasDetailDrilldown) {
                betTypeRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateMatchDetail(' + betTypeId + ',\'' + betTypeName.replace(/'/g, "\\'") + '\',' + isLive + ')">' + betTypeName + '</a>';
            }
            if (betTypeRow.SubRows) {
                for (var i = 0; i < betTypeRow.SubRows.length; i++) {
                    var siteRow = betTypeRow.SubRows[i];
                    var siteName = siteRow.Columns[0].Value;
                    siteRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateSite(' + betTypeId + ',\'' + betTypeName.replace(/'/g, "\\'") + '\',\'' + siteName.replace(/'/g, "\\'") + '\',' + isLive + ',' + isFreePlay + ')">' + siteName + '</a>';
                }
            }
        }

        function renderGrid(data) {
            var titleInfo = {
                text: tab.generateBreadcrumbHtml(),
                showCollapseExpandButton: true,
                showExcelButton: true,
                exportExcelFunction: $scope.exportExcel,
                customButtons: showHideColumnBuilder.getButtonHtml()
            };

            data.titleInfo = titleInfo;
            data.gridExpandHeight = fanexGridService.calculateExpandHeight(gridElement, true);
            data.bottomBorderWidth = 3;
            gridElement.fanexGrid(data);
        }

        function generateSite(betType, betTypeName, site, isLive, isFreePlay) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                betType: betType,
                betTypeName: betTypeName,
                siteType: site,
                mainColumnText: site,
                isLive: isLive,
                isFreePlay: isFreePlay
            });

            data.isHedge = getHedgeParameters(data.sportType, betTypeName);
            data.isCorners = getCornerParameters(betType, betTypeName);

            tab.goForward('site', data);
        }

        function generateMatchDetail(betType, betTypeName, isLive) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data,{
                betType: betType,
                betTypeName: betTypeName,
                isLive: isLive
            });
            data.isHedge = getHedgeParameters(data.sportType, betTypeName);
            data.isCorners = getCornerParameters(betType, betTypeName);

            tab.goForward('matchDetail', data);
        }

        function exportExcel() {
            pane.service.exportExcelForSingleGrid($scope.viewSetting, tab, pane);
        }

        function getHedgeParameters(sportType, betTypeName) {
            if (sportType && sportType > 1) {
                return 2;
            }

            return betTypeName && betTypeName.indexOf("(hedge)") > -1 ? 1 : 0;
        }

        function getCornerParameters(betType, betTypeName) {
            if (betType !== 228 && betType !== 229) {
                return 2;
            }

            return betTypeName && betTypeName.indexOf("(Corners)") > -1 ? 1 : 0;
        }
    }
})(angular);