angular.module('abioka')

.service('userService', ['$cookies', '$rootScope', function($cookies, $rootScope) {
  var user = getDefault();

  this.getUser = function() {
    var userInfo = $cookies.getObject('userInfo');
    if (userInfo) {
      var now = parseInt(new Date().getTime() / 1000);
      if (userInfo.ExpirationDate > now) {
        user = userInfo;
      } else {
        this.destroy();
        $rootScope.$broadcast('userSignedOut');
      }
    }
    return user;
  };

  this.setUser = function(token, callback) {
    var payload = Base64.decode(token.split('.')[1]);
    var tokenUser = angular.fromJson(payload);

    user.Name = tokenUser.name;
    user.Email = tokenUser.email;
    user.ImageUrl = tokenUser.imageUrl;
    user.ShortName = tokenUser.shortName;
    user.Provider = tokenUser.provider;
    user.ExpirationDate = tokenUser.exp;
    user.Token = token;
    user.IsSignedIn = true;
    if (angular.isUndefined(user.Language) || user.Language.trim() === "") {
      user.Language = getDefault().Language;
    }
    $cookies.putObject('userInfo', user);
    callback(user)
  };

  this.destroy = function() {
    var oldLanguage = user.Language;
    user = getDefault();
    user.Language = oldLanguage;
    $cookies.remove('userInfo');
  }

  this.setLanguage = function(language){
    var tmpUser = this.getUser();
    tmpUser.Language = language;
    $cookies.remove('userInfo');
    $cookies.putObject('userInfo', user);
  }

  function getDefault() {
    return {
      Language: "en",
      IsSignedIn: false
    };
  };
}]);
