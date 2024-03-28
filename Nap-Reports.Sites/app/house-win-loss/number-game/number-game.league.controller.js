import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .controller('NGLeagueController', NGLeagueController);

    NGLeagueController.$inject = ['$injector', '$scope', '$element', 'fanexGridService', 'showHideColumnService'];
    function NGLeagueController($injector, $scope, $element, fanexGridService, showHideColumnService) {
        var pane = $scope.pane;
        var showHideColumnBuilder = new showHideColumnService(pane);
        var tab = $scope.tab;
        var gridElement = $element.find('.freegrid');
        $scope.show = showHideColumnBuilder.toggle;
        $scope.generate = generate;
        $scope.generateBetType = generateBetType;
        $scope.viewSetting = {};
        $scope.sportType = 0;
        function generate(viewSetting) {
            $scope.viewSetting = viewSetting;
            $scope.sportType = pane.data.sportType;
            pane.service.getData(viewSetting, pane.data).then(function (data) {
                showHideColumnBuilder.build(gridElement, data);
                createLinks(data);
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

        function createLinks(data) {
            for (var i = 0; i < data.bodyRows.length; i++) {
                var sportTypeRow = data.bodyRows[i];
                var leagueId = sportTypeRow.Columns[0].CustomAttr;
                var leagueName = sportTypeRow.Columns[0].Value;
                sportTypeRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateBetType(' + leagueId + ',\'' + leagueName.replace(/'/g, "\\'") + '\')">' + leagueName + '</a>';
            }
        }

        function generateBetType(leagueId, leagueName) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                leagueId: leagueId,
                leagueName: leagueName,
                sportType: $scope.sportType
            });
            tab.goForward('betType', data);
        }
      

        function exportExcel() {
            pane.service.exportExcelForSingleGrid($scope.viewSetting, tab, pane);
        }
    }
})(angular);