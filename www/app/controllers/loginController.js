angular.module('abioka').controller('loginController', ['$scope', '$location', 'translationService', 'userService', 'localSignInService', 'authService', function($scope, $location, translationService, userService, localSignInService, authService) {
  BaseCtrl.call(this, $scope, translationService);

  $scope.user = {};
  $scope.defaultUser = userService.getUser();

  $scope.$on("userLoggedInForProvider", function(events, user) {
    authService.login(user);
  });

  $scope.$on("userSignedIn", function(events, user) {
    $location.path("/");
  });

  $scope.login = function() {
    localSignInService.login($scope.user);
  }
}]);
