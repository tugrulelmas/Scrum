(function() {
  'use strict';

  angular.module('abioka')
    .service('translationService', translationService);

  /* @ngInject */
  function translationService($resource, userService) {
    var resources = [];
    var resourceLoaded = false;

    var service = {
      getResource: getResource,
      setGlobalResources: setGlobalResources
    };
    return service;

    function getRecourcesFromFileOrCache(languageFilePath, callback) {
      var sessionData = null;
      if (sessionStorage && sessionStorage.getItem(languageFilePath)) {
        sessionData = JSON.parse(sessionStorage.getItem(languageFilePath));
      }
      if (sessionData === null) {
        $resource(languageFilePath).get(function(data) {
          if (sessionStorage) {
            sessionStorage.setItem(languageFilePath, JSON.stringify(data));
          }
          callback(data);
        });
      } else {
        callback(sessionData);
      }
    }

    function setGlobalResources(callback) {
      resourceLoaded = false;
      var languageFilePath = "Resources/Resource" + "_" + userService.getUser().Language + '.json';
      getRecourcesFromFileOrCache(languageFilePath, function(data) {
        resources = data;
        resourceLoaded = true;
        if (callback) {
          callback();
        }
      });
    };

    function getResource(resourceName) {
      var result = resources[resourceName];
      if (!result)
        return resourceName;

      return result;
    };
  }
})();
