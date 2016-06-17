(function() {
  'use strict';

  angular.module('abioka')
    .controller('GlobalController', GlobalController);

  /* @ngInject */
  function GlobalController($scope, $location, translationService, authService, userService, initializerService) {
    var vm = this;
    translationService.setGlobalResources();
    BaseCtrl.call(this, vm, translationService);
    initializerService.initialize();

    vm.user = userService.getUser();
    vm.changeLanguage = changeLanguage;
    vm.signOut = signOut;

    function changeLanguage(language) {
      var oldLanguage = userService.getUser().Language;
      if (oldLanguage !== language) {
        vm.user.Language = language;
        userService.updateUser(vm.user);
        translationService.setGlobalResources(function() {
          alert.info(vm.ml("LanguageChangedMessage"));
        });
      }
    }

    function signOut() {
      authService.logout();
    };

    $scope.$on('userSignedOut', function() {
      vm.user = userService.getUser();
      $location.path("/login");
    });

    $scope.$on('userUpdated', function() {
      vm.user = userService.getUser();
    });
  }
})();
