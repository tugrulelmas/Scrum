angular.module('abioka').controller('boardsController', ['$scope', 'translationService', '$http', function($scope, translationService, $http) {
  BaseCtrl.call(this, $scope, translationService);

  $scope.addBoard = function() {
    var board = {
      "Name": $scope.newBoardName
    };
    $http.post("./Board/Add", board).success(function(result) {
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

  $scope.deleteBoard = function(board) {
    $http.delete("./Board/" + board.Id).success(function(result) {
      $scope.boards.splice($scope.boards.indexOf(board), 1);
    });
  };

  function init() {
    $http.get("./Board").success(function(result) {
      $scope.boards = result;
    });
    $http.get("./User/Params?loadAllUsers=false").success(function(result) {
      $scope.users = result;
    });
  }

  init();
}]);
