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
.directive("modal", function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            scope.showModal = function () {
              element.modal('show');
            }
            scope.hideModal = function () {
                element.modal('hide');
            }
        }
    }
})
.directive('onEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if(event.which === 13) {
                scope.$apply(function (){
                    scope.$eval(attrs.onEnter);
                });

                event.preventDefault();
            }
        });
    };
})
.directive('users', function(){
  return {
      restrict: 'EA',
      scope: {
        model: '='
      },
      template: '<any ng-repeat="user in model">' +
                  '<span ng-if="user.ImageUrl" class="card-avatar"><img ng-src="{{user.ImageUrl}}" alt="{{user.Name}}" class="img-circle"/></span>' +
                  '<span ng-if="!user.ImageUrl" class="label label-avatar circle-text">{{user.ShortName}}</span>'+
                '</any>',
      replace: true
  };
})
.filter('formattedDate', ["$filter", "translationService", function ($filter, translationService) {
    return function (input, formatName) {
        if (input == null) {
            return "";
        }
        var format = "";
        if (!formatName || formatName === "") {
            format = translationService.getResource("LongDateFormat");
        } else {
            format = translationService.getResource(formatName);
        }

        var _date = $filter('date')(new Date(input), format);
        return _date.toUpperCase();
    };
}])
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
