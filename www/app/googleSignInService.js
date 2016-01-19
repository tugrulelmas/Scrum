angular.module('abioka')

.service('googleSignInService', ['$rootScope', '$q', function($rootScope, $q) {
  this.login = function(googleUser) {
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

  this.logout = function() {
    var auth2 = gapi.auth2.getAuthInstance();
    return auth2.signOut();
  }
}]);
