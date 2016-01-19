angular.module('abioka', ['ngRoute', 'ngResource', 'ngCookies', 'ui.sortable', 'directive.g+signin'])
.constant('abiokaSettings',
    {
        apiUrl: "http://localhost/AbiokaScrum.Api/api/"
    }
)
.value('context',
    {

    }
)
.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider
     .when('/boards', { templateUrl: 'Views/boards.html', controller: 'boardsController' })
     .when('/board/:boardId', { templateUrl: 'Views/board.html', controller: 'boardController' })
     .when('/login', { templateUrl: 'Views/login.html', controller: 'loginController' })
     .otherwise({ redirectTo: '/boards' });
}])
.run(['$rootScope','$location', 'userService', function($rootScope, $location, userService) {
  var user = userService.getUser();
  $rootScope.$on( "$routeChangeStart", function(event, next, current) {
    if(next.templateUrl !== "Views/login.html" && !user.IsSignedIn){
      $location.path("/login");
    }
  });
}]);
