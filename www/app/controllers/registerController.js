angular.module('abioka').controller('registerController', ['$scope', 'translationService', 'localSignInService', function($scope, translationService, localSignInService) {
  BaseCtrl.call(this, $scope, translationService);

  $scope.user = {};
  $scope.action = {};
  
  $scope.signUp = function() {
    $scope.action.loading = true;
    localSignInService.signUp($scope.user).then(function(){
      $scope.action.loading = false;
    }, function(reason){
      $scope.action.loading = false;
    });
  }
}]);
