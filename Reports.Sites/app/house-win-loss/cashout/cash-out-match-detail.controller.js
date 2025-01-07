import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .controller('CashOutMatchDetailController', CashOutMatchDetailController);

    CashOutMatchDetailController.$inject = ['$scope', '$element', 'fanexGridService'];
    function CashOutMatchDetailController($scope, $element, fanexGridService) {
        var pane = $scope.pane;
        var tab = $scope.tab;
        var gridElement = $element.find('.freegrid');
        $scope.generate = generate;
        $scope.viewSetting = {};

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
    }
})(angular);