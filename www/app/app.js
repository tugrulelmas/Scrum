
angular.module('abioka', ['ngRoute', 'ngResource', 'ngCookies', 'ui.sortable', 'directive.g+signin', 'ngMessages', 'ui.router'])
  .config(['$stateProvider', '$urlRouterProvider', function($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/boards');

    $stateProvider
    .state('boards', { url: '/boards', templateUrl: 'Views/boards.html', controller: 'boardsController' })
    .state('board', { url: '/board/:boardId', templateUrl: 'Views/board.html', controller: 'boardController' })
    .state('board.detail', { url: '/card/:cardId', templateUrl: 'Views/card.html' })
    .state('profile', { url: '/profile', templateUrl: 'Views/profile.html', controller: 'profileController' })
    .state('changePassword', { url: '/changePassword', templateUrl: 'Views/changePassword.html', controller: 'changePasswordController' })
    .state('login', { url: '/login', templateUrl: 'Views/login.html', controller: 'loginController', isPublic: true })
    .state('register', { url: '/register', templateUrl: 'Views/register.html', controller: 'registerController', isPublic: true });
  }])
  .config(['$httpProvider', function($httpProvider) {
    $httpProvider.interceptors.push('tokenInjector');
    $httpProvider.interceptors.push('errorInjector');
  }])
  .run(['$rootScope', 'userService', '$state', '$stateParams', function($rootScope, userService, $state, $stateParams) {
    $rootScope.$on('$stateChangeStart', function(e, toState, toParams, fromState, fromParams) {
    var user = userService.getUser();
    if (toState.isPublic !== true && !user.IsSignedIn) {
        e.preventDefault();
        $state.transitionTo("login", null, {notify:false});
        $state.go("login");
      }
    });
  }]);
