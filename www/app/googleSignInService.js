(function() {
  'use strict';

  angular.module('abioka')
    .service('googleSignInService', googleSignInService);

  googleSignInService.$inject = ['$rootScope', '$q'];

  function googleSignInService($rootScope, $q) {
    var service = {
      login: login,
      logout: logout
    };
    return service;

    function login(googleUser) {
      var profile = googleUser.getBasicProfile();
      var user = {
        "Id": profile.getId(),
        "Name": profile.getName(),
        "ImageUrl": profile.getImageUrl(),
        "Email": profile.getEmail(),
        "ProviderToken": googleUser.getAuthResponse().id_token,
        "Provider": "google"
      };

      $rootScope.$broadcast('userLoggedInForProvider', user);
    };

    function logout() {
      var auth2 = gapi.auth2.getAuthInstance();
      return auth2.signOut();
    }
  }
})();
