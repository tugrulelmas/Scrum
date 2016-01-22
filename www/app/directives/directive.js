angular.module('abioka')
  .directive('onEnter', function() {
    return function(scope, element, attrs) {
      element.bind("keydown keypress", function(event) {
        if (event.which === 13) {
          scope.$apply(function() {
            scope.$eval(attrs.onEnter);
          });

          event.preventDefault();
        }
      });
    };
  })
  .directive('users', function() {
    return {
      restrict: 'EA',
      scope: {
        model: '='
      },
      template: '<any ng-repeat="user in model">' +
        '<span ng-if="user.ImageUrl" class="card-avatar"><img ng-src="{{user.ImageUrl}}" alt="{{user.Name}}" class="img-circle"/></span>' +
        '<span ng-if="!user.ImageUrl" class="label label-avatar circle-text">{{user.ShortName}}</span>' +
        '</any>',
      replace: true
    };
  })
  .directive('validationMessage', function() {
    return {
      restrict: 'EA',
      templateUrl: 'Views/Partials/messages.html',
      scope: {
        fieldName: '@',
        model: '='
      },
      controller: ['$scope', 'translationService', function($scope, translationService) {
        BaseCtrl.call(this, $scope, translationService);
      }]
    };
  });
