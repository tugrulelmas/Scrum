angular.module('abioka', ['ngRoute', 'ngResource', 'ngCookies', 'ui.sortable', 'directive.g+signin', 'ngMessages'])
.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider
     .when('/boards', { templateUrl: 'Views/boards.html', controller: 'boardsController' })
     .when('/board/:boardId', { templateUrl: 'Views/board.html', controller: 'boardController' })
     .when('/profile', { templateUrl: 'Views/profile.html', controller: 'profileController' })
     .when('/changePassword', { templateUrl: 'Views/changePassword.html', controller: 'changePasswordController' })
     .when('/login', { templateUrl: 'Views/login.html', controller: 'loginController' })
     .when('/register', { templateUrl: 'Views/register.html', controller: 'registerController' })
     .otherwise({ redirectTo: '/boards' });
}])
.config(['$httpProvider', function($httpProvider) {
    $httpProvider.interceptors.push('tokenInjector');
    $httpProvider.interceptors.push('errorInjector');
}])
.run(['$rootScope','$location', 'userService', function($rootScope, $location, userService) {
  $rootScope.$on( "$routeChangeStart", function(event, next, current) {
    var user = userService.getUser();
    if(next.templateUrl !== "Views/login.html" && next.templateUrl !== "Views/register.html" && !user.IsSignedIn){
      $location.path("/login");
    }
  });
}]);
