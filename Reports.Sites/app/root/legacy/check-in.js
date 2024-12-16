import angular from "angular";
import $ from "jquery";
import jQuery from "jquery";

(function ($) {
    'use strict';

    angular
        .module('app.core')
        .run(reCheckIn);

    reCheckIn.$inject = ['$interval', 'legacyService'];
    function reCheckIn($interval, legacyService) {
        $interval(callback, 60000);

        function callback() {
            var url = legacyService.resolveUrl('/Auth/ReCheckIn');
            $.post(url, function (result) {
                if (result.IsKickedOut === "1") {
                    alert('Another session has been logged in or your session is expired. Forced to logout!');
                    window.location.href = legacyService.resolveUrl('/Auth/LogOut');
                }
            }).fail(function (request, textStatus, error) {
                var result = $.parseJSON(request.responseText);
                if (result.code == 999999) {
                    window.location.href = legacyService.resolveUrl('/Auth/Offline');
                }
            });;
        }
    }
})(jQuery);