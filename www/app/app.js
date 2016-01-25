﻿angular.module('abioka', ['ngRoute', 'ngResource', 'ngCookies', 'ui.sortable', 'directive.g+signin', 'ngMessages'])
.constant('abiokaSettings',
    {
        apiUrl: "http://localhost/AbiokaScrum.Api/api/"
    }
)
.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider
     .when('/boards', { templateUrl: 'Views/boards.html', controller: 'boardsController' })
     .when('/board/:boardId', { templateUrl: 'Views/board.html', controller: 'boardController' })
     .when('/login', { templateUrl: 'Views/login.html', controller: 'loginController' })
     .otherwise({ redirectTo: '/boards' });
}])
.config(['$httpProvider', function($httpProvider) {
    $httpProvider.interceptors.push('tokenInjector');
    $httpProvider.interceptors.push('errorInjector');
}])
.run(['$rootScope','$location', 'userService', function($rootScope, $location, userService) {
  $rootScope.$on( "$routeChangeStart", function(event, next, current) {
    var user = userService.getUser();
    if(next.templateUrl !== "Views/login.html" && !user.IsSignedIn){
      $location.path("/login");
    }
  });
}]);
