(function() {
  'use strict';

  angular.module('abioka')
    .service('initializerService', initializerService);

  /* @ngInject */
  function initializerService($rootScope, $q, abiokaSettings) {
    var googleAuthObj;
    var loadingGoogle = false;

    var service = {
      initialize: initialize,
      initializeGoogle: initializeGoogle
    };
    return service;

    function initialize() {
      initializeGoogle();
    };

    function initializeGoogle() {
      if (loadingGoogle)
        return;

      if (typeof gapi !== "undefined" && gapi.auth2) {
        $rootScope.$broadcast('gapiLoaded', gapi.auth2.getAuthInstance());
      } else {
        loadingGoogle = true;
        // Asynchronously load the G+ SDK.
        var po = document.createElement('script');
        po.type = 'text/javascript';
        po.async = true;
        po.src = 'https://apis.google.com/js/client:platform.js';
        var s = document.getElementsByTagName('script')[0];
        s.parentNode.insertBefore(po, s);

        po.onload = function() {
          //Initialize Auth2 with our clientId
          gapi.load('auth2', function() {
            googleAuthObj =
              gapi.auth2.init({
                client_id: abiokaSettings.googleClientId,
                scope: 'profile email',
                cookie_policy: 'single_host_origin'
              });
            loadingGoogle = false;
            $rootScope.$broadcast('gapiLoaded', googleAuthObj);
          });
        };
      }
    };
  }
})();
