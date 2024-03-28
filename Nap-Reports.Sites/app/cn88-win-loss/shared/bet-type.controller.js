import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.cn88WinlossApp')
        .controller('BetTypeController', BetTypeController);

    BetTypeController.$inject = ['$scope', '$element', 'cn88WinlossService', 'fanexGridService', 'showHideColumnService'];
    function BetTypeController($scope, $element, cn88WinlossService, fanexGridService, showHideColumnService) {
        var pane = $scope.pane;
        var showHideColumnBuilder = new showHideColumnService(pane);
        var tab = $scope.tab;
        var gridElement = $element.find('.freegrid');
        const betradarVirtualSportsMixParlayBettype = 2799;
        $scope.show = showHideColumnBuilder.toggle;
        $scope.generate = generate;
        $scope.generateMatchDetail = generateMatchDetail;
        $scope.createDrilldownLink = createDrilldownLink;
        $scope.createDrilldownLinks = createDrilldownLinks;
        $scope.renderGrid = renderGrid;
        $scope.exportExcel = exportExcel;
        

        function generate(viewSetting) {
            $scope.viewSetting = viewSetting;
            pane.service.getData(viewSetting, pane.data).then(function (data) {
                showHideColumnBuilder.build(gridElement, data);

                var canViewMatchDetail = pane.data.endDate.diff(pane.data.startDate, 'days') <= 3;
                if (canViewMatchDetail) {
                    createDrilldownLinks(data.bodyRows, pane.data.isLive);
                }

                renderGrid(data);
                pane.compileAll();
            });
        }

        function createDrilldownLinks(rows, isLive) {
            for (var i = 0; i < rows.length; i++) {
                createDrilldownLink(rows[i], isLive);
            }
        }

        function createDrilldownLink(betTypeRow, isLive) {
            var betTypeName = betTypeRow.Columns[0].Value;
            var betTypeId = betTypeRow.Columns[0].CustomAttr;

            var template = cn88WinlossService.getViewSetting(pane.data.productName, 'matchDetail', pane.data);
            var hasDetailDrilldown = angular.isDefined(template) && parseInt(betTypeId) !== betradarVirtualSportsMixParlayBettype && parseInt(betTypeId) !== 0;

            if (hasDetailDrilldown) {
                betTypeRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="generateMatchDetail(' + betTypeId + ',\'' + betTypeName.replace(/'/g, "\\'") + '\',' + isLive + ')">' + betTypeName + '</a>';
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

        function generateMatchDetail(betType, betTypeName, isLive) {
            var data = {};
            angular.extend(data, pane.data);
            angular.extend(data,{
                betType: betType,
                betTypeName: betTypeName,
                isLive: isLive
            });
            data.isHedge = getHedgeParameters(data.sportType, betTypeName);

            tab.goForward('matchDetail', data);
        }

        function exportExcel() {
            pane.service.exportExcelForSingleGrid($scope.viewSetting, tab, pane);
        }

        function getHedgeParameters(sportType, betTypeName) {
            if (sportType && sportType > 1) {
                return 2;
            }

            return betTypeName && betTypeName.indexOf("(hedge)") > -1 ? 1 : 0;
        }
    }
})(angular);