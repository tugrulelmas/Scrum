angular.module('abioka').controller('boardController', ['$scope', '$filter', 'translationService', 'context', function($scope, $filter, translationService, context){
  BaseCtrl.call(this, $scope, translationService);
	$scope.list = [{"Title": "ToDo",
						"Cards": [
							{"Title": "Paket giriş ekranı", "EstimatedPoints": 2, "Users": [{"Name": "Tuğrul"}],
								"Labels": [{"Title": "YeniBeamer", "Type": "success"}, {"Title": "SSO", "Type": "info"}]}
							]},
				   {"Title": "Doing",
						"Cards": []},
				   {"Title": "Testing",
						"Cards": [
							{"Title": "CUSTODY tanimlarinin tasinmasi", "EstimatedPoints": 2, "Users": [{"Name": "Fırat"}],
                "Comments": [{"Text": "yapıcaz bunu", "User": {"Name": "Test User", "Email": "a", "ImageUrl": "adsas"}, "CreateDate": new Date()}]}
						]},
				   {"Title": "Done",
						"Cards": [
							{"Title": "Host tanım ekranlarının yazılması", "EstimatedPoints": 2, "Users": [{"Name": "Fırat"},{"Name": "Tuğrul"}]},
							{"Title": "Dapper Değişiklikleri", "EstimatedPoints": 2, "Users": [{"Name": "Tuğrul"}]}
						]}
				];

  $scope.labels = [{"Title": "YeniBeamer", "Type": "success"},
                 {"Title": "SSO", "Type": "info"}];
  $scope.users = [{"Name": "Tuğrul"}, {"Name": "Fırat"}, {"Name": "Emrah"}];

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

		if($scope.includes($scope.selectedCard.Labels, label, 'Title')){
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
		var newList = {"Title": $scope.newListTitle, "Cards": []};
		$scope.list.push(newList);
		$scope.newListTitle = null;
	};
}]);
