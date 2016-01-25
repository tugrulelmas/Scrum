angular.module('abioka').controller('boardsController', ['$scope', 'translationService', 'restService', function($scope, translationService, restService) {
  BaseCtrl.call(this, $scope, translationService);

  $scope.addBoard = function() {
    var board = {
      "Name": $scope.newBoardName
    };
    restService.post("Board/Add", board).then(function(result) {
      $scope.boards.unshift(result);
      $scope.newBoardName = "";
    });
  };

  $scope.addUser = function(board, user) {
    if (!board.Users) {
      board.Users = [];
    }

    var index = $scope.getIndex(board.Users, user, 'Id');
    if (index > -1) {
      restService.remove("Board/" + board.Id + "/DeleteUser?userId=" + user.Id).then(function(result) {
        board.Users.splice(index, 1);
        $("#menu" + board.Id).dropdown('toggle');
      });
    } else {
      restService.post("Board/" + board.Id + "/AddUser?userId=" + user.Id, null).then(function(result) {
        board.Users.push(user);
        $("#menu" + board.Id).dropdown('toggle');
      });
    }
  };

  $scope.deleteBoard = function(board) {
    restService.put("Board/Delete?d=y", board).then(function(result) {
      $scope.boards.splice($scope.boards.indexOf(board), 1);
    });
  };

  function init() {
    restService.get("Board").then(function(result) {
      $scope.boards = result;
    });
    restService.get("User/Params?loadAllUsers=false").then(function(result) {
      $scope.users = result;
    });
  }

  init();
}]);
