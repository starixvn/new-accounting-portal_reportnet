import angular from "angular";
import moment from "moment";
(function (angular) {
    'use strict';

    angular
        .module('app.core')
        .directive('napDatePicker', napDatePicker);

    function napDatePicker() {
        var directive = {
            restrict: 'EA',
            templateUrl: 'app/root/components/date-picker/date-picker.directive.html',
            scope: {
                ngModel: '=',
                minDate: '=',
                maxDate: '=',
                pickerName: '@pickerName'
            },
            replace: true,
            controller: controller,
            controllerAs: 'vm',
            bindToController: true
        };
        return directive;
    }

    controller.$inject = ['$scope', '$rootScope', '$element', '$document', 'moment'];
    function controller($scope, $rootScope, $element, $document, moment) {
        var vm = this;
        var selectedDate;
        var startOfMonthOfMinDate = moment.utc(vm.minDate).startOf('month');
        var endOfMonthOfMaxDate = moment.utc(vm.maxDate).endOf('month');

        vm.openCalendar = function () {
            vm.isOpen = true;
            $document.unbind('click', documentClickBind);
            $document.bind('click', documentClickBind);
        };

        vm.goPreviousMonth = function () {
            if (vm.previousDisabled) {
                return;
            }
            selectedDate = vm.previousMonth;
            correctSelectedDate();
            generateViewModel();
        };

        vm.goNextMonth = function () {
            if (vm.nextDisabled) {
                return;
            }

            selectedDate = vm.nextMonth;
            correctSelectedDate();
            generateViewModel();
        };

        vm.changeMonth = function () {
            selectedDate.month(vm.month.value);
            correctSelectedDate();
            generateViewModel();
        };

        vm.changeYear = function () {
            selectedDate.year(vm.year);
            correctSelectedDate();
            generateViewModel();
        };

        vm.changeDate = function (date) {
            if (date.disabled) {
                return;
            }

            selectedDate = date.value;
            generateViewModel();
            vm.text = selectedDate.format('MMM DD, YYYY');
            vm.ngModel = moment.utc(selectedDate);
            $rootScope.$broadcast('nap-date-picker-select', { selectedDate: selectedDate, name: vm.pickerName });
        };

        $scope.$on('$destroy', function () {
            $document.unbind('click', documentClickBind);
        });

        $scope.$watchGroup(['vm.ngModel', 'vm.maxDate', 'vm.minDate'], function () {
            init();
        });

        function init() {
            selectedDate = moment(vm.ngModel);
            vm.isOpen = false;
            vm.text = selectedDate.format('MMM DD, YYYY');
            generateViewModel();
        }

        function correctSelectedDate() {
            if (selectedDate.isBefore(vm.minDate)) {
                selectedDate = moment(vm.minDate);
            } else if (selectedDate.isAfter(vm.maxDate)) {
                selectedDate = moment(vm.maxDate);
            }
        }

        function generateViewModel() {
            var selectedMonth = selectedDate.month();
            var selectedYear = selectedDate.year();
            var startOfMonth = moment.utc(selectedDate).startOf('month');
            var endOfMonth = moment.utc(selectedDate).endOf('month');
            var startDate = moment.utc(startOfMonth).subtract(Math.abs(startOfMonth.weekday()), 'days');

            startOfMonthOfMinDate = moment.utc(vm.minDate).startOf('month');
            endOfMonthOfMaxDate = moment.utc(vm.maxDate).endOf('month');

            vm.monthOptions = generateMonthOptions();
            vm.month = vm.monthOptions[0];
            angular.forEach(vm.monthOptions, function (monthOption) {
                if (monthOption.value === selectedMonth) {
                    vm.month = monthOption;
                }
            });
            vm.yearOptions = generateYearOptions();
            vm.year = selectedDate.year();
            vm.dayHeaders = vm.dayHeaders || generateDayHeaders();
            vm.weeks = generateWeeks();

            var previousMonth = moment(selectedDate).subtract(1, 'months');
            vm.previousDisabled = previousMonth.isBefore(startOfMonthOfMinDate);
            if (!vm.previousDisabled) {
                vm.previousMonth = previousMonth;
            }

            var nextMonth = moment(selectedDate).add(1, 'months');
            vm.nextDisabled = nextMonth.isAfter(endOfMonthOfMaxDate);
            if (!vm.nextDisabled) {
                vm.nextMonth = nextMonth;
            }

            function generateMonthOptions() {
                var monthShortNames = moment.monthsShort();
                var monthOptions = [];
                var date = moment(selectedDate);
                for (var monthNumber = 0; monthNumber < 12; monthNumber++) {
                    date.month(monthNumber);
                    if (date.isBefore(startOfMonthOfMinDate) || date.isAfter(endOfMonthOfMaxDate)) {
                        continue;
                    }

                    monthOptions.push({
                        name: monthShortNames[monthNumber],
                        value: monthNumber
                    });
                }

                return monthOptions;
            }

            function generateYearOptions() {

                var yearOptions = [];
                var beginYear = vm.minDate.year();
                var endYear = vm.maxDate.year();
                for (var yearNumber = beginYear; yearNumber <= endYear; yearNumber++) {
                    yearOptions.push(yearNumber);
                }

                return yearOptions;
            }

            function generateDayHeaders() {
                var dayShortNames = moment.weekdaysMin();
                var dayFullNames = moment.weekdays();
                var dayHeaders = [];
                for (var dayNumber = 0; dayNumber < 7; dayNumber++) {
                    var shortName = dayShortNames[dayNumber];
                    var fullName = dayFullNames[dayNumber];
                    var weekend = dayNumber === 0 || dayNumber === 6;
                    dayHeaders.push({
                        name: shortName,
                        title: fullName,
                        weekend: weekend
                    });
                }

                return dayHeaders;
            }

            function generateWeeks() {
                var weeks = [];
                for (var i = 0; i < 6; i++) {
                    var week = {
                        dates: []
                    };
                    for (var j = 0; j < 7; j++) {
                        var monthMoment = moment.utc(startDate).add(i * 7 + j, 'days');
                        var date = {
                            value: monthMoment,
                            text: monthMoment.format('D'),
                            active: monthMoment.format('YYYY-MMM-DD') === selectedDate.format('YYYY-MMM-DD'),
                            past: monthMoment.isBefore(startOfMonth),
                            future: monthMoment.isAfter(endOfMonth),
                            disabled: monthMoment.isBefore(vm.minDate) || monthMoment.isAfter(vm.maxDate)
                        };
                        week.dates.push(date);
                    }
                    weeks.push(week);
                }

                return weeks;
            }
        }

        function documentClickBind(event) {
            if (!vm.isOpen) {
                $document.unbind('click', documentClickBind);
                return;
            }

            var dpContainsTarget = $element[0].contains(event.target);
            if (vm.isOpen && !dpContainsTarget) {
                $scope.$apply(function () {
                    vm.isOpen = false;
                });
            }
        }
    }
})(angular);