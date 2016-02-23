(function() {
  'use strict';

  angular.module('abioka')
    .controller('ChangePasswordController', ChangePasswordController);

  ChangePasswordController.$inject = ['$http', 'translationService', 'userService'];

  function ChangePasswordController($http, translationService, userService) {
    var vm = this;
    BaseCtrl.call(this, vm, translationService);

    vm.save = save;

    activate();

    function activate() {
      setDefault();
    }

    function save() {
      vm.loading = true;
      $http.put('./User/ChangePassword', vm.user).then(function() {
        vm.loading = false;
        setDefault();
        vm.changePasswordForm.$setUntouched();
        alert.info(vm.ml("PasswordIsChanged"))
      }, function(reason) {
        vm.loading = false;
      });
    }

    function setDefault() {
      vm.user = {};
      vm.user.Id = userService.getUser().Id;
    }
  }
})();
