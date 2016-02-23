(function() {
  'use strict';

  angular.module('abioka')
    .directive('validationMessage', validationMessage);

  function validationMessage() {
    var directive = {
      restrict: 'EA',
      templateUrl: 'Views/Partials/messages.html',
      scope: {
        fieldName: '@',
        model: '='
      },
      controller: validationMessageController,
      controllerAs: 'vm',
      bindToController: true
    };
    return directive;
  }

  validationMessageController.$inject = ['translationService'];

  function validationMessageController(translationService) {
    var vm = this;
    BaseCtrl.call(this, vm, translationService);
  }
})();
