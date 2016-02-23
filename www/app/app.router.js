(function() {
  'use strict';
  
  angular.module('abioka.router', ['ui.router'])
    .config(routeConfig);

  routeConfig.$inject = ['$stateProvider', '$urlRouterProvider'];

  function routeConfig($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/boards');

    $stateProvider
      .state('boards', {
        url: '/boards',
        templateUrl: 'Views/boards.html',
        controller: 'BoardsController',
        controllerAs: 'vm'
      })
      .state('board', {
        url: '/board/:boardId',
        templateUrl: 'Views/board.html',
        controller: 'BoardController',
        controllerAs: 'vm'
      })
      .state('board.detail', {
        url: '/card/:cardId',
        templateUrl: 'Views/card.html',
        controllerAs: 'vm'
      })
      .state('profile', {
        url: '/profile',
        templateUrl: 'Views/profile.html',
        controller: 'ProfileController',
        controllerAs: 'vm'
      })
      .state('changePassword', {
        url: '/changePassword',
        templateUrl: 'Views/changePassword.html',
        controller: 'ChangePasswordController',
        controllerAs: 'vm'
      })
      .state('login', {
        url: '/login',
        templateUrl: 'Views/login.html',
        controller: 'LoginController',
        controllerAs: 'vm',
        isPublic: true
      })
      .state('register', {
        url: '/register',
        templateUrl: 'Views/register.html',
        controller: 'RegisterController',
        controllerAs: 'vm',
        isPublic: true
      });
  }
})();
