import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.cn88WinlossApp')
        .controller('BetTypeNewCricketController', BetTypeNewCricketController);

    BetTypeNewCricketController.$inject = ['$scope', '$controller', '$element', 'cn88WinlossService', 'fanexGridService', 'showHideColumnService'];
    function BetTypeNewCricketController($scope, $controller, $element, cn88WinlossService, fanexGridService, showHideColumnService) {

        angular.extend(this, $controller('BetTypeController', {
            $scope: $scope,
            $element: $element,
            cn88WinlossService: cn88WinlossService,
            fanexGridService: fanexGridService,
            showHideColumnService: showHideColumnService
        }));

        var pane = $scope.pane;
        var gridElement = $element.find('.freegrid');
        var showHideColumnBuilder = new showHideColumnService(pane);
        $scope.show = showHideColumnBuilder.toggle;
        $scope.generate = generate;

        function generate(viewSetting) {
            $scope.viewSetting = viewSetting;
            pane.service.getData(viewSetting, pane.data).then(function (data) {

                showHideColumnBuilder.build(gridElement, data);

                var canViewMatchDetail = pane.data.endDate.diff(pane.data.startDate, 'days') <= 3;
                if (canViewMatchDetail) {
                    $scope.createDrilldownLinks(data.bodyRows, pane.data.isLive);
                    createLinkChargeCom(data.footerRows);
                }

                $scope.renderGrid(data);
                pane.compileAll();
            });
        }

        function createLinkChargeCom(rows) {
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].Columns[0].Value === "Charge Commission") {
                    $scope.createDrilldownLink(rows[i]);
                }
            }
        }
    }
})(angular);