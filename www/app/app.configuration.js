(function() {
  'use strict';

  angular.module('abioka')
    .config(config);

  config.$inject = ['$httpProvider'];

  function config($httpProvider) {
    $httpProvider.interceptors.push('tokenInjector');
    $httpProvider.interceptors.push('errorInjector');
  }
})();
