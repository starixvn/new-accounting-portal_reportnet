import angular from "angular";
import moment from "moment";
(function (angular) {
    'use strict';

    angular
        .module('app.core')
        .directive('napMonthPicker', napMonthPicker);

    function napMonthPicker() {
        var directive = {
            restrict: 'EA',
            templateUrl: 'app/root/components/month-picker/month-picker.directive.html',
            scope: {
                startDate: '=',
                endDate: '=',
                maxDate: '='
            },
            replace: true,
            controller: controller,
            controllerAs: 'vm',
            bindToController: true
        };
        return directive;
    }

    controller.$inject = ['$log', '$scope', 'moment', 'config'];
    function controller($log, $scope, moment, config) {
        var vm = this;
        var minDate = moment.utc([2011, 0, 1]);
        var monthOfMinDate = minDate.month();
        var yearOfMinDate = minDate.year();
        var startOfMonthOfMinDate = moment.utc(minDate).startOf('month');
        var maxDate = moment.utc(vm.maxDate);
        var monthOfMaxDate = maxDate.month();
        var yearOfMaxDate = maxDate.year();
        var endOfMonthOfMaxDate = moment.utc(maxDate).endOf('month');
        var startDate = moment.utc(vm.startDate);
        var endDate = moment.utc(vm.endDate);
        if (startDate.year() !== endDate.year() || startDate.month() !== endDate.month()) {
            $log.error('startDate and endDate are not same month and year');
            return;
        }

        generateViewModel();

        vm.goPreviousMonth = function ($event, year, month) {
            $event.stopPropagation();
            if (vm.previousDisabled) {
                return;
            }

            vm.selectMonth(year, month);
        };

        vm.goNextMonth = function ($event, year, month) {
            $event.stopPropagation();
            if (vm.nextDisabled) {
                return;
            }

            vm.selectMonth(year, month);
        };

        vm.selectFromDay = function ($event, day) {
            $event.preventDefault();
            vm.fromDay = day;
            startDate.date(vm.fromDay);
            if (vm.toDay < vm.fromDay) {
                vm.toDay = vm.fromDay;
                endDate.date(vm.toDay);
            }
            updateModel();
        };

        vm.selectToDay = function ($event, day) {
            $event.preventDefault();
            vm.toDay = day;
            endDate.date(vm.toDay);
            if (vm.toDay < vm.fromDay) {
                vm.fromDay = vm.toDay;
                startDate.date(vm.fromDay);
            }
            updateModel();
        };

        vm.selectYear = function ($event, year) {
            $event.stopPropagation();
            angular.forEach(vm.years, function (item) {
                item.expand = item.value === year;
            });
        };

        vm.selectMonth = function (year, month) {
            var fromDay = year === yearOfMinDate && month === monthOfMinDate ? minDate.date() : 1;
            startDate = moment.utc([year, month, fromDay]);
            var toDay = year === yearOfMaxDate && month === monthOfMaxDate ? maxDate.date() : startDate.daysInMonth();
            endDate = moment.utc([year, month, toDay]);
            generateViewModel();
            updateModel();
        };

        $scope.$watch('vm.startDate', function () {
            startDate = moment.utc(vm.startDate);
            generateViewModel();
        });
        $scope.$watch('vm.endDate', function () {
            endDate = moment.utc(vm.endDate);
            generateViewModel();
        });

        function generateViewModel() {
            var previousMonth = moment(startDate).subtract(1, 'months').startOf('month');
            var nextMonth = moment(startDate).add(1, 'months').endOf('month');
            vm.year = startDate.year();
            vm.month = startDate.month();
            vm.text = startDate.format('MMM YYYY');
            vm.years = generateYears();
            vm.fromDay = startDate.date();
            vm.toDay = endDate.date();
            vm.daysOfMonth = generateDaysOfMonth();

            vm.previousDisabled = previousMonth.isBefore(startOfMonthOfMinDate);
            if (!vm.previousDisabled) {
                vm.previousMonth = previousMonth.month();
                vm.previousYear = previousMonth.year();
            }
            vm.nextDisabled = nextMonth.isAfter(endOfMonthOfMaxDate);
            if (!vm.nextDisabled) {
                vm.nextMonth = nextMonth.month();
                vm.nextYear = nextMonth.year();
            }

            function generateYears() {
                var monthFullNames = moment.months();
                var years = [];
                for (var yearNumber = yearOfMinDate; yearNumber <= yearOfMaxDate; yearNumber++) {
                    var months = [];
                    var minMonth = yearNumber === yearOfMinDate ? monthOfMinDate : 0;
                    var maxMonth = yearNumber === yearOfMaxDate ? monthOfMaxDate : 11;
                    for (var monthNumber = minMonth; monthNumber <= maxMonth; monthNumber++) {
                        months.push({
                            value: monthNumber,
                            text: monthFullNames[monthNumber]
                        });
                    }
                    years.push({
                        value: yearNumber,
                        expand: yearNumber === vm.year,
                        months: months
                    });
                }

                return years;
            }

            function generateDaysOfMonth() {
                var daysOfMonth = [];
                var startDay = startDate.month() === minDate.month() && startDate.year() === minDate.year() ? minDate.date() : 1;
                var endDay = endDate.month() === maxDate.month() && endDate.year() === maxDate.year() ? maxDate.date() : moment(endDate).endOf('month').date();
                for (var dayNumber = startDay; dayNumber <= endDay; dayNumber++) {
                    daysOfMonth.push(dayNumber);
                }

                return daysOfMonth;
            }
        }

        function updateModel() {
            vm.startDate = moment.utc(startDate);
            vm.endDate = moment.utc(endDate);
        }
    }
})(angular);