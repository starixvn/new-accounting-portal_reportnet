import angular from "angular";
import moment from "moment";
(function (angular) {
    'use strict';

    angular
        .module('app.core')
        .directive('napCn88DateRangePicker', napCn88DateRangePicker);

    function napCn88DateRangePicker() {
        var directive = {
            restrict: 'EA',
            templateUrl: 'app/root/components/cn-88-date-range-picker/date-range-picker.directive.html',
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
            var currentMonthConfig = {
                datefrom: moment.utc(yesterday).startOf('month'),
                dateto: moment.utc(yesterday)
            };
            var lastMonthConfig = {
                datefrom: moment.utc(yesterday).subtract(1, 'months').startOf('month'),
                dateto: moment.utc(yesterday).subtract(1, 'months').endOf('month').subtract({ 'hours': 1 })
            };
            var specificDateConfig = yesterdayConfig;

            return {
                "yesterday": yesterdayConfig,
                "current-month": currentMonthConfig,
                "last-month": lastMonthConfig,
                "specific-period": specificDateConfig
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