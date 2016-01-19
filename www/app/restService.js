angular.module('abioka')

.factory('restService', ['$http', '$q', 'abiokaSettings', 'translationService', 'userService', function($http, $q, abiokaSettings, translationService, userService) {
  function setError(response, status, headers, config) {
    var message = "";
    var statusReason = headers("Status-Reason");
    var closeWith = ['button'];
    if (statusReason === "validation-failed") {
      angular.forEach(response, function(validationMessage) {
        var errorMessage = getRecource(validationMessage.ErrorCode);
        var args = [];
        angular.forEach(validationMessage.Args, function(arg) {
          var text = arg.Name;
          if (arg.IsLocalizable) {
            text = getRecource(arg.Name);
          }
          args.push(text);
        });
        var text = getRecource(validationMessage.Text);
        if (args.length > 0) {
          errorMessage = errorMessage.format.apply(errorMessage, args);
        }
        message += errorMessage + "<br/>";
      });
      closeWith = ['click'];
    } else if (response && response.ExceptionMessage) {
      message = "<b>" + response.ExceptionMessage + "</b>";
    } else {
      message = angular.toJson(response);
    }
    alert.error(message, closeWith);
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

  function setHeader() {
    if (!$http.defaults.headers.common["Authorization"]) {
      var user = userService.getUser();
      if (user && user.IsSignedIn === true) {
        $http.defaults.headers.common["Authorization"] =  "Bearer " + user.Token;
      }
    }
  };

  function get(url, resolve, reject) {
    var serviceUrl = abiokaSettings.apiUrl + url;
    $http.get(serviceUrl)
      .success(function(response) {
        resolve(response);
      })
      .error(function(response, status, headers, config) {
        setError(response, status, headers, config);
        reject(response);
      });
  };

  function post(url, request, resolve, reject) {
    var serviceUrl = abiokaSettings.apiUrl + url;
    $http.post(serviceUrl, request)
      .success(resolve)
      .error(function(response, status, headers, config) {
        setError(response, status, headers, config);
        reject(response);
      });
  }

  function put(url, request, resolve, reject) {
    var serviceUrl = abiokaSettings.apiUrl + url;
    $http.put(serviceUrl, request)
      .success(resolve)
      .error(function(response, status, headers, config) {
        setError(response, status, headers, config);
        reject(response);
      });
  }

  function remove(url, resolve, reject) {
    var serviceUrl = abiokaSettings.apiUrl + url;
    $http.delete(serviceUrl)
      .success(resolve)
      .error(function(response, status, headers, config) {
        setError(response, status, headers, config);
        reject(response);
      });
  }

  function getPromisedResult(callback) {
    setHeader();
    var deferred = $q.defer();
    callback(deferred.resolve, deferred.reject);
    return deferred.promise;
  }

  return {
    get: function(url) {
      return getPromisedResult(function(resolve, reject) {
        get(url, resolve, reject);
      });
    },
    post: function(url, request) {
      return getPromisedResult(function(resolve, reject) {
        post(url, request, resolve, reject);
      });
    },
    put: function(url, request) {
      return getPromisedResult(function(resolve, reject) {
        put(url, request, resolve, reject);
      });
    },
    remove: function(url) {
      return getPromisedResult(function(resolve, reject) {
        remove(url, resolve, reject);
      });
    }
  }
}]);
