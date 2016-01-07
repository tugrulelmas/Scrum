angular.module('abioka').controller('loginController', ['$scope', '$location', 'translationService', 'gapiService', 'restService', function($scope, $location, translationService, gapiService, restService) {
  BaseCtrl.call(this, $scope, translationService);

  $scope.$on("userSignedIn", function(events, user) {
    restService.post("User/Login", user).then(function(result) {
      $location.path("/");
    });
  });

  function init() {
    gapiService.renderSignInButton("loginButton");
  }

  init();
}]);
