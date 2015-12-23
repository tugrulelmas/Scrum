angular.module('abioka').controller('globalController', ['$scope', '$window', '$route', 'translationService', 'gapiService', 'context', function($scope, $window, $route, translationService, gapiService, context){
  BaseCtrl.call(this, $scope, translationService);
  $scope.user = {};
  $scope.user = context.user;

  $scope.$on('userSignedIn', function(){
    $scope.user = context.user;
  });

  $scope.signOut = function() {
    gapiService.signOut(function(){
      //TODO: call web service. add log for user.
      $scope.user = context.user;
      $route.reload();
    });
  };

  $window.initGapi = function() {
    gapiService.initGapi(postInitiation);
  }
}]);
