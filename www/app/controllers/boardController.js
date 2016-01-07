angular.module('abioka').controller('boardController', ['$scope', '$filter', '$routeParams', 'translationService', 'restService', 'context', function($scope, $filter, $routeParams, translationService, restService, context){
  BaseCtrl.call(this, $scope, translationService);
  var boardId = parseInt($routeParams.boardId);
  $scope.loginUser = context.user;

  $scope.sortableOptions  = {
      connectWith: '.project'
  };

	$scope.getTotalEstimatedPoints = function(cards){
		var filteredCards = $filter('filter')(cards, $scope.search);

		if(!filteredCards || filteredCards.length === 0)
			return "";

		var result = 0;
		angular.forEach(filteredCards, function(card){
			result += card.EstimatedPoints;
		});
		return result;
	};

	$scope.getUserNames = function(card){
    if(!card.Users)
      return "";

		return card.Users.map(function(e){return e.Name}).join(',');
	};

	$scope.setSelecteds = function(listItem, card){
		$scope.selectedList = listItem;
		$scope.selectedCard = card;
		$scope.showModal();
	}

	$scope.setLabel = function(label){
		if(!$scope.selectedCard.Labels){
			$scope.selectedCard.Labels = [];
		}

		if($scope.includes($scope.selectedCard.Labels, label, 'Name')){
			$scope.selectedCard.Labels.splice($scope.selectedCard.Labels.indexOf(label), 1);
		} else{
			$scope.selectedCard.Labels.push(label);
		}
	};

	$scope.setUser = function(user){
		if(!$scope.selectedCard.Users){
			$scope.selectedCard.Users = [];
		}

		if($scope.includes($scope.selectedCard.Users, user, 'Name')){
			$scope.selectedCard.Users.splice($scope.selectedCard.Users.indexOf(user), 1);
		} else{
			$scope.selectedCard.Users.push(user);
		}
	};

	$scope.setEstimatedPoints = function(estimatedPoints){
		$scope.selectedCard.EstimatedPoints = estimatedPoints;
	};

	$scope.addComment = function(){
		if(!$scope.selectedCard.Comments){
			$scope.selectedCard.Comments = [];
		}

		$scope.selectedCard.Comments.push({"Text": $scope.newComment, "User": context.user, "CreateDate": new Date()});
		$scope.newComment = null;
	};

	$scope.deleteComment = function(comment){
		$scope.selectedCard.Comments.splice($scope.selectedCard.Comments.indexOf(comment), 1);
	};

	$scope.saveTitle = function(){
		//$scope.selectedCard.Title;
	};

	$scope.addCard = function(listItem){
		$scope.selectedCard = {"Users":[], "Labels":[]};
		$scope.selectedList = listItem;
		listItem.Cards.push($scope.selectedCard);
		$scope.showModal();
	};

	$scope.deleteCard = function(){
		$scope.selectedList.Cards.splice($scope.selectedList.indexOf($scope.selectedCard), 1);
		$scope.hideModal();
	};

	$scope.deleteList = function(listItem){
		$scope.list.splice($scope.list.indexOf(listItem), 1);
	};

	$scope.addList = function(){
		var newList = {"Name": $scope.newListTitle, "Cards": []};
		$scope.list.push(newList);
		$scope.newListTitle = null;
	};

  function init(){
      restService.get("User").then(function (result) {
          $scope.users = result;
      });
      restService.get("Label").then(function (result) {
          $scope.labels = result;
      });
      restService.get("Board/" + boardId).then(function (result) {
          $scope.list = result.Lists;
      });
  }

  init();
}]);
