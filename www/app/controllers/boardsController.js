(function() {
  'use strict';

  angular.module('abioka')
    .controller('BoardsController', BoardsController);

  BoardsController.$inject = ['translationService', '$http'];

  function BoardsController(translationService, $http) {
    var vm = this;
    BaseCtrl.call(this, vm, translationService);

    vm.addBoard = addBoard;
    vm.addUser = addUser;
    vm.deleteBoard = deleteBoard;

    activate();

    function activate() {
      $http.get("./Board").success(function(result) {
        vm.boards = result;
      });
      $http.get("./User/Params?loadAllUsers=false").success(function(result) {
        vm.users = result;
      });
    }

    function addBoard() {
      var board = {
        "Name": vm.newBoardName
      };
      $http.post("./Board/Add", board).success(function(result) {
        vm.boards.unshift(result);
        vm.newBoardName = "";
      });
    };

    function addUser(board, user) {
      if (!board.Users) {
        board.Users = [];
      }

      var index = vm.getIndex(board.Users, user, 'Id');
      if (index > -1) {
        $http.delete("./Board/" + board.Id + "/User/" + user.Id).success(function(result) {
          board.Users.splice(index, 1);
          $("#menu" + board.Id).dropdown('toggle');
        });
      } else {
        $http.post("./Board/" + board.Id + "/User/" + user.Id, null).success(function(result) {
          board.Users.push(user);
          $("#menu" + board.Id).dropdown('toggle');
        });
      }
    };

    function deleteBoard(board) {
      $http.delete("./Board/" + board.Id).success(function(result) {
        vm.boards.splice(vm.boards.indexOf(board), 1);
      });
    }
  }
})();
