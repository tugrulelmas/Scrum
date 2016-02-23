(function() {
  'use strict';

  angular.module('abioka')
    .service('authService', authService);

  authService.$inject = ['$rootScope', '$q', '$http', '$location', 'userService', 'localSignInService', 'googleSignInService'];

  function authService($rootScope, $q, $http, $location, userService, localSignInService, googleSignInService) {
    var user = {};
    activate();

    var service = {
      login: login,
      logout: logout
    };
    return service;

    function activate() {
      $rootScope.$on("userLoggedInForProvider", function(events, user) {
        login(user);
      });
    }

    function login(user) {
      $http.post("./Auth/Token", user).success(function(result) {
        userService.setUser(result, function(user) {
          $location.path("/");
        });
      });
    }

    function logout() {
      internalLogut().then(function() {
        userService.destroy();
        $rootScope.$broadcast('userSignedOut', null);
        //TODO: call web service. add log for user.
      });
    }

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
  }
})();
