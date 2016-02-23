(function() {
  'use strict';

  angular.module('abioka')
    .factory('errorInjector', errorInjector);

  errorInjector.$inject = ['$rootScope', '$q', '$injector'];

  function errorInjector($rootScope, $q, $injector) {
    var service = {
      responseError: responseError
    }
    return service;

    function responseError(rejection) {
      if (rejection.status === 401) {
        userService.destroy();
        $rootScope.$broadcast('userSignedOut', null);
        return $q.reject(rejection);
      }

      var message = "";
      var statusReason = rejection.headers("Status-Reason");
      var closeWith = ['button'];
      if (statusReason === "validation-failed") {
        var translationService = $injector.get('translationService');
        angular.forEach(rejection.data, function(validationMessage) {
          var errorMessage = translationService.getResource(validationMessage.ErrorCode);
          var args = [];
          angular.forEach(validationMessage.Args, function(arg) {
            var text = arg.Name;
            if (arg.IsLocalizable) {
              text = translationService.getResource(arg.Name);
            }
            args.push(text);
          });
          var text = translationService.getResource(validationMessage.Text);
          if (args.length > 0) {
            errorMessage = errorMessage.format.apply(errorMessage, args);
          }
          message += errorMessage + "<br/>";
        });
        closeWith = ['click'];
      } else if (rejection.data && rejection.data.Message) {
        message = rejection.data.Message;
      } else if (rejection.data) {
        message = rejection.data;
      } else {
        message = angular.toJson(rejection);
      }
      alert.error(message, closeWith);

      return $q.reject(rejection);
    }
  }
})();
