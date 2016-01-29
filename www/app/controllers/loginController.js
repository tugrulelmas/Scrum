angular.module('abioka').controller('loginController', ['$scope', 'translationService', 'userService', 'localSignInService', function($scope, translationService, userService, localSignInService) {
  BaseCtrl.call(this, $scope, translationService);

  $scope.user = {};
  $scope.defaultUser = userService.getUser();

  $scope.login = function() {
    localSignInService.login($scope.user);
  }
}]);
