import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .directive('napTab', napTab);

    napTab.$inject = ['napPane'];
    function napTab(Pane) {
        var directive = {
            restrict: 'EA',
            scope: {
                panes: '='
            },
            templateUrl: 'app/house-win-loss/tab/tab.directive.html',
            controller: controller,
            controllerAs: 'vm',
            bindToController: true
        };
        return directive;
    }

    controller.$inject = ['$scope', '$rootScope', 'napTab', 'napPane'];
    function controller($scope, $rootScope, Tab, Pane) {
        var tab = new Tab($scope.panes);
        var vm = this;
        vm.tab = tab;

        $scope.$on('nap-filter-generate', function (event, data) {
            if (tab.panes.length === 0) {
                var pane = getMainPane(data);
                tab.addPane(pane);
            } else {
                var activePane = tab.getActivePane();
                angular.extend(activePane.data, data);
                activePane.generate();
            }
        });

        $scope.$on('nap-filter-switch', function (event) {
            tab.panes = [];
        });

        function getMainPane(data) {
            var hasMainPagePermission = productPermissions.indexOf("HouseWinLossByProducts_AllProducts") >= 0
                            || productPermissions.indexOf("HouseWinLossByProducts_Sportsbook") >= 0
                            || productPermissions.indexOf("HouseWinLossByProducts_ThirdPartyProducts") >= 0
                            || productPermissions.indexOf("HouseWinLossByProducts_Soccer") >= 0
                            || productPermissions.indexOf("HouseWinLossByProducts_OtherSports") >= 0;
            var hasOnlySoccerLivePermission = productPermissions.indexOf("HouseWinLossByProducts_SoccerLive") >= 0;
            var hasOnlySoccerNonLivePermission = productPermissions.indexOf("HouseWinLossByProducts_SoccerNonLive") >= 0;

            if (hasMainPagePermission) {
                data.productName = "Main";
                return new Pane('product', data);
            }

            if (hasOnlySoccerLivePermission) {
                data.productName = "Soccer";
                data.isLive = 1;
                data.isSabaSport = 2;
                data.soccerLiveNonLive = "Soccer Live";
                return new Pane('soccerMajorMinor', data);
            }

            if (hasOnlySoccerNonLivePermission) {
                data.productName = "Soccer";
                data.isLive = 0;
                data.isSabaSport = 2;
                data.soccerLiveNonLive = "Soccer Non-Live";
                return new Pane('soccerMajorMinor', data);
            }

            data.productName = "Main";
            return new Pane('product', data);
        }
    }
})(angular);