angular.module('abioka')

.service('localSignInService', ['$rootScope', '$q', '$http', function($rootScope, $q, $http) {
  this.login = function(localUser) {
    var deferred = $q.defer();
    $http.post("./User/Login", localUser).then(function(result) {
      $rootScope.$broadcast('userLoggedInForProvider', result);
      deferred.resolve();
    }, function(response) {
      deferred.reject(response);
    });
    return deferred.promise;
  };

  this.signUp = function(signUpRequest) {
    var deferred = $q.defer();
    $http.post("./User/signup", signUpRequest).then(function(result) {
      $rootScope.$broadcast('userLoggedInForProvider', result);
      deferred.resolve();
    }, function(response) {
      deferred.reject(response);
    });
    return deferred.promise;
  };

  this.logout = function() {
    var deferred = $q.defer();
    deferred.resolve();
    return deferred.promise;
  }
}]);
