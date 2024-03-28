import angular from "angular";
import moment from "moment";
(function (angular) {
    'use strict';

    angular
        .module('app.core')
        .directive('napHouseWinLossDateRangePicker', napHouseWinLossDateRangePicker);

    function napHouseWinLossDateRangePicker() {
        var directive = {
            restrict: 'EA',
            templateUrl: 'app/root/components/house-win-loss-date-range-picker/date-range-picker.directive.html',
            scope: {
                ngModel: '=',
                minDate: '=',
                maxDate: '=',
                startDate: '=',
                endDate: '='
            },
            replace: true,
            controller: controller,
            controllerAs: 'vm',
            bindToController: true
        };
        return directive;
    }

    controller.$inject = ['$scope', '$element', '$document', 'moment', 'config'];
    function controller($scope, $element, $document, moment, config) {

        var vm = this;
        var timePeriodConfiguration = getTimePeriodConfiguration(config);

        vm.timePeriod = "today";
        vm.changeTimePeriod = changeTimePeriod;
        vm.changeTimePeriod();

        $scope.$on('nap-filter-switch-history', function (event, data) {
            vm.timePeriod = data;
        });

        $scope.$on('nap-date-picker-select', function (event, data) {

            setDateRangeContrains(data);
            vm.timePeriod = "specific-period";
            if (vm.startDate.isAfter(vm.maxDateFrom)) {
                vm.startDate = data.selectedDate;
            }

            if (vm.startDate.isAfter(vm.endDate)) {
                vm.endDate = data.startDate;
            }
        });

        $scope.$watch('vm.startDate', function () {
            vm.datefrom = moment.utc(vm.startDate);
        });
        $scope.$watch('vm.endDate', function () {
            vm.dateto = moment.utc(vm.endDate);
        });

        function changeTimePeriod() {

            if (vm.timePeriod === 'specific-period') {
                changeSpecificTimePeriod();
            }
            else {
                changePredefinedTimePeriod();
            }

            function changeSpecificTimePeriod() {
                vm.minDateFrom = vm.minDate;
                vm.maxDateFrom = vm.maxDate;

                vm.minDateTo = vm.minDate;
                vm.maxDateTo = vm.maxDate;
            }

            function changePredefinedTimePeriod() {
                var configuration = timePeriodConfiguration[vm.timePeriod];
                vm.datefrom = configuration.datefrom;
                vm.dateto = configuration.dateto;

                vm.minDateFrom = vm.minDate;
                vm.maxDateFrom = vm.dateto;

                vm.minDateTo = vm.minDate;
                vm.maxDateTo = vm.maxDate;

                vm.startDate = vm.datefrom;
                vm.endDate = vm.dateto;
            }
        }

        function getTimePeriodConfiguration(config) {
            var today = config.today;
            var todayConfig = {
                datefrom: moment.utc(today),
                dateto: moment.utc(today)
            };

            var yesterday = config.yesterday;
            var yesterdayConfig = {
                datefrom: moment.utc(yesterday),
                dateto: moment.utc(yesterday)
            };
            var currentWeekConfig = {
                datefrom: moment.utc(today).startOf('isoWeek'),
                dateto: moment.utc(today)
            };
            var lastWeekConfig = {
                datefrom: moment.utc(today).subtract(7, 'days').startOf('isoWeek'),
                dateto: moment.utc(today).subtract(7, 'days').endOf('isoWeek').subtract({ 'hours': 1 })
            };
            var currentMonthConfig = {
                datefrom: moment.utc(today).startOf('month'),
                dateto: moment.utc(today)
            };
            var lastMonthConfig = {
                datefrom: moment.utc(today).subtract(1, 'months').startOf('month'),
                dateto: moment.utc(today).subtract(1, 'months').endOf('month').subtract({ 'hours': 1 })
            };
            var specificDateConfig = todayConfig;

            return {
                "today": todayConfig,
                "yesterday": yesterdayConfig,
                "current-week": currentWeekConfig,
                "last-week": lastWeekConfig,
                "current-month": currentMonthConfig,
                "last-month": lastMonthConfig,
                "specific-period": specificDateConfig,

            };
        }

        function setDateRangeContrains(data) {
            switch (data.name) {
                case "from":
                    vm.minDateTo = data.selectedDate;
                    vm.startDate = data.selectedDate;
                    vm.datefrom = data.selectedDate;
                    break;
                case "to":
                    vm.maxDateFrom = data.selectedDate;
                    vm.endDate = data.selectedDate;
                    vm.dateto = data.selectedDate;
                    break;
                default:
                    break;
            }
        }
    }

})(angular);