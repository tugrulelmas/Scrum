angular.module('abioka').controller('loginController', ['$scope', 'translationService', 'userService', 'localSignInService', function($scope, translationService, userService, localSignInService) {
  BaseCtrl.call(this, $scope, translationService);

  $scope.user = {};
  $scope.action = {};
  $scope.defaultUser = userService.getUser();

  $scope.login = function() {
    $scope.action.loading = true;
    localSignInService.login($scope.user).then(function(){
      $scope.action.loading = false;
    }, function(reason){
      $scope.action.loading = false;
    });
  }
}]);
