import angular from "angular";
import "bootstrap";
import "angular-ui-bootstrap";
import "angular-route";
import "angular-sanitize";
import "angular-moment";


(function (angular) {
    'use strict';

    angular
        .module('app.core', [
            'ui.bootstrap',
            'ngRoute',
            'ngSanitize',
            'angularMoment'
        ]);
})(angular);