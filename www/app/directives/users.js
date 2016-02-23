(function() {
  'use strict';

  angular.module('abioka')
    .directive('users', users);

  function users() {
    var directive = {
      restrict: 'EA',
      scope: {
        model: '='
      },
      templateUrl: 'Views/Partials/users.html',
      replace: true
    };
    return directive;
  }
})();
