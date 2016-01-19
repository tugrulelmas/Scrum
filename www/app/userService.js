angular.module('abioka')

.service('userService', ['$cookies', function($cookies) {
  var user = getDefault();

  this.getUser = function() {
    var userInfo = $cookies.getObject('userInfo');
    if (userInfo) {
      var now = new Date().getTime() / 1000;
      if (userInfo.ExparationDate > now) {
        //user = userInfo;
      } else {
        //TODO: go to login screen
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
    user.ExparationDate = tokenUser.exp;
    user.Token = token;
    user.IsSignedIn = true;
    if (angular.isUndefined(user.Language) || user.Language.trim() === "") {
      user.Language = getDefault().Language;
    }
    $cookies.putObject('userInfo', user);
    callback(user)
  };

  this.destroy = function() {
    user = getDefault();
    $cookies.remove('userInfo');
  }

  function getDefault() {
    return {
      Language: "en",
      IsSignedIn: false
    };
  };
}]);
