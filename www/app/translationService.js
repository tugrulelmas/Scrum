angular.module('abioka')

.service('translationService', ['$resource', 'userService', function ($resource, userService) {
    var resources = [];
    var resourceLoaded = false;

    function getRecources(languageFilePath, callback) {
        var sessionData = null;
        if (sessionStorage && sessionStorage.getItem(languageFilePath)) {
            sessionData = JSON.parse(sessionStorage.getItem(languageFilePath));
        }
        if (sessionData === null) {
            $resource(languageFilePath).get(function (data) {
                if (sessionStorage) {
                    sessionStorage.setItem(languageFilePath, JSON.stringify(data));
                }
                callback(data);
            });
        } else {
            callback(sessionData);
        }
    }

    this.setGlobalResources = function (callback) {
        resourceLoaded = false;
        var languageFilePath = "Resources/Resource" + "_" + userService.getUser().Language + '.json';
        getRecources(languageFilePath, function (data) {
            resources = data;
            resourceLoaded = true;
            if (callback) {
                callback();
            }
        });
    };

    this.getResource = function (resourceName) {
        var result = resources[resourceName];
        if (!result)
            return resourceName;

        return result;
    };
}]);
