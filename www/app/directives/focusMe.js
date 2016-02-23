(function() {
  'use strict';

  angular.module('abioka')
    .directive('focusMe', focusMe);

  focusMe.$inject = ['$timeout', '$parse'];

  function focusMe($timeout, $parse) {
    var directive = {
      link: link
    };
    return directive;

    function link(scope, element, attrs) {
      var model = $parse(attrs.focusMe);
      scope.$watch(model, function(value) {
        if (value === true) {
          $timeout(function() {
            element[0].focus();
          });
        }
      });

      element.bind('blur', function() {
        //scope.$apply(model.assign(scope, false));
      });
    }
  }
})();
