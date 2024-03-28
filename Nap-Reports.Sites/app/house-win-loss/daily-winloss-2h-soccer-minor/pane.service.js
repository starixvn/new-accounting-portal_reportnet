import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .factory('napPane', napPane);

    napPane.$inject = ['$log', '$compile', '$injector', 'houseWinlossService', 'showHideColumnService'];
    function napPane($log, $compile, $injector, houseWinlossService, showHideColumnService) {
        function Pane(viewLevel, data) {
            this.viewLevel = viewLevel;
            this.data = data;
            this.$scope = undefined;
            this.$element = undefined;
            this.generate = generate;
            this.show = show;
            this.compileAll = compileAll;
            this.compileLinks = compileLinks;
            this.compileDrilldown = compileDrilldown;
            this.getFileName = getFileName;

            if (angular.isUndefined(this.viewLevel) || angular.isUndefined(this.data)) {
                $log.error('Invalid pane');
                return;
            }

            this.viewSetting = houseWinlossService.getViewSetting(data.productName, viewLevel, data);
            if(this.viewSetting === undefined) {
                loggerService.logError('Sorry, the configuration is corrupted, please notify the administrators.');
                throw 'Sorry, the configuration is corrupted, please notify the administrators.';
            }

            this.templateUrl = this.viewSetting.templateUrl;
            this.controller = this.viewSetting.controller;
            this.service = $injector.get(this.viewSetting.service);

            function generate() {
                this.$scope.generate(this.viewSetting);
            }

            function show() {
                var gridElement = this.$element.find('.freegrid');
                gridElement.fanexGrid('resizeGrid');
            }

            function getFileName() {
                return 'Daily_Win_Loss_2h_Soccer_Minor_Bet_Types_From_' + this.data.startDate.format('DD_MM_YYYY') + '_To_' + this.data.endDate.format('DD_MM_YYYY') + '.xls';
            }

            function compileAll() {
                this.compileLinks();
                var elements = this.$element.find('.nap-component');
                $compile(elements)(this.$scope);
            }

            function compileLinks() {
                var elements = this.$element.find('.drill-down');
                $compile(elements)(this.$scope);
                elements = this.$element.find('.breadcrumb');
                $compile(elements)(this.$scope);
            }

            function compileDrilldown() {
                var elements = this.$element.find('.drill-down');
                $compile(elements)(this.$scope);
            }
        }

        return Pane;
    }
})(angular);