angular.module('abioka')

.service('authService', ['$rootScope', '$q', 'userService', 'restService', 'localSignInService', 'googleSignInService', function($rootScope, $q, userService, restService, localSignInService, googleSignInService) {
  var user = {};
  this.login = function(user) {
    restService.post("Auth/Token", user).then(function(result) {
      userService.setUser(result, function(user){
        $rootScope.$broadcast('userSignedIn', user);
      });
    });
  };

  this.logout = function() {
    var deferred = $q.defer();

    internalLogut().then(function() {
      userService.destroy();
      deferred.resolve();
      $rootScope.$broadcast('userSignedOut', null);
      //TODO: call web service. add log for user.
    });
    return deferred.promise;
  };

  function internalLogut() {
    var user = userService.getUser();
    var deferred = $q.defer();

    if (user.Provider === "Google") {
      googleSignInService.logout().then(function() {
        deferred.resolve();
      });
    } else if (user.Provider === "Local") {
      localSignInService.logout().then(function() {
        deferred.resolve();
      });
    } else {
      deferred.reject("unknown provider");
    }
    return deferred.promise;
  }
}]);
