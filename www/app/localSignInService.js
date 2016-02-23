(function() {
  'use strict';

  angular.module('abioka')
    .service('localSignInService', localSignInService);

  localSignInService.$inject = ['$rootScope', '$q', '$http']

  function localSignInService($rootScope, $q, $http) {
    var service = {
      login: login,
      logou: logout,
      signUp: signUp
    };
    return service;

    function login(localUser) {
      var deferred = $q.defer();
      $http.post("./User/Login", localUser).then(function(result) {
        $rootScope.$broadcast('userLoggedInForProvider', result.data);
        deferred.resolve();
      }, function(response) {
        deferred.reject(response);
      });
      return deferred.promise;
    };

    function signUp(signUpRequest) {
      var deferred = $q.defer();
      $http.post("./User/signup", signUpRequest).then(function(result) {
        $rootScope.$broadcast('userLoggedInForProvider', result.data);
        deferred.resolve();
      }, function(response) {
        deferred.reject(response);
      });
      return deferred.promise;
    };

    function logout() {
      var deferred = $q.defer();
      deferred.resolve();
      return deferred.promise;
    }
  }
})();
