import angular from "angular";
import moment from "moment";
(function (angular) {
    'use strict';

    angular
        .module('app.core')
        .directive('napDateRangePicker', napDateRangePicker);

    function napDateRangePicker() {
        var directive = {
            restrict: 'EA',
            templateUrl: 'app/root/components/date-range-picker/date-range-picker.directive.html',
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

        vm.timePeriod = "yesterday";
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
            var yesterday = config.yesterday;
            var yesterdayConfig = {
                datefrom: moment.utc(yesterday),
                dateto: moment.utc(yesterday)
            };
            var currentWeekConfig = {
                datefrom: moment.utc(yesterday).startOf('week'),
                dateto: moment.utc(yesterday)
            };
            var lastWeekConfig = {
                datefrom: moment.utc(yesterday).subtract(7, 'days').startOf('week'),
                dateto: moment.utc(yesterday).subtract(7, 'days').endOf('week').subtract({ 'hours': 1 })
            };
            var currentMonthConfig = {
                datefrom: moment.utc(yesterday).startOf('month'),
                dateto: moment.utc(yesterday)
            };
            var lastMonthConfig = {
                datefrom: moment.utc(yesterday).subtract(1, 'months').startOf('month'),
                dateto: moment.utc(yesterday).subtract(1, 'months').endOf('month').subtract({ 'hours': 1 })
            };
            var sinceLastMonthConfig = {
                datefrom: moment.utc(yesterday).subtract(1, 'months').startOf('month'),
                dateto: moment.utc(yesterday)
            };
            var specificDateConfig = yesterdayConfig;
            var last3MonthsConfig = {
                datefrom: moment.utc(yesterday).subtract(3, 'months').startOf('month'),
                dateto: moment.utc(yesterday).subtract(1, 'months').endOf('month').subtract({ 'hours': 1 })
            };
            var last6MonthsConfig = {
                datefrom: moment.utc(yesterday).subtract(6, 'months').startOf('month'),
                dateto: moment.utc(yesterday).subtract(1, 'months').endOf('month').subtract({ 'hours': 1 })
            };
            var last1YearConfig = {
                datefrom: moment.utc(yesterday).subtract(1, 'years').startOf('month'),
                dateto: moment.utc(yesterday).subtract(1, 'months').endOf('month').subtract({ 'hours': 1 })
            };

            return {
                "yesterday": yesterdayConfig,
                "current-week": currentWeekConfig,
                "last-week": lastWeekConfig,
                "current-month": currentMonthConfig,
                "last-month": lastMonthConfig,
                "since-last-month": sinceLastMonthConfig,
                "specific-period": specificDateConfig,
                "last-3-months": last3MonthsConfig,
                "last-6-months": last6MonthsConfig,
                "last-1-year": last1YearConfig

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