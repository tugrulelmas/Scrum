angular.module('abioka')

.service('localSignInService', ['$rootScope', '$q', 'restService', function($rootScope, $q, restService) {
  this.login = function(localUser) {
    restService.post("User/Login", localUser).then(function(result) {
        $rootScope.$broadcast('userLoggedInForProvider', result);
    });
  };

  this.logout = function() {
    var deferred = $q.defer();
    deferred.resolve();
    return deferred.promise;
  }
}]);
