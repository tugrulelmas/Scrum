angular.module('abioka').controller('globalController', ['$scope', '$window', '$location', 'translationService', 'authService', 'userService', function($scope, $window, $location, translationService, authService, userService) {
  BaseCtrl.call(this, $scope, translationService);
  $scope.user = userService.getUser();

  $scope.signOut = function() {
    authService.logout().then(function() {
      $location.path("/login");
    });
  };

  $scope.$on('userSignedOut', function() {
    $scope.user = userService.getUser();
  });
}]);
