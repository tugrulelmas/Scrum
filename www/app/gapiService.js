angular.module('abioka')
.service('gapiService', ['$rootScope', 'context', function ($rootScope, context) {
    this.initGapi = function(postInitiation) {
      gapi.client.load('helloWorld', 'v1', postInitiation, restURL);
    };

    this.signOut = function(callback){
      var auth2 = gapi.auth2.getAuthInstance();
      auth2.signOut().then(function(){
        context.user.IsSignedIn = false;
        callback();
      });
    };

    this.renderSignInButton = function(id) {
      gapi.signin2.render(id, {
        'scope': 'profile email',
        'longtitle': false,
        'theme': 'dark',
        'onsuccess': onSuccess,
        'onfailure': onFailure
      });
    };

    function onSuccess(googleUser){
      var profile = googleUser.getBasicProfile();
      user = {
        "Id": profile.getId(),
        "Name": profile.getName(),
        "ImageUrl": profile.getImageUrl(),
        "Email": profile.getEmail(),
        "Token": googleUser.getAuthResponse().id_token,
        "IsSignedIn": true
      };

      Object.getOwnPropertyNames(user).forEach(function(val, idx, array) {
        context.user[val] = user[val];
      });

      $rootScope.$broadcast('userSignedIn', user);
    }

    function onFailure(reason){
      //TODO:alert below message
      console.log("login failed. reason: " + reason);
    }
}]);
