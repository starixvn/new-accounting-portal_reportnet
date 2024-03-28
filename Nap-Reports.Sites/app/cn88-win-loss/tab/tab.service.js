//import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.cn88WinlossApp')
        .factory('napTab', napTab);

    napTab.$inject = ['$rootScope', 'breadcrumbService', 'napPane', 'titleService'];
    function napTab($rootScope, breadcrumbService, Pane, titleService) {
        function Tab(panes) {
            this.panes = angular.isDefined(panes) ? panes : [];
            this.addPane = addPane;
            this.getActivePane = getActivePane;
            this.getPreviousPane = getPreviousPane;
            this.goBack = goBack;
            this.goForward = goForward;
            this.generateBreadcrumbList = generateBreadcrumbList;
            this.generateBreadcrumbListWithModifiedLastItem = generateBreadcrumbListWithModifiedLastItem;
            this.generateBreadcrumbHtml = generateBreadcrumbHtml;
            this.generateBreadcrumbText = generateBreadcrumbText;
            this.generateBreadcrumbTextWithSuffix = generateBreadcrumbTextWithSuffix;
            this.generateBreadcrumbHtmlWithSuffix = generateBreadcrumbHtmlWithSuffix;
            

            function addPane(pane) {
                this.panes.push(pane);
            }

            function getActivePane() {
                return this.panes[this.panes.length - 1];
            }

            function getPreviousPane(pane) {
                var previousPane = null;
                for (var i = 0; i < this.panes.length; i++) {
                    var currentPane = this.panes[i];
                    if (currentPane === pane) {
                        break;
                    }

                    previousPane = currentPane;
                }

                return previousPane;
            }

            function goBack(index) {
                this.panes = this.panes.slice(0, index + 1);
                var newActivePane = this.getActivePane();
                if (angular.isFunction(newActivePane.$scope.show)) {
                    newActivePane.$scope.show();
                }

                var activePane = this.panes[index];
                var dateRange = {
                    normalMode: activePane.data.normalMode,
                    startDate: activePane.data.startDate,
                    endDate: activePane.data.endDate
                };
                $rootScope.$broadcast('nap-filter-date', dateRange);
            }

            function goForward(level, data) {
                var pane = new Pane(level, data);
                this.addPane(pane);
            }

            function generateBreadcrumbListWithModifiedLastItem() {
                var list = [];
                for (var i = 0; i < this.panes.length; i++) {
                    var pane = this.panes[i];
                    var isLast = i === this.panes.length - 1;
                    var getTitleFunction = pane.viewSetting.getTitleFunction;
                    var title = titleService[getTitleFunction](pane.data, isLast);
                    list.push(title);
                }

                return list;
            }

            function generateBreadcrumbList() {
                var list = [];
                for (var i = 0; i < this.panes.length; i++) {
                    var pane = this.panes[i];
                    var getTitleFunction = pane.viewSetting.getTitleFunction;
                    var title = titleService[getTitleFunction](pane.data, false);
                    list.push(title);
                }

                return list;
            }

            function generateBreadcrumbHtml() {
                var list = this.generateBreadcrumbListWithModifiedLastItem();
                return breadcrumbService.generateHtml(list);
            }

            function generateBreadcrumbText() {
                var list = this.generateBreadcrumbListWithModifiedLastItem();
                return breadcrumbService.generateText(list);
            }

            function generateBreadcrumbHtmlWithSuffix(suffix) {
                var list = this.generateBreadcrumbList();
                list[list.length - 1] = list[list.length - 1] + suffix;
                return breadcrumbService.generateHtml(list);
            }

            function generateBreadcrumbTextWithSuffix(suffix) {
                var list = this.generateBreadcrumbList();
                list[list.length - 1] = list[list.length - 1] + suffix;
                return breadcrumbService.generateText(list);
            }
        }

        return Tab;
    }
})(angular);