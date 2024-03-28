import angular from "angular";
(function (angular, undefined) {
    'use strict';

    angular
        .module('app.cn88WinlossApp')
        .service('cn88WinlossService', cn88WinlossService);

    cn88WinlossService.$inject = ['templateSetting'];
    function cn88WinlossService(templateSetting) {
        var service = {
            getViewSetting: getViewSetting,
            getMainView: getMainView
        };
        return service;

        function getMainView(productName) {
            var productSetting = templateSetting[productName];
            var isSettingCorrupted = angular.isUndefined(productSetting) || angular.isUndefined(productSetting["mainView"]);

            return isSettingCorrupted ? undefined : productSetting["mainView"];
        }

        function getViewSetting(productName, viewType, data) {

            var productSetting = templateSetting[productName];
            var isSettingCorrupted = angular.isUndefined(productSetting) || angular.isUndefined(productSetting[viewType]);

            if (isSettingCorrupted) {
                return undefined;
            }

            var viewSetting = productSetting[viewType];
            
            return compileSetting(viewSetting, data);
        }

        function compileSetting(setting, data) {
            var compiledSetting = {};
            for (var property in setting) {
                if (setting.hasOwnProperty(property)) {
                    compiledSetting[property] = resolveFunction(setting[property], data);
                }
            }

            return compiledSetting;
        }

        function resolveFunction(func, data) {
            return angular.isFunction(func)
            ? func(data)
                : func;
        }
    }
})(angular, undefined);