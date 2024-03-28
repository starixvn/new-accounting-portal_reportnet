import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.houseWinlossApp')
        .controller('NGBetTypeController', NGBetTypeController);

    NGBetTypeController.$inject = ['$scope', '$element', 'houseWinlossService', 'fanexGridService', 'showHideColumnService'];
    function NGBetTypeController($scope, $element, houseWinlossService, fanexGridService, showHideColumnService) {
        var pane = $scope.pane;
        var showHideColumnBuilder = new showHideColumnService(pane);
        var tab = $scope.tab;
        var gridElement = $element.find('.freegrid');
        $scope.show = showHideColumnBuilder.toggle;
        $scope.generate = generate;
        $scope.generateSite = generateSite;
        $scope.generateMatchDetail = generateMatchDetail;
        $scope.createDrilldownLink = createDrilldownLink;
        $scope.createDrilldownLinks = createDrilldownLinks;
        $scope.renderGrid = renderGrid;
        $scope.exportExcel = exportExcel;
        $scope.leagueId = 0;

        function generate(viewSetting) {
            $scope.viewSetting = viewSetting;
            $scope.leagueId = pane.data.leagueId;
            pane.service.getData(viewSetting, pane.data).then(function (data) {
                showHideColumnBuilder.build(gridElement, data);

                var canViewMatchDetail = pane.data.endDate.diff(pane.data.startDate, 'days') <= 3;
                if (canViewMatchDetail) {
                    createDrilldownLinks(data.bodyRows);
                }

                renderGrid(data);
                pane.compileAll();
            });
        }

        function createDrilldownLinks(rows) {
            for (var i = 0; i < rows.length; i++) {
                createDrilldownLink(rows[i]);
            }
        }

        function createDrilldownLink(betTypeRow) {
            var betTypeName = betTypeRow.Columns[0].Value;
            var betTypeId = betTypeRow.Columns[0].CustomAttr;

            if (betTypeRow.SubRows) {
                var bettypeIdValue = parseInt(betTypeId);

                if(bettypeIdValue < 3601 || bettypeIdValue > 3610) {
                    betTypeRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateMatchDetail(' + betTypeId + ',\'' + betTypeName.replace(/'/g, "\\'") + '\')">' + betTypeName + '</a>';
                }

                for (var i = 0; i < betTypeRow.SubRows.length; i++) {
                    var siteRow = betTypeRow.SubRows[i];
                    var siteName = siteRow.Columns[0].Value;
                    siteRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateSite(' + betTypeId + ',\'' + betTypeName.replace(/'/g, "\\'") + '\',\'' + siteName.replace(/'/g, "\\'") + '\')">' + siteName + '</a>';
                }
            }
        }

        function renderGrid(data) {
            var titleInfo = {
                text: tab.generateBreadcrumbHtml(),
                showCollapseExpandButton: true,
                showExcelButton: true,
                exportExcelFunction: $scope.exportExcel,
                customButtons: showHideColumnBuilder.getButtonHtml()
            };

            data.titleInfo = titleInfo;
            data.gridExpandHeight = fanexGridService.calculateExpandHeight(gridElement, true);
            data.bottomBorderWidth = 3;
            gridElement.fanexGrid(data);
        }

        function generateSite(betType, betTypeName, site) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                betType: betType,
                betTypeName: betTypeName,
                siteType: site,
                mainColumnText: site,
                leagueId: $scope.leagueId
            });

            tab.goForward('site', data);
        }

        function generateMatchDetail(betType, betTypeName) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data, {
                betType: betType,
                betTypeName: betTypeName,
                leagueId: $scope.leagueId
            });

            tab.goForward('matchDetail', data);
        }

        function exportExcel() {
            pane.service.exportExcelForSingleGrid($scope.viewSetting, tab, pane);
        }
    }
})(angular);