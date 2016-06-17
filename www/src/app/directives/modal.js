(function() {
  'use strict';

  angular.module('abioka')
    .directive('modal', modal);

  function modal() {
    var directive = {
      templateUrl: 'Views/Partials/modal.html',
      restrict: 'E',
      transclude: true,
      replace: true,
      scope: {
        title: '@',
        visible: '=',
        afterClosing: '&'
      },
      link: link
    };

    return directive;

    function link(scope, element, attrs) {
      scope.$watch('visible', function(value) {
        if (value == true) {
          $(element).modal('show');
        } else {
          $(element).modal('hide');
        }
      });

      $(element).on('shown.bs.modal', function() {
        scope.$apply(function() {
          scope.$parent[attrs.visible] = true;
        });
      });

      $(element).on('hidden.bs.modal', function() {
        scope.$apply(function() {
          scope.$parent[attrs.visible] = false;
          scope.afterClosing();
        });
      });
    }
  }
})();
