angular.module('abioka', ['ngRoute', 'ngResource', 'ui.sortable'])
.constant('abiokaSettings',
    {
        apiUrl: "http://localhost/AbiokaScrum.Api/api/"
    }
)
.value('context',
    {
        user: {
            lang: "en",
            IsSignedIn: false
        }
    }
)
.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider
     .when('/boards', { templateUrl: 'Views/boards.html', controller: 'boardsController' })
     .when('/board/:boardId', { templateUrl: 'Views/board.html', controller: 'boardController' })
     .when('/login', { templateUrl: 'Views/login.html', controller: 'loginController' })
     .otherwise({ redirectTo: '/boards' });
}])
.run(['$rootScope','$location', 'context', function($rootScope, $location, context) {
  $rootScope.$on( "$routeChangeStart", function(event, next, current) {
    if(next.templateUrl !== "Views/login.html" && !context.user.IsSignedIn){
      $location.path("/login");
    }
  });
}]);
