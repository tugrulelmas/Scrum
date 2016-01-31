angular.module('abioka').controller('changePasswordController', ['$scope', '$http', 'translationService', 'userService', function($scope, $http, translationService, userService) {
  BaseCtrl.call(this, $scope, translationService);

  $scope.action = {};

  $scope.save = function() {
    $scope.action.loading = true;
    $http.put('./User/ChangePassword', $scope.user).then(function(){
      $scope.action.loading = false;
      setDefault();
      $scope.changePasswordForm.$setUntouched();
      alert.info($scope.ml("PasswordIsChanged"))
    }, function(reason){
      $scope.action.loading = false;
    });
  }

  function setDefault(){
    $scope.user = {};
    $scope.user.Id = userService.getUser().Id;
  }

  setDefault();
}]);
