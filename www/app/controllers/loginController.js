angular.module('abioka').controller('loginController', ['$scope', '$location', 'translationService', 'gapiService', function($scope, $location, translationService, gapiService){
  BaseCtrl.call(this, $scope, translationService);
  $scope.user = {};

  $scope.$on("userSignedIn", function(events, user){
    $scope.user = user;
    //TODO: call web service. add log for user.
    $location.path("/");
    $scope.$apply();
  });

  function init(){
    gapiService.renderSignInButton("loginButton");
  }

  init();
}]);
