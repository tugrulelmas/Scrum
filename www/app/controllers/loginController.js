angular.module('abioka').controller('loginController', ['$scope', '$location', 'translationService', 'restService', 'localSignInService', 'authService', function($scope, $location, translationService, restService, localSignInService, authService) {
  BaseCtrl.call(this, $scope, translationService);

  $scope.user = {};

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
