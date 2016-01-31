angular.module('abioka')

.service('initializerService', ['$rootScope', '$q', 'abiokaSettings', function($rootScope, $q, abiokaSettings) {
  var googleAuthObj;
  var loadingGoogle = false;

  this.initialize = function() {
    this.initializeGoogle();
  };

  this.initializeGoogle = function() {
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
}]);
