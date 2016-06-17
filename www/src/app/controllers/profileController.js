(function() {
  'use strict';

  angular.module('abioka')
    .controller('ProfileController', ProfileController);

  /* @ngInject */
  function ProfileController($http, translationService, userService) {
    var vm = this;
    BaseCtrl.call(this, vm, translationService);

    vm.user = userService.getUser();
    vm.save = save;

    function save() {
      vm.loading = true;
      $http.put('./User/update', vm.user).then(function() {
        vm.loading = false;
        userService.updateUser(vm.user);
        alert.info(vm.ml("ProfileSettingsAreUpdated"))
      }, function(reason) {
        vm.loading = false;
      });
    }
  }
})();
