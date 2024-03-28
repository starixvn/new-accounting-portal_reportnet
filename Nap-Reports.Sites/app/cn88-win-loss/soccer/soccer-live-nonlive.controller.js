import angular from "angular";
(function (angular, undefined) {
    'use strict';

    angular
        .module('app.cn88WinlossApp')
        .controller('SoccerLiveNonLiveController', SoccerLiveNonLiveController);

    SoccerLiveNonLiveController.$inject = ['$scope', '$element', 'fanexGridService', 'showHideColumnService', 'soccerConstants'];
    function SoccerLiveNonLiveController($scope, $element, fanexGridService, showHideColumnService, soccerConstants) {
        var pane = $scope.pane;
        var showHideColumnBuilder = new showHideColumnService(pane);
        var tab = $scope.tab;
        var gridElements = $element.find('.freegrid');
        var majorMinorGridElement = angular.element(gridElements[0]);
        var liveNonLiveGridElement = angular.element(gridElements[1]);
        $scope.show = showHideColumnBuilder.toggle;
        $scope.generate = generate;
        $scope.generateSoccerLiveNonLivePane = generateSoccerLiveNonLivePane;
        $scope.generateSoccerBetTypePane = generateSoccerBetTypePane;
        $scope.viewSetting = {};
        pane.data.isLive = soccerConstants.LiveNonLive;

        function generate(viewSetting) {
            var majorMinorGridOptions,
                liveNonLiveGridOptions;
            $scope.viewSetting = viewSetting;

            pane.service.getData(viewSetting, pane.data).then(function (data) {
                majorMinorGridOptions = data[0];
                liveNonLiveGridOptions = data[1];                
                renderAll();
            });

            function renderAll() {
                var isDataReady=angular.isDefined(majorMinorGridOptions) && angular.isDefined(liveNonLiveGridOptions);
                if (!isDataReady) {
                    return;
                }

                showHideColumnBuilder.build([majorMinorGridElement, liveNonLiveGridElement], [majorMinorGridOptions, liveNonLiveGridOptions]);

                createLinksForMajorMinor(majorMinorGridOptions);
                renderMajorMinorGrid(majorMinorGridElement, majorMinorGridOptions);

                createLinksForLiveNonLive(liveNonLiveGridOptions);
                renderLiveNonLiveGrid(liveNonLiveGridElement, liveNonLiveGridOptions);

                pane.compileAll();
            }
        }

        function renderMajorMinorGrid(gridElement, data) {
            var titleInfo = {
                text: tab.generateBreadcrumbHtmlWithSuffix(" MAJOR/MINOR"),
                showCollapseExpandButton: true,
                showExcelButton: true,
                showButtonsWhenNoData: true,
                exportExcelFunction: exportExcel,
                customButtons : showHideColumnBuilder.getButtonHtml()
            };

            data.titleInfo = titleInfo;
            data.bottomBorderWidth = 3;
            gridElement.fanexGrid(data);
        }

        function renderLiveNonLiveGrid(gridElement, data) {
            var titleInfo = {
                text: tab.generateBreadcrumbHtmlWithSuffix(" LIVE/NON-LIVE"),
                showCollapseExpandButton: true,
                showExcelButton: false
            };

            data.titleInfo = titleInfo;
            data.bottomBorderWidth = 3;
            gridElement.fanexGrid(data);
        }

        function createLinksForMajorMinor(data) {
            for (var i = 0; i < data.bodyRows.length; i++) {
                var betTypeRow = data.bodyRows[i];
                var betTypeNameMajorMinor = betTypeRow.Columns[0].Value;
                betTypeRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateSoccerBetTypePane(\'' + betTypeNameMajorMinor + '\')">' + betTypeNameMajorMinor + '</a>';
            }
        }

        function generateSoccerBetTypePane(betTypeNameMajorMinor) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                betTypeNameMajorMinor: betTypeNameMajorMinor,
                sportTypeName: betTypeNameMajorMinor
            });
            tab.goForward('soccerBetTypeFull', data);
        }

        function createLinksForLiveNonLive(data) {
            for (var i = 0; i < data.bodyRows.length; i++) {
                var betTypeRow = data.bodyRows[i];
                var soccerLiveNonLive = betTypeRow.Columns[0].Value;
                betTypeRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateSoccerLiveNonLivePane(\'' + soccerLiveNonLive + '\')">' + soccerLiveNonLive + '</a>';
            }
        }

        function generateSoccerLiveNonLivePane(soccerLiveNonLive) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                soccerLiveNonLive: soccerLiveNonLive
            });
            tab.goForward('soccerMajorMinor', data);
        }

        function exportExcel() {

            var majorMinorTitle = tab.generateBreadcrumbTextWithSuffix(" MAJOR/MINOR");
            var liveNonLiveTitle = tab.generateBreadcrumbTextWithSuffix(" LIVE/NON-LIVE");

            var httpData = {
                majorMinorTitle: majorMinorTitle,
                liveNonLiveTitle: liveNonLiveTitle
            };
            angular.extend(httpData, pane.data);
            pane.service.exportExcel($scope.viewSetting, httpData, pane.getFileName());
        }
    }
})(angular, undefined);