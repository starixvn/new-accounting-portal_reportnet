import angular from "angular";
import moment from "moment";
(function (angular) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .controller('ParlayBetTypeController', ParlayBetTypeController);

    ParlayBetTypeController.$inject = ['$scope', '$element', 'fanexGridService', 'showHideColumnService', 'legacyService'];
    function ParlayBetTypeController($scope, $element, fanexGridService, showHideColumnService, legacyService) {
        var pane = $scope.pane;
        var showHideColumnBuilder = new showHideColumnService(pane);
        var tab = $scope.tab;
        var parlaySportType = 9;
        var systemParlayBetType = 29;
        var mixParlayBetType = 9;
        var gridElement = $element.find('.freegrid');
        $scope.show = showHideColumnBuilder.toggle;
        $scope.generate = generate;
        $scope.generateSite = generateSite;
        $scope.viewMixParlayWindow = viewMixParlayWindow;
        $scope.viewSetting = {};

        function generate(viewSetting) {
            $scope.viewSetting = viewSetting;
            pane.service.getData(viewSetting, pane.data).then(function (data) {
                showHideColumnBuilder.build(gridElement, data);
                createLinks(data);
                renderGrid(data);
                pane.compileAll();
            });
        }

        function generateSite(betType, betTypeName, site) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                betType: betType,
                betTypeName: betTypeName,
                siteType: site,
                mainColumnText: site
            });
            tab.goForward('site', data);
        }

        function viewMixParlayWindow() {
            var url = '/WinLoss/DailyMixParlayWinLoss_2hLink/Index?startDate=' + pane.data.startDate.format('MM/DD/YYYY') + '&endDate=' + pane.data.endDate.format('MM/DD/YYYY') + '&SportId=' + parlaySportType + '&IsRedirect=true&BetType=' + mixParlayBetType;
            url = legacyService.resolveUrl(url);
            location.href = url;
        }


        function checkvalidLinks() {
            var balanceday = moment(napConfig.BalanceUp);
            return pane.data.startDate.isSameOrAfter(balanceday);
        }

        function createLinks(data) {
            for (var i = 0; i < data.bodyRows.length; i++) {
                var betTypeRow = data.bodyRows[i];
                var betTypeName = betTypeRow.Columns[0].Value;
                var betTypeId = parseInt(betTypeRow.Columns[0].CustomAttr);
                var isEnableLink = checkvalidLinks();
                if (betTypeId === systemParlayBetType && isEnableLink === true && pane.data.normalMode === true) {
                    betTypeRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateSite(' + betTypeId + ',\'' + betTypeName + '\',\'ALL\')">' + betTypeName + '</a>';
                    for (var j = 0; j < betTypeRow.SubRows.length; j++) {
                        var siteRow = data.bodyRows[i].SubRows[j];
                        var siteName = siteRow.Columns[0].Value;
                        siteRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateSite(' + betTypeId + ',\'' + betTypeName + '\',\'' + siteName + '\')">' + siteName + '</a>';
                    }
                } else if (betTypeId === mixParlayBetType) {
                    betTypeRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="viewMixParlayWindow()">' + betTypeName + '</a>';
                }
            }
        }

        function renderGrid(data) {
            var titleInfo = {
                text: tab.generateBreadcrumbHtml(),
                showCollapseExpandButton: true,
                showExcelButton: true,
                exportExcelFunction: exportExcel,
                customButtons: showHideColumnBuilder.getButtonHtml()
            };

            data.titleInfo = titleInfo;
            data.gridExpandHeight = fanexGridService.calculateExpandHeight(gridElement, true);
            data.bottomBorderWidth = 3;
            gridElement.fanexGrid(data);
        }

        function exportExcel() {
            var title = tab.generateBreadcrumbText();
            var httpData = {};
            angular.extend(httpData, pane.data);
            angular.extend(httpData, { title: title });
            pane.service.exportExcel($scope.viewSetting, httpData, pane.getFileName());
        }
    }
})(angular);