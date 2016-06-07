(function() {
  'use strict';

  angular.module('abioka')
    .controller('RegisterController', RegisterController);

  /* @ngInject */
  function RegisterController(translationService, localSignInService) {
    var vm = this;
    BaseCtrl.call(this, vm, translationService);

    vm.user = {};
    vm.signUp = signUp;

    function signUp() {
      vm.loading = true;
      localSignInService.signUp(vm.user).then(function() {
        vm.loading = false;
      }, function(reason) {
        vm.loading = false;
      });
    }
  }
})();
