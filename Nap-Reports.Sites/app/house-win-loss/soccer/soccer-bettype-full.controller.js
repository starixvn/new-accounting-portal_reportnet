import angular from "angular";
(function (angular, undefined) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .controller('SoccerBetTypeFullController', SoccerBetTypeFullController);

    SoccerBetTypeFullController.$inject = ['$scope', '$element', 'fanexGridService', 'showHideColumnService', 'soccerConstants'];
    function SoccerBetTypeFullController($scope, $element, fanexGridService, showHideColumnService, soccerConstants) {
        var pane = $scope.pane;
        var showHideColumnBuilder = new showHideColumnService(pane);
        var tab = $scope.tab;
        var gridElements = $element.find('.freegrid');
        var betTypeFullGridElement = angular.element(gridElements[0]);
        var betTypeLiveGridElement = angular.element(gridElements[1]);
        var betTypeNonLiveGridElement = angular.element(gridElements[2]);

        $scope.viewSetting = {};
        $scope.show = showHideColumnBuilder.toggle;
        $scope.generate = generate;
        $scope.generateSite = generateSite;
        $scope.generateMatchDetail = generateMatchDetail;
        $scope.createDrilldownLink = createDrilldownLink;
        $scope.createDrilldownLinks = createDrilldownLinks;
        $scope.renderGrid = renderGrid;
        $scope.exportExcel = exportExcel;


        function generate(viewSetting) {
            var betTypeFullGridOptions,
                betTypeLiveGridOptions,
                betTypeNonLiveGridOptions;
            $scope.viewSetting = viewSetting;
            

            pane.service.getData(viewSetting, pane.data, 2).then(function (data) {
                betTypeFullGridOptions = data;
                renderAll();
            });

            pane.service.getData(viewSetting, pane.data, 1).then(function (data) {
                betTypeLiveGridOptions = data;
                renderAll();
            });

            pane.service.getData(viewSetting, pane.data, 0).then(function (data) {
                betTypeNonLiveGridOptions = data;
                renderAll();
            });

            function renderAll() {
                var isDataReady = angular.isDefined(betTypeFullGridOptions)
                    && angular.isDefined(betTypeLiveGridOptions)
                    && angular.isDefined(betTypeNonLiveGridOptions);
                if (!isDataReady) {
                    return;
                }

                showHideColumnBuilder.build([betTypeFullGridElement, betTypeLiveGridElement, betTypeNonLiveGridElement]
                    , [betTypeFullGridOptions, betTypeLiveGridOptions, betTypeNonLiveGridOptions]);

                var canViewMatchDetail = pane.data.endDate.diff(pane.data.startDate, 'days') <= 3;
                if (canViewMatchDetail) {
                    createDrilldownLinks(betTypeFullGridOptions.bodyRows, 2);
                    createDrilldownLinks(betTypeLiveGridOptions.bodyRows, 1);
                    createDrilldownLinks(betTypeNonLiveGridOptions.bodyRows, 0);
                }

                renderFullGrid(betTypeFullGridElement, betTypeFullGridOptions);
                renderGrid(betTypeLiveGridElement, betTypeLiveGridOptions, 1);
                renderGrid(betTypeNonLiveGridElement, betTypeNonLiveGridOptions, 0);
                pane.compileAll();
            }
        }

        function createDrilldownLinks(rows, isLive) {
            for (var i = 0; i < rows.length; i++) {
                createDrilldownLink(rows[i], isLive);
            }
        }

        function createDrilldownLink(betTypeRow, isLive) {
            var betTypeName = betTypeRow.Columns[0].Value;
            var betTypeId = betTypeRow.Columns[0].CustomAttr;

            if (betTypeRow.SubRows) {
                betTypeRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateMatchDetail(' + betTypeId + ',\'' + betTypeName.replace(/'/g, "\\'") + '\',' + isLive + ')">' + betTypeName + '</a>';
            }
            if (betTypeRow.SubRows) {
                for (var i = 0; i < betTypeRow.SubRows.length; i++) {
                    var siteRow = betTypeRow.SubRows[i];
                    var siteName = siteRow.Columns[0].Value;
                    siteRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateSite(' + betTypeId + ',\'' + betTypeName.replace(/'/g, "\\'") + '\',\'' + siteName.replace(/'/g, "\\'") + '\',' + isLive + ')">' + siteName + '</a>';
                }
            }
        }
        function renderFullGrid(gridElement, gridOptions){
            var titleInfo = {
                text: tab.generateBreadcrumbHtml(),
                showCollapseExpandButton: true,
                showExcelButton: true,
                showButtonsWhenNoData: true,
                exportExcelFunction: $scope.exportExcel,
                customButtons: showHideColumnBuilder.getButtonHtml()
            };

            gridOptions.titleInfo = titleInfo;
            gridOptions.bottomBorderWidth = 3;
            gridElement.fanexGrid(gridOptions);
        }
        
        function renderGrid(gridElement, gridOptions, isLive) {
            var suffix = isLive === 1 ? ' "LIVE"' : ' "NON-LIVE"';
            var titleInfo = {
                text: tab.generateBreadcrumbHtmlWithSuffix(suffix),
                showCollapseExpandButton: true,
                showExcelButton: false
            };
            
            gridOptions.titleInfo = titleInfo;
            gridOptions.bottomBorderWidth = 3;
            gridElement.fanexGrid(gridOptions);
        }

        function generateSite(betType, betTypeName, site, isLive) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                betType: betType,
                betTypeName: betTypeName,
                siteType: site,
                mainColumnText: site,
                isLive: isLive
            });

            data.isHedge = getHedgeParameters(data.sportType, betTypeName);
            data.isCorners = getCornerParameters(betType, betTypeName);

            tab.goForward('site', data);
        }

        function generateMatchDetail(betType, betTypeName, isLive) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                betType: betType,
                betTypeName: betTypeName,
                isLive: isLive
            });
            data.isHedge = getHedgeParameters(data.sportType, betTypeName);
            data.isCorners = getCornerParameters(betType, betTypeName);

            tab.goForward('matchDetail', data);
        }

        function getHedgeParameters(sportType, betTypeName) {
            if (sportType && sportType > 1) {
                return 2;
            }

            return betTypeName && betTypeName.indexOf("(hedge)") > -1 ? 1 : 0;
        }

        function exportExcel() {
            var isSabaSport = 0;
            var betTypeFullTitle = tab.generateBreadcrumbText();
            var betTypeLiveTitle = tab.generateBreadcrumbTextWithSuffix(' "LIVE"');
            var betTypeNonLiveTitle = tab.generateBreadcrumbTextWithSuffix(' "NON-LIVE"');
            var productName = pane.data.productName;

            if (productName === 'SABA Soccer') {
                isSabaSport = 1;
            }

            if (productName === 'SABA Soccer PinGoal') {
                isSabaSport = 3;
            }

            var httpData = {
                betTypeFullTitle: betTypeFullTitle,
                betTypeLiveTitle: betTypeLiveTitle,
                betTypeNonLiveTitle: betTypeNonLiveTitle,
                isSabaSport: isSabaSport
            };
            angular.extend(httpData, pane.data);
            pane.service.exportExcel($scope.viewSetting, httpData, pane.getFileName());
        }

        function getCornerParameters(betType, betTypeName) {
            if (betType !== 228 && betType !== 229) {
                return 2;
            }

            return betTypeName && betTypeName.indexOf("(Corners)") > -1 ? 1 : 0;
        }
    }
})(angular, undefined);