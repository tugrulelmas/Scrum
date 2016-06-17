  function BaseCtrl(vm, translationService) {
    "use strict";

    vm.ml = ml; 
    vm.includes = includes;
    vm.getIndex = getIndex;

    function includes(list, value, propertyName) {
      return vm.getIndex(list, value, propertyName) > -1;
    }

    function getIndex(list, value, propertyName) {
      if (!list)
        return -1;

      return list.map(function(e) {
        return e[propertyName]
      }).indexOf(value[propertyName]);
    }

    function ml(resourceName) {
      return translationService.getResource(resourceName);
    }
  }
