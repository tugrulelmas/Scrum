angular.module('abioka').controller('registerController', ['$scope', 'translationService', 'localSignInService', function($scope, translationService, localSignInService) {
  BaseCtrl.call(this, $scope, translationService);

  $scope.user = {};
  $scope.signUp = function() {
    localSignInService.signUp($scope.user);
  }
}]);
