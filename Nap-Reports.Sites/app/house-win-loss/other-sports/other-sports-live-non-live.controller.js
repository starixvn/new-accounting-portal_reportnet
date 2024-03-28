import angular from "angular";
(function (angular, undefined) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .controller('OtherSportsLiveNonLiveController', OtherSportsLiveNonLiveController);

    OtherSportsLiveNonLiveController.$inject = ['$scope', '$element', 'fanexGridService', 'showHideColumnService'];
    function OtherSportsLiveNonLiveController($scope, $element, fanexGridService, showHideColumnService) {
        var pane = $scope.pane;
        var showHideColumnBuilder = new showHideColumnService(pane);
        var tab = $scope.tab;
        var gridElements = $element.find('.freegrid');
        var sportTypeGridElement = angular.element(gridElements[0]);
        var liveNonLiveGridElement = angular.element(gridElements[1]);
        $scope.show = showHideColumnBuilder.toggle;
        $scope.generate = generate;
        $scope.generateBetType = generateBetType;
        $scope.generateSportType = generateSportType;
        $scope.viewSetting = {};
        $scope.generateProductBetTypes = generateProductBetTypes;
        $scope.generateProductLiveNonLiveBetTypes = generateProductLiveNonLiveBetTypes;

        function generate(viewSetting) {
            $scope.viewSetting = viewSetting;
            var sportTypeGridOptions,
                liveNonLiveGridOptions;

            pane.service.getData(viewSetting, pane.data).then(function (data) {
                sportTypeGridOptions = data[0];
                liveNonLiveGridOptions = data[1];
                renderAll();
            });

            function renderAll() {
                var isDataReady = angular.isDefined(sportTypeGridOptions) && angular.isDefined(liveNonLiveGridOptions);
                if (!isDataReady) {
                    return;
                }

                showHideColumnBuilder.build([sportTypeGridElement, liveNonLiveGridElement], [sportTypeGridOptions, liveNonLiveGridOptions]);
                createLinksForSportType(sportTypeGridOptions);
                renderGridForSportType(sportTypeGridOptions);
                createLinksForLiveNonLive(liveNonLiveGridOptions);
                renderGridForLiveNonLive(liveNonLiveGridOptions);
                pane.compileAll();
            }
        }

        function renderGridForSportType(data) {
            var titleInfo = {
                text: tab.generateBreadcrumbHtmlWithSuffix(""),
                showCollapseExpandButton: true,
                showExcelButton: true,
                showButtonsWhenNoData: true,
                exportExcelFunction: exportExcel,
                customButtons: showHideColumnBuilder.getButtonHtml()
            };

            data.titleInfo = titleInfo;
            data.bottomBorderWidth = 3;
            sportTypeGridElement.fanexGrid(data);
        }

        function createLinksForSportType(data) {
            for (var i = 0; i < data.bodyRows.length; i++) {
                var sportTypeRow = data.bodyRows[i];
                var sportTypeName = sportTypeRow.Columns[0].Value;
                var sportType = sportTypeRow.Columns[0].CustomAttr;
                var isSabaSport = sportTypeRow.Columns[1].CustomAttr;
                sportTypeRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateBetType(' + sportType + ',\'' + sportTypeName.replace(/'/g, "\\'") + '\',' + isSabaSport + ')">' + sportTypeName + '</a>';

                if (sportTypeRow != null && sportTypeRow.SubRows != null) {
                    for (var j = 0; j < sportTypeRow.SubRows.length; j++) {
                        var subRow = sportTypeRow.SubRows[j];
                        var siteType = subRow.Columns[0].Value;
                        subRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateProductBetTypes(' + sportType + ', \'' + sportTypeName.replace(/'/g, "\\'") + '\',' + isSabaSport + ',\'' + siteType + '\')">' + siteType + '</a>';
                    }
                }
            }
        }

        function renderGridForLiveNonLive(data) {
            var titleInfo = {
                text: tab.generateBreadcrumbHtmlWithSuffix(" Live/Non-Live"),
                showCollapseExpandButton: true,
                showExcelButton: false
            };

            data.titleInfo = titleInfo;
            data.bottomBorderWidth = 3;
            liveNonLiveGridElement.fanexGrid(data);
        }

        function createLinksForLiveNonLive(data) {
            for (var i = 0; i < data.bodyRows.length; i++) {
                var row = data.bodyRows[i];
                var name = row.Columns[0].Value;
                var isLive = parseInt(row.Columns[0].CustomAttr);
                row.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateSportType(' + isLive + ')">' + name + '</a>';

                if (row != null && row.SubRows != null) {
                    for (var j = 0; j < row.SubRows.length; j++) {
                        var subRow = row.SubRows[j];
                        var siteType = subRow.Columns[0].Value;
                        subRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateProductLiveNonLiveBetTypes(' + isLive + ',\'' + name + '\',\'' + siteType + '\')">' + siteType + '</a>';
                    }
                }
            }
        }

        function generateBetType(sportType, sportTypeName, isSabaSport) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                sportType: sportType,
                sportTypeName: sportTypeName,
                isLive: 2,
                isSabaSport: isSabaSport
            });
            tab.goForward('betType', data);
        }

        function generateSportType(isLive) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                isLive: isLive
            });
            tab.goForward('sportType', data);
        }

        function exportExcel() {
            var sportTypeTitle = tab.generateBreadcrumbTextWithSuffix("");
            var liveNonLiveTitle = tab.generateBreadcrumbTextWithSuffix(" Live/Non-Live");
            var httpData = {};

            angular.extend(httpData, pane.data);
            angular.extend(httpData, {
                sportTypeTitle: sportTypeTitle,
                liveNonLiveTitle: liveNonLiveTitle
            });
            pane.service.exportExcel($scope.viewSetting, httpData, pane.getFileName());
        }

        function generateProductBetTypes(sportType, sportTypeName, isSabaSport, siteType) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                sportType: sportType,
                sportTypeName: sportTypeName,
                isLive: 2,
                isSabaSport: isSabaSport,
                siteType: siteType,
                isDrilldownFromMain: false,
                nameDrilldown: sportTypeName,
                isMajor: 2
            });
            tab.goForward('productBetType', data);
        }

        function generateProductLiveNonLiveBetTypes(isLive, name, siteType) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                isLive: isLive,
                siteType: siteType,
                isDrilldownFromMain: false,
                nameDrilldown: name,
                isMajor: 2,
                isSabaSport: 2
            });
            tab.goForward('productBetType', data);
        }
    }
})(angular, undefined);