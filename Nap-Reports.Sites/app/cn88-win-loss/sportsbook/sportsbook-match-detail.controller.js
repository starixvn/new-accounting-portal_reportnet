import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.cn88WinlossApp')
        .controller('SportsbookMatchDetailController', SportsbookMatchDetailController);

    SportsbookMatchDetailController.$inject = ['$scope', '$element', 'fanexGridService', '$uibModal', '$uibModalStack'];
    function SportsbookMatchDetailController($scope, $element, fanexGridService, $uibModal, $uibModalStack) {
        var pane = $scope.pane;
        var tab = $scope.tab;
        var gridElement = $element.find('.freegrid');
        $scope.generate = generate;
        $scope.viewMatchInformation = viewMatchInformation;
        $scope.viewSetting = {};

        function generate(viewSetting) {
            $scope.viewSetting = viewSetting;
            pane.service.getData(viewSetting, pane.data).then(function (data) {
                createLinks(data);
                renderGrid(data);
                pane.compileAll();
            });
        }

        function viewMatchInformation(sportType, matchId, eventDate, leagueName, matchCode) {
            pane.data.sportType = sportType;
            pane.data.matchId = matchId;
            pane.data.eventDate = eventDate;
            pane.data.matchCode = matchCode;
            pane.service.getMatchInformation(pane.data).then(function (data) {
                showPopup(data, leagueName);
            });
        }

        function showPopup(data, leagueName) {
            var traderName = data && data.TraderName && data.TraderName !== '' ? data.TraderName : "Unassigned";
            var newScope = $scope.$new();
            angular.extend(newScope, {
                sportTypeName: pane.data.sportTypeName,
                betTypeName: pane.data.betTypeName,
                leagueName: leagueName,
                traderName: traderName,
                matchId: pane.data.matchId,
                liveName: getLiveName(pane.data.isLive),
                matchInformation: data.MatchInformation,
                closePopup: closePopup
            });
            $uibModal.open({
                templateUrl: 'app/cn88-win-loss/sportsbook/sportsbook-match-information.html',
                size: 'match-information',
                scope: newScope
            });
        }

        function getLiveName(data) {

            if (data === 1) {
                return "Live!";
            }
            else if (data === 0) {
                return "Non-Live!";
            }
            else {
                return " ";
            }
        }

        function closePopup() {
            $uibModalStack.dismissAll("Close");
        }
        function createLinks(data) {
            for (var i = 0; i < data.bodyRows.length; i++) {
                var bodyRow = data.bodyRows[i];
                var customAttr = bodyRow.Columns[0].CustomAttr;
                if (customAttr) {
                    var matchName = bodyRow.Columns[0].Value;
                    bodyRow.Columns[0].Value = '<a class="drill-down" href data-nap-click="viewMatchInformation(' + customAttr + ')">' + matchName + '</a>';
                }
            }
        }

        function renderGrid(data) {
            var titleInfo = {
                text: tab.generateBreadcrumbHtml(),
                showExcelButton: true,
                exportExcelFunction: exportExcel
            };

            data.titleInfo = titleInfo;
            data.showPager = true;
            data.pagerOption = {
                itemsPerPage: 50,
                currentPage: 0,
                itemsPerPageArray: [50, 100, 200, 500],
                totalItem: data.bodyRows.length,
                afterPagedFunction: function () {
                    pane.compileDrilldown();
                }
            };
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