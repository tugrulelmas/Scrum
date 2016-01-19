angular.module('abioka').controller('globalController', ['$scope', '$window', '$location', 'translationService', 'authService', 'userService', function($scope, $window, $location, translationService, authService, userService) {
  BaseCtrl.call(this, $scope, translationService);
  $scope.isGlobalController = true;
  $scope.user = userService.getUser();

  $scope.changeLanguage = function (language) {
      var oldLanguage = userService.getUser().Language;
      if (oldLanguage !== language) {
          userService.setLanguage(language);
          $scope.$broadcast('languageChanged');
      }
  };

  $scope.signOut = function() {
    authService.logout().then(function() {
      $location.path("/login");
    });
  };

  $scope.$on('userSignedOut', function() {
    $scope.user = userService.getUser();
  });
}]);
