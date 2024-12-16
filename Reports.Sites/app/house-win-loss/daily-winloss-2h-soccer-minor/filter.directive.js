import angular from "angular";
import moment from "moment";
(function (angular) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .directive('napFilter', napFilter);

    function napFilter() {
        var directive = {
            restrict: 'EA',
            scope: {
            },
            templateUrl: 'app/house-win-loss/filter/filter.directive.html',
            replace: true,
            controller: controller,
            controllerAs: 'vm',
            bindToController: true
        };
        return directive;
    }

    controller.$inject = ['$scope', '$rootScope', 'config', 'moment'];
    function controller($scope, $rootScope, config, moment) {
        /*jshint validthis: true */
        var vm = this;
        vm.minDate= moment("20110101", "YYYYMMDD");
        vm.currentLevel = 0;
        vm.accessDenied = modePermissions.length === 0 || productPermissions.length === 0;
        vm.normalMode = modePermissions.indexOf("HouseWinLossByProducts_NormalMode") >= 0;
        vm.showBothMode = modePermissions.indexOf("HouseWinLossByProducts_NormalMode") >= 0 && modePermissions.indexOf("HouseWinLossByProducts_HistoryMode") >= 0;
        vm.today = config.today;
        vm.yesterday = config.yesterday;
        vm.date = config.today;
        vm.startDate = moment.utc(config.yesterday).startOf('month');
        vm.endDate = moment.utc(config.yesterday);
        vm.generate = generate;
        vm.todayClicked = todayClicked;
        vm.yesterdayClicked = yesterdayClicked;
        vm.viewNormalMode = viewNormalMode;
        vm.viewHistoryMode = viewHistoryMode;
        vm.getDateRange = getDateRange;
        vm.updateDateRange = updateDateRange;
        vm.dataMenuId = 370;
        vm.legentTitle = 'Daily Win Loss (2h) Soccer Minor Bet Types Legend';

        $scope.$on('nap-filter-date', function (event, data) {
            updateDateRange(data);
        });

        function generate() {
            vm.dateRange = getDateRange();
            $rootScope.$broadcast('nap-filter-generate', vm.dateRange);
        }

        function todayClicked() {
            vm.date = vm.today;
            vm.generate();
        }

        function yesterdayClicked() {
            vm.date = vm.yesterday;
            vm.generate();
        }

        function viewNormalMode() {
            vm.normalMode = true;

            $('.filter-note').css('display', 'block');
            $rootScope.$broadcast('nap-filter-switch');
        }

        function viewHistoryMode() {
            vm.normalMode = false;

            $('.filter-note').css('display', 'none');
            $rootScope.$broadcast('nap-filter-switch');
        }

        function getDateRange() {
            if (vm.normalMode) {
                return {
                    normalMode: vm.normalMode,
                    startDate: vm.date,
                    endDate: vm.date,
                    permissionProfileID: 1
                };
            } else {
                return {
                    normalMode: vm.normalMode,
                    startDate: vm.startDate,
                    endDate: vm.endDate,
                    permissionProfileID: 1
                };
            }
        }

        function updateDateRange(dateRange) {
            vm.normalMode = dateRange.normalMode;
            if (vm.normalMode) {
                vm.date = dateRange.startDate;
            } else {
                vm.startDate = dateRange.startDate;
                vm.endDate = dateRange.endDate;
            }
        }
    }
})(angular);