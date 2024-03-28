import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.cn88WinlossApp')
        .controller('NGSportTypeController', NGSportTypeController);

    NGSportTypeController.$inject = ['$injector', '$scope', '$element', 'fanexGridService', 'showHideColumnService'];
    function NGSportTypeController($injector, $scope, $element, fanexGridService, showHideColumnService) {
        var pane = $scope.pane;
        var showHideColumnBuilder = new showHideColumnService(pane);
        var tab = $scope.tab;
        var gridElement = $element.find('.freegrid');
        $scope.show = showHideColumnBuilder.toggle;
        $scope.generate = generate;
        $scope.generateLeague = generateLeague;
        $scope.viewSetting = {};

        function generate(viewSetting) {
            $scope.viewSetting = viewSetting;
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
                var sportTypeId = sportTypeRow.Columns[0].CustomAttr;
                var sportTypeName = sportTypeRow.Columns[0].Value;
                sportTypeRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateLeague(' + sportTypeId + ',\'' + sportTypeName.replace(/'/g, "\\'") + '\')">' + sportTypeName + '</a>';
            }
        }
       
        function generateLeague(sportType, sportTypeName) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                sportType: sportType,
                sportTypeName: sportTypeName,
            });
            tab.goForward('league', data);
        }

        function exportExcel() {
            pane.service.exportExcelForSingleGrid($scope.viewSetting, tab, pane);
        }
    }
})(angular);