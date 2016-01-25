angular.module('abioka')

.service('localSignInService', ['$rootScope', '$q', '$http', function($rootScope, $q, $http) {
  this.login = function(localUser) {
    $http.post("./User/Login", localUser).success(function(result) {
        $rootScope.$broadcast('userLoggedInForProvider', result);
    });
  };

  this.logout = function() {
    var deferred = $q.defer();
    deferred.resolve();
    return deferred.promise;
  }
}]);
