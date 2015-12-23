angular.module('abioka').controller('boardsController', ['$scope', 'translationService', function($scope, translationService){
  BaseCtrl.call(this, $scope, translationService);

    //TODO: get from service
    $scope.boards = [{"Name": "Altyapı-13", "Id": "13"}, {"Name": "Altyapı-12", "Id": "12"}];
    $scope.users = [{"Name": "Tuğrul"}, {"Name": "Fırat"}, {"Name": "Emrah"}];

    $scope.addBoard = function(){
        //TODO: call service
        $scope.boards.unshift({"Name": $scope.newBoardName, "Id": "14"});
        $scope.newBoardName = "";
    };

    $scope.addUser = function(board, user){
    		if(!board.Users){
    			board.Users = [];
    		}

    		if($scope.includes(board.Users, user, 'Name')){
          //TODO: call service
    			board.Users.splice(board.Users.indexOf(user), 1);
    		} else{
          //TODO: call service
    			board.Users.push(user);
    		}
        $('.dropdown-menu').dropdown('toggle');
    };

    $scope.deleteBoard = function(board){
        //TODO: call service
        $scope.boards.splice($scope.boards.indexOf(board), 1);
    };
}]);
