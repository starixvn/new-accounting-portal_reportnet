import angular from "angular";
import Blob from "blob";
(function (angular) {
    'use strict';

    angular
        .module('app.core')
        .factory('httpResponseService', httpResponseService);

    httpResponseService.$inject = ['loggerService'];
    function httpResponseService(loggerService) {
        var service = {
            getDataComplete: getDataComplete,
            getDataFailed: getDataFailed,
            saveExcelFile: saveExcelFile
        };

        return service;

        function getDataComplete(response) {
            loggerService.hide();
            return response.data;
        }

        function getDataFailed() {
            loggerService.logError('Sorry, an error occurred while processing your request, please notify the administrators.');
        }

        function saveExcelFile(data, fileName) {
            var contentType = 'application/vnd.ms-excel';
            var blob = new Blob([data], { type: contentType });
            if (navigator.appVersion.toString().indexOf('.NET') > 0) {
                window.navigator.msSaveBlob(blob, fileName);
            }
            else {
                var e = document.createEvent('MouseEvents');
                var a = document.createElement('a');

                a.download = fileName;
                a.href = window.URL.createObjectURL(blob);
                a.dataset.downloadurl = [contentType, a.download, a.href].join(':');
                e.initMouseEvent('click', true, false, window,
                    0, 0, 0, 0, 0, false, false, false, false, 0, null);
                a.dispatchEvent(e);
            }
        }
    }
})(angular);