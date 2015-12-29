angular.module('abioka').controller('boardsController', ['$scope', 'translationService', 'restService', function($scope, translationService, restService){
  BaseCtrl.call(this, $scope, translationService);

    $scope.addBoard = function(){
      var board = {"Name": $scope.newBoardName};
        restService.post("Board/Add", board, function (result) {
            $scope.boards.unshift(result);
            $scope.newBoardName = "";
        });
    };

    $scope.addUser = function(board, user){
    		if(!board.Users){
    			board.Users = [];
    		}

    		if($scope.includes(board.Users, user, 'Email')){
          restService.remove("Board/" + board.Id + "/DeleteUser?userEmail=" + user.Email, function (result) {
    			     board.Users.splice(board.Users.indexOf(user), 1);
               $("#menu" + board.Id).dropdown('toggle');
          });
    		} else{
          restService.post("Board/" + board.Id + "/AddUser?userEmail=" + user.Email, null, function (result) {
      			   board.Users.push(user);
               $("#menu" + board.Id).dropdown('toggle');
          });
    		}
    };

    $scope.deleteBoard = function(board){
        restService.put("Board/Delete?d=y", board, function (result) {
          $scope.boards.splice($scope.boards.indexOf(board), 1);
        });
    };

    function init() {
        restService.get("Board", function (result) {
            $scope.boards = result;
        });
        restService.get("User/Params?loadAllUsers=false", function (result) {
            $scope.users = result;
        });
    }

    init();
}]);
