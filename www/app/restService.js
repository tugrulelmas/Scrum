angular.module('abioka')

.factory('restService', ['$http', 'abiokaSettings', 'translationService', 'context', function ($http, abiokaSettings, translationService, context) {
    function setError(response, status, headers, config, callback) {
        var message = "";
        var statusReason = headers("Status-Reason");
        var closeWith = ['button'];
        if (statusReason === "validation-failed") {
            angular.forEach(response, function (validationMessage) {
                var errorMessage = getRecource(validationMessage.ErrorCode);
                var propertyName = getRecource(validationMessage.PropertyName);
                if (propertyName !== "") {
                    errorMessage = errorMessage.format(propertyName);
                }
                message += errorMessage + "<br/>";
            });
            closeWith = ['click'];
        } else {
            message = "<b>" + response.ExceptionMessage + "</b>";
        }

        noty({ text: message, layout: 'top', type: 'error', closeWith: closeWith, timeout: 15000 });
        callback(message, false);
    };

    function getRecource(key) {
        if (!key || key === "")
            return "";

        var result = translationService.getResource(key);
        if (!result) {
            result = key;
        }
        return result;
    }

    function setHeader(callback) {
      var abiokaTokenName = "ABIOKA-TOKEN";
      if (!$http.defaults.headers.common[abiokaTokenName]) {
          var userInfo = {"Token": context.user.Token, "Language": context.user.lang};
          var string = angular.toJson(userInfo);
          var encodedString = Base64.encode(string);
          $http.defaults.headers.common[abiokaTokenName] = encodedString;
      }
      callback();
    };

    function get(url, callback) {
        var serviceUrl = abiokaSettings.apiUrl + url;
        $http.get(serviceUrl)
        .success(function (response) {
            callback(response);
        })
        .error(function (response, status, headers, config) {
            setError(response, status, headers, config, callback);
        });
    };

    function post(url, request, callback) {
        var serviceUrl = abiokaSettings.apiUrl + url;
        $http.post(serviceUrl, request)
        .success(callback)
        .error(function (response, status, headers, config) {
            setError(response, status, headers, config, callback);
        });
    }

    function put(url, request, callback) {
        var serviceUrl = abiokaSettings.apiUrl + url;
        $http.put(serviceUrl, request)
        .success(callback)
        .error(function (response, status, headers, config) {
            setError(response, status, headers, config, callback);
        });
    }

    function remove(url, callback) {
        var serviceUrl = abiokaSettings.apiUrl + url;
        $http.delete(serviceUrl)
        .success(callback)
        .error(function (response, status, headers, config) {
            setError(response, status, headers, config, callback);
        });
    }

    return {
        get: function (url, callback) {
            setHeader(function () { get(url, callback) });
        },
        post: function (url, request, callback) {
            setHeader(function () { post(url, request, callback) });
        },
        put: function (url, request, callback) {
            setHeader(function () { put(url, request, callback) });
        },
        remove: function (url, callback) {
            setHeader(function () { remove(url, callback) });
        }
    }
}]);
