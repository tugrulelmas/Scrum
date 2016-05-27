(function() {
  'use strict';

  angular.module('abioka', [
      'ngResource',
      'ngCookies',
      'ui.sortable',
      'directive.g+signin',
      'ngMessages',
      'abioka.router'
    ])
    .run(run);

  run.$inject = ['$rootScope', 'userService', '$state', '$stateParams'];

  function run($rootScope, userService, $state, $stateParams) {
    $rootScope.$on('$stateChangeStart', function(e, toState, toParams, fromState, fromParams) {
      var user = userService.getUser();
      if (toState.isPublic !== true && !user.IsSignedIn) {
        e.preventDefault();
        $state.transitionTo("login", null, {
          notify: false
        });
        $state.go("login");
      }
    });
  }
})();
