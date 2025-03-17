import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .controller('BetBuilderMatchDetailController', BetBuilderMatchDetailController);

    BetBuilderMatchDetailController.$inject = ['$scope', '$element', 'fanexGridService'];
    function BetBuilderMatchDetailController($scope, $element, fanexGridService) {
        var pane = $scope.pane;
        var tab = $scope.tab;
        var gridElement = $element.find('.freegrid');
        $scope.generate = generate;
        $scope.viewSetting = {};
        $scope.sortData = sortData;

        function generate(viewSetting) {
            $scope.viewSetting = viewSetting;
            pane.service.getData(viewSetting, pane.data).then(function (data) {
                renderGrid(data);
                pane.compileAll();
            });
        }

        function renderGrid(data) {
            var titleInfo = {
                text: tab.generateBreadcrumbHtml(),
                showExcelButton: true,
                exportExcelFunction: exportExcel
            };

            data.titleInfo = titleInfo;
            data.sortOption = {
                serverSortFunction:
                  function (sortColumnName, sortDirection) {
                      sortData(sortColumnName, sortDirection);
                  }
            };
            data.showPager = true;
            data.pagerOption = {
                itemsPerPage: 50,
                currentPage: 0,
                itemsPerPageArray: [50, 100, 200, 500],
                totalItem: data.bodyRows.length,
                afterPagedFunction: function () {
                    pane.compileDrilldown();
                }
            };
            data.gridExpandHeight = fanexGridService.calculateExpandHeight(gridElement, true);
            data.bottomBorderWidth = 3;
            gridElement.fanexGrid(data);
        }

        function exportExcel() {
            var title = tab.generateBreadcrumbText();
            var httpData = {};
            angular.extend(httpData, pane.data);
            angular.extend(httpData, { title: title });

            pane.service.exportExcel($scope.viewSetting, httpData, pane.getFileName());
        }

        function sortData(sortColumnName, sortDirection) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                sortColumnName: sortColumnName,
                sortDirection: sortDirection
            });

            pane.data = data;
            pane.service.getData($scope.viewSetting, data).then(function (newData) {                
                renderGrid(newData);
                pane.compileAll();
            });
        }
    }
})(angular);