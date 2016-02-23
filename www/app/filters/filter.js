(function() {
  'use strict';

  angular.module('abioka')
    .filter('formattedDate', formattedDate);

  formattedDate.$inject = ["$filter", "translationService"];

  function formattedDate($filter, translationService) {
    return function(input, formatName) {
      if (input == null) {
        return "";
      }
      var format = "";
      if (!formatName || formatName === "") {
        format = translationService.getResource("LongDateFormat");
      } else {
        format = translationService.getResource(formatName);
      }

      var _date = $filter('date')(new Date(input), format);
      return _date.toUpperCase();
    }
  }
})();
