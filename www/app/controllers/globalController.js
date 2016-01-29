angular.module('abioka').controller('globalController', ['$scope', '$location', 'translationService', 'authService', 'userService', 'initializerService', function($scope, $location, translationService, authService, userService, initializerService) {
  translationService.setGlobalResources();
  BaseCtrl.call(this, $scope, translationService);
  initializerService.initialize();

  $scope.user = userService.getUser();

  $scope.changeLanguage = function(language) {
    var oldLanguage = userService.getUser().Language;
    if (oldLanguage !== language) {
      userService.setLanguage(language);
      translationService.setGlobalResources(function() {
        alert.warning($scope.ml("LanguageChangedMessage"));
      });
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
