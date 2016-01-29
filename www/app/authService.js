angular.module('abioka')

.service('authService', ['$rootScope', '$q', '$http', '$location', 'userService', 'localSignInService', 'googleSignInService', function($rootScope, $q, $http, $location, userService, localSignInService, googleSignInService) {
  var self = this;
  var user = {};
  self.login = function(user) {
    $http.post("./Auth/Token", user).success(function(result) {
      userService.setUser(result, function(user){
        $location.path("/");
      });
    });
  };

  self.logout = function() {
    internalLogut().then(function() {
      userService.destroy();
      $rootScope.$broadcast('userSignedOut', null);
      //TODO: call web service. add log for user.
    });
  };

  $rootScope.$on("userLoggedInForProvider", function(events, user) {
    self.login(user);
  });

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
