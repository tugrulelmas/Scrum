angular.module('abioka').controller('boardController', ['$scope', '$filter', '$routeParams', 'translationService', 'restService', 'userService', function($scope, $filter, $routeParams, translationService, restService, userService) {
  BaseCtrl.call(this, $scope, translationService);
  var boardId = $routeParams.boardId;

  $scope.showModal = false;
  $scope.loginUser = userService.getUser();

  $scope.sortableOptions = {
    connectWith: '.project'
  };

  $scope.getTotalEstimatedPoints = function(cards) {
    var filteredCards = $filter('filter')(cards, $scope.search);

    if (!filteredCards || filteredCards.length === 0)
      return "";

    var result = 0;
    angular.forEach(filteredCards, function(card) {
      result += card.EstimatedPoints;
    });
    return result;
  };

  $scope.getUserNames = function(card) {
    if (!card.Users)
      return "";

    return card.Users.map(function(e) {
      return e.Name
    }).join(',');
  };

  $scope.setSelecteds = function(listItem, card) {
    $scope.selectedList = listItem;
    $scope.selectedCard = card;
    $scope.showModal = true;
    $scope.newComment = {};
  }

  $scope.setLabel = function(label) {
    if (!$scope.selectedCard.Labels) {
      $scope.selectedCard.Labels = [];
    }

    var index = $scope.getIndex($scope.selectedCard.Labels, label, 'Id');
    if (index > -1) {
      $scope.selectedCard.Labels.splice(index, 1);
    } else {
      $scope.selectedCard.Labels.push(label);
    }
  };

  $scope.setUser = function(user) {
    if (!$scope.selectedCard.Users) {
      $scope.selectedCard.Users = [];
    }

    var index = $scope.getIndex($scope.selectedCard.Users, user, 'Id');
    if (index > -1) {
      $scope.selectedCard.Users.splice(index, 1);
    } else {
      $scope.selectedCard.Users.push(user);
    }
  };

  $scope.setEstimatedPoints = function(estimatedPoints) {
    $scope.selectedCard.EstimatedPoints = estimatedPoints;
  };

  $scope.addComment = function() {
    if (!$scope.selectedCard.Comments) {
      $scope.selectedCard.Comments = [];
    }

    $scope.selectedCard.Comments.push({
      "Text": $scope.newComment.Text,
      "User": $scope.loginUser,
      "CreateDate": new Date()
    });
    $scope.newComment = {};
  };

  $scope.deleteComment = function(comment) {
    $scope.selectedCard.Comments.splice($scope.selectedCard.Comments.indexOf(comment), 1);
  };

  $scope.saveTitle = function() {
    //$scope.selectedCard.Title;
  };

  $scope.addCard = function(listItem) {
    $scope.selectedCard = {
      "Users": [],
      "Labels": []
    };
    $scope.selectedList = listItem;
    listItem.Cards.push($scope.selectedCard);
    $scope.showModal = true;
  };

  $scope.deleteCard = function() {
    $scope.selectedList.Cards.splice($scope.selectedList.indexOf($scope.selectedCard), 1);
    $scope.showModal = false;
  };

  $scope.deleteList = function(listItem) {
    $scope.list.splice($scope.list.indexOf(listItem), 1);
  };

  $scope.addList = function() {
    var newList = {
      "Name": $scope.newListTitle,
      "Cards": []
    };
    $scope.list.push(newList);
    $scope.newListTitle = null;
  };

  function init() {
    restService.get("User").then(function(result) {
      $scope.users = result;
    });
    restService.get("Label").then(function(result) {
      $scope.labels = result;
    });
    restService.get("Board/" + boardId).then(function(result) {
      $scope.list = result.Lists;
    });
  }

  init();
}]);
