angular.module('abioka').controller('globalController', ['$scope', '$location', 'translationService', 'authService', 'userService', 'initializerService', function($scope, $location, translationService, authService, userService, initializerService) {
  BaseCtrl.call(this, $scope, translationService);
  initializerService.initialize();

  $scope.isGlobalController = true;
  $scope.user = userService.getUser();

  $scope.changeLanguage = function(language) {
    var oldLanguage = userService.getUser().Language;
    if (oldLanguage !== language) {
      userService.setLanguage(language);
      $scope.$broadcast('languageChanged');
    }
  };

  $scope.signOut = function() {
    authService.logout();
  };

  $scope.$on('userSignedOut', function() {
    $scope.user = userService.getUser();
    $location.path("/login");
  });
}]);
