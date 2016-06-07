(function() {
  'use strict';

  angular.module('abioka')
    .service('userService', userService);

  /* @ngInject */
  function userService($cookies, $rootScope) {
    var user = getDefault();

    var service = {
      getUser: getUser,
      setUser: setUser,
      updateUser: updateUser,
      destroy: destroy
    };

    return service;

    function getUser() {
      var userInfo = $cookies.getObject('userInfo');
      if (userInfo && userInfo.IsSignedIn === true) {
        var now = parseInt(new Date().getTime() / 1000);
        if (userInfo.ExpirationDate > now) {
          //TODO: check if the token same as the token stored in db.
          user = userInfo;
        } else {
          destroy();
          $rootScope.$broadcast('userSignedOut');
        }
      }
      return user;
    }

    function setUser(token, callback) {
      var payload = Base64.decode(token.split('.')[1]);
      var tokenUser = angular.fromJson(payload);

      user.Name = tokenUser.name;
      user.Id = tokenUser.id;
      user.Email = tokenUser.email;
      user.ImageUrl = tokenUser.image_url;
      user.Initials = tokenUser.initials;
      user.Provider = tokenUser.provider;
      user.ExpirationDate = tokenUser.exp;
      user.Token = token;
      user.IsSignedIn = true;
      if (angular.isUndefined(user.Language) || user.Language.trim() === "") {
        user.Language = getDefault().Language;
      }
      $cookies.putObject('userInfo', user);
      callback(user);
    };

    function updateUser(userInfo) {
      user.Name = userInfo.Name;
      user.Id = userInfo.Id;
      user.Email = userInfo.Email;
      user.ImageUrl = userInfo.ImageUrl;
      user.Initials = userInfo.Initials;
      user.Provider = userInfo.Provider;
      user.ExpirationDate = userInfo.ExpirationDate;
      user.Token = userInfo.Token;
      user.IsSignedIn = userInfo.IsSignedIn;
      user.Language = userInfo.Language;
      $cookies.remove('userInfo');
      $cookies.putObject('userInfo', user);
      $rootScope.$broadcast('userUpdated');
    };

    function destroy() {
      var oldLanguage = user.Language;
      user = getDefault();
      user.Language = oldLanguage;
      $cookies.remove('userInfo');
    }

    function getDefault() {
      return {
        Language: "en",
        IsSignedIn: false
      };
    };
  }
})();
