import angular from "angular";
(function (angular) {
    'use strict';

    angular
        .module('app.core')
        .directive('napClick', napClick);

    napClick.$inject = ['$parse', '$rootScope'];
    function napClick($parse, $rootScope) {
        var directive = {
            restrict: 'A',
            compile: compile
        };
        return directive;

        function compile($element, attr) {
            // so that they are not executed in an inconsistent state.
            var forceAsyncEvents = {
                'blur': true,
                'focus': true
            };
            var eventName = 'click';
            var directiveName = 'napClick';
            var fn = $parse(attr[directiveName], /* interceptorFn */ null, /* expensiveChecks */ true);
            return function eventHandler(scope, element) {
                element.on(eventName, function (event) {
                    event.stopPropagation();
                    var callback = function () {
                        fn(scope);
                    };
                    if (forceAsyncEvents[eventName] && $rootScope.$$phase) {
                        scope.$evalAsync(callback);
                    } else {
                        scope.$apply(callback);
                    }
                });
            };
        }
    }
})(angular);