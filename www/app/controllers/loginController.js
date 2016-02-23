(function() {
  'use strict';

  angular.module('abioka')
    .controller('LoginController', LoginController);

  LoginController.$inject = ['translationService', 'userService', 'localSignInService'];

  function LoginController(translationService, userService, localSignInService) {
    var vm = this;
    BaseCtrl.call(this, vm, translationService);

    vm.user = {};
    vm.action = {};
    vm.defaultUser = userService.getUser();
    vm.login = login;

    function login() {
      vm.action.loading = true;
      localSignInService.login(vm.user).then(function() {
        vm.action.loading = false;
      }, function(reason) {
        vm.action.loading = false;
      });
    }
  }
})();
