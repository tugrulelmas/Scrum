angular.module('abioka').controller('profileController', ['$scope', '$http', 'translationService', 'userService', function($scope, $http, translationService, userService) {
  BaseCtrl.call(this, $scope, translationService);

  $scope.user = userService.getUser();
  $scope.action = {};

  $scope.save = function() {
    $scope.action.loading = true;
    $http.put('./User/update', $scope.user).then(function(){
      $scope.action.loading = false;
      userService.updateUser($scope.user);
      alert.info($scope.ml("ProfileSettingsAreUpdated"))
    }, function(reason){
      $scope.action.loading = false;
    });
  }
}]);
