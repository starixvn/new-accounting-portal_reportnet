import angular from "angular";
(function (angular, undefined) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .controller('SoccerMajorMinorController', SoccerMajorMinorController);

    SoccerMajorMinorController.$inject = ['$scope', '$element', 'fanexGridService', 'soccerConstants', 'showHideColumnService'];
    function SoccerMajorMinorController($scope, $element, fanexGridService, soccerConstants, showHideColumnService) {
        var pane = $scope.pane;
        var showHideColumnBuilder = new showHideColumnService(pane);
        var tab = $scope.tab;
        var gridElements = $element.find('.freegrid');
        $scope.generate = generate;
        $scope.show = showHideColumnBuilder.toggle;
        $scope.generateSoccerLiveNonLiveBetTypePane = generateSoccerLiveNonLiveBetTypePane;
        $scope.viewSetting = {};
        pane.data.isLive = pane.data.soccerLiveNonLive.includes('Non-Live')
            ? soccerConstants.NonLive 
            : soccerConstants.Live;
        $scope.generateProductBetTypes = generateProductBetTypes;

        function generate(viewSetting) {
            $scope.viewSetting = viewSetting;
            pane.service.getData(viewSetting, pane.data).then(function (data) {
                showHideColumnBuilder.build(gridElements, data);
                createLinksForMajorMinor(data);
                renderGrid(gridElements, data);
                pane.compileAll();
            });
        }

        function renderGrid(gridElement, data) {
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

        function createLinksForMajorMinor(data) {
            for (var i = 0; i < data.bodyRows.length; i++) {
                var betTypeRow = data.bodyRows[i];
                var betTypeNameMajorMinor = betTypeRow.Columns[0].Value;
                betTypeRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateSoccerLiveNonLiveBetTypePane(\'' + betTypeNameMajorMinor + '\')">' + betTypeNameMajorMinor + '</a>';

                if (betTypeRow != null && betTypeRow.SubRows != null) {
                    for (var j = 0; j < betTypeRow.SubRows.length; j++) {
                        var subRow = betTypeRow.SubRows[j];
                        var siteType = subRow.Columns[0].Value;
                        subRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateProductBetTypes(\'' + betTypeNameMajorMinor + '\',' + '\'' + siteType + '\')">' + siteType + '</a>';
                    }
                }
            }
        }

        function generateSoccerLiveNonLiveBetTypePane(betTypeNameMajorMinor) {
            var data = {};
            var isSabaSport = 0;
            var isMajor = false;

            if(betTypeNameMajorMinor.includes('SABA Soccer')) {
                isSabaSport = 1;
            }
            if(betTypeNameMajorMinor.includes('SABA Soccer PinGoal')) {
                isSabaSport = 3;
            }

            if(betTypeNameMajorMinor.includes('Major')) {
                isMajor = true;
            }

            angular.extend(data, pane.data);
            angular.extend(data, {
                betTypeNameMajorMinor: betTypeNameMajorMinor,
                sportTypeName: betTypeNameMajorMinor,
                isSabaSport: isSabaSport,
                isMajor: isMajor
            });
            tab.goForward('soccerBetType', data);
        }

        function exportExcel() {
            var title = tab.generateBreadcrumbText();
            var httpData = {
                title: title
            };
            angular.extend(httpData, pane.data);
            pane.service.exportExcel($scope.viewSetting, httpData, pane.getFileName());
        }

        function generateProductBetTypes(betTypeNameMajorMinor, siteType) {
            var isMajor = 2;

            if (betTypeNameMajorMinor.includes('Major')) {
                isMajor = 1;
            }

            if (betTypeNameMajorMinor.includes('Minor')) {
                isMajor = 0;
            }

            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                siteType: siteType,
                isMajor: isMajor,
                isDrilldownFromMain: false,
                nameDrilldown: betTypeNameMajorMinor
            });
            tab.goForward('productBetType', data);
        }
    }
})(angular, undefined);