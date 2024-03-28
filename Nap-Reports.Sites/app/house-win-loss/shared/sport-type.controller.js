import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .controller('SportTypeController', SportTypeController);

    SportTypeController.$inject = ['$injector', '$scope', '$element', 'fanexGridService', 'showHideColumnService'];
    function SportTypeController($injector, $scope, $element, fanexGridService, showHideColumnService) {
        var pane = $scope.pane;
        var showHideColumnBuilder = new showHideColumnService(pane);
        var tab = $scope.tab;
        var gridElement = $element.find('.freegrid');
        $scope.show = showHideColumnBuilder.toggle;
        $scope.generate = generate;
        $scope.generateBetType = generateBetType;
        $scope.viewSetting = {};
        $scope.generateProductBetTypes = generateProductBetTypes;

        function generate(viewSetting) {
            $scope.viewSetting = viewSetting;
            pane.service.getData(viewSetting, pane.data).then(function (data) {
                showHideColumnBuilder.build(gridElement, data);
                createLinks(data, pane.data.isLive);
                renderGrid(data);
                pane.compileAll();
            });
        }

        function renderGrid(data) {
            var titleInfo = {
                text: tab.generateBreadcrumbHtml(),
                showCollapseExpandButton: true,
                showExcelButton: true,
                exportExcelFunction: exportExcel,
                customButtons: showHideColumnBuilder.getButtonHtml()
            };

            data.titleInfo = titleInfo;
            data.gridExpandHeight = fanexGridService.calculateExpandHeight(gridElement, true);
            data.bottomBorderWidth = 3;
            gridElement.fanexGrid(data);
        }

        function createLinks(data, isLive) {
            for (var i = 0; i < data.bodyRows.length; i++) {
                var sportTypeRow = data.bodyRows[i];
                var sportTypeId = sportTypeRow.Columns[0].CustomAttr;
                var sportTypeName = sportTypeRow.Columns[0].Value;
                var isSabaSport = sportTypeRow.Columns[1].CustomAttr;
                sportTypeRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateBetType(' + sportTypeId + ',\'' + sportTypeName.replace(/'/g, "\\'") + '\',' + isLive + ',' + isSabaSport + ')">' + sportTypeName + '</a>';

                if (pane.data.aliasProductID === -4 &&
                    sportTypeRow != null &&
                    sportTypeRow.SubRows != null) {
                    for (var j = 0; j < sportTypeRow.SubRows.length; j++) {
                        var subRow = sportTypeRow.SubRows[j];
                        var siteType = subRow.Columns[0].Value;
                        subRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateProductBetTypes(' + sportTypeId + ', \'' + sportTypeName.replace(/'/g, "\\'") + '\',' + isLive + ',\''  + siteType + '\')">' + siteType + '</a>';
                    }
                }
            }
        }

        function generateBetType(sportType, sportTypeName, isLive, isSabaSport) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                sportType: sportType,
                sportTypeName: sportTypeName,
                isLive: isLive,
                isSabaSport: isSabaSport
            });
            tab.goForward('betType', data);
        }

        function exportExcel() {
            pane.service.exportExcelForSingleGrid($scope.viewSetting, tab, pane);
        }

        function generateProductBetTypes(sportType, sportTypeName, isLive, siteType) {
            var isSabaSport = 2;

            if (sportTypeName === 'Basketball' || sportTypeName === 'Cricket' || sportTypeName === 'E-Sports') {
                isSabaSport = 0;
            } else if (sportTypeName === 'SABA Basketball' || sportTypeName === 'SABA Cricket') {
                isSabaSport = 1;
            } else if (sportTypeName === 'SABA Basketball PinGoal' || sportTypeName === 'SABA E-Sports PinGoal') {
                isSabaSport = 3;
            } else {
                isSabaSport = 2;
            }

            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                sportType: sportType,
                sportTypeName: sportTypeName,
                isLive: isLive,
                isSabaSport: isSabaSport,
                isDrilldownFromMain: false,
                nameDrilldown: sportTypeName,
                isMajor: 2,
                siteType: siteType
            });
            tab.goForward('productBetType', data);
        }
    }
})(angular);