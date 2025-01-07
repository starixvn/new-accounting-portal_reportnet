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

    controller.$inject = ['$scope', '$rootScope', 'config', 'moment', 'ngDialog'];
    function controller($scope, $rootScope, config, moment, ngDialog) {
        /*jshint validthis: true */
        var vm = this;
        vm.minDate= moment("20110101", "YYYYMMDD");
        vm.currentLevel = 0;
        vm.accessDenied = modePermissions.length === 0 || productPermissions.length === 0;
        vm.normalMode = modePermissions.indexOf("HouseWLByProducts_NormalMode") >= 0;
        vm.showBothMode = modePermissions.indexOf("HouseWLByProducts_NormalMode") >= 0 && modePermissions.indexOf("HouseWLByProducts_HistoryMode") >= 0;
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
        vm.dataMenuId = 487;
        vm.legentTitle = 'House Win Loss Legend';
        vm.turnover = false;
        vm.permissionProfileID = parseInt(permissionProfileID);

        $scope.$on('nap-filter-date', function (event, data) {
            updateDateRange(data);
        });

        function generate() {
            vm.dateRange = getDateRange();
            if (validDateRange(vm.dateRange.startDate, vm.dateRange.endDate)) {
                $rootScope.$broadcast('nap-filter-generate', vm.dateRange);
            }
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
                    isNoDraw: parseInt(vm.turnover) === 1,
                    permissionProfileID: vm.permissionProfileID
                };
            } else {
                return {
                    normalMode: vm.normalMode,
                    startDate: vm.startDate,
                    endDate: vm.endDate,
                    isNoDraw: parseInt(vm.turnover) === 1,
                    permissionProfileID: vm.permissionProfileID
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

        function validDateRange(fromDate, toDate) {
            var dateDiff = toDate.diff(fromDate, 'days');

            if (dateDiff < 0) {
                var contentMsg = "From Date must be less than or equal to To Date!";
                showErrorMsg(contentMsg);
                return false;
            }
            else {
                var isValidRange = dateDiff <= 40;

                if (isValidRange) {
                    return true;
                }
                else {
                    var contentMsg = "Date range should not exceed 40 days!";
                    showErrorMsg(contentMsg);
                    return false;
                }
            }
        }

        function showErrorMsg(msg) {
            ngDialog.open({
                template: '<div class="errorMsg"><p>' + msg + '</p></div>',
                plain: true,
                closeByDocument: true,
                width: 295,
                overlay: false,
                className: 'ngdialog-theme-plain'
            });
        }
    }
})(angular);