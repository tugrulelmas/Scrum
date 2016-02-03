angular.module('abioka').controller('boardController', ['$scope', '$filter', '$stateParams', 'translationService', '$http', 'userService', '$state', function($scope, $filter, $stateParams, translationService, $http, userService, $state) {
  BaseCtrl.call(this, $scope, translationService);
  var boardId = $stateParams.boardId;

  $scope.showModal = false;
  $scope.loginUser = userService.getUser();
  $scope.estimatedPoints = [0, 0.5, 1, 2, 3, 5, 8, 13, 21];
  $scope.newCard = {};

  $scope.sortableOptions = {
    placeholder: "app",
    connectWith: '.project',
    update: function(e, ui) {
      if (ui.item.sortable.received)
        return;

      var targetModel = ui.item.sortable.droptargetModel;
      var originNgModel = ui.item.sortable.sourceModel;
      var itemModel = originNgModel[ui.item.sortable.index];
      var request = {
        CardId: ui.item.sortable.model.Id,
        CurrentIndex: ui.item.sortable.index,
        NewIndex: ui.item.sortable.dropindex,
        NewListId: ui.item.sortable.droptarget.scope().listItem.Id
      };

      $http.post("./Card/Move", request);
    }
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

  $scope.openDetail = function(listItem, card) {
    $http.get("./Card/" + card.Id + "/Comment").success(function(result) {
      card.Comments = result;

      $scope.selectedList = listItem;
      $scope.selectedCard = card;
      $scope.showModal = true;
      $scope.newComment = {};
      $state.go("board.detail", {cardId: card.Id});
    });
  }

  $scope.setLabel = function(label) {
    if (!$scope.selectedCard.Labels) {
      $scope.selectedCard.Labels = [];
    }

    var index = $scope.getIndex($scope.selectedCard.Labels, label, 'Id');
    if (index > -1) {
      $http.delete("./Card/" + $scope.selectedCard.Id + "/Label/" + label.Id).success(function(result) {
        $scope.selectedCard.Labels.splice(index, 1);
      });
    } else {
      $http.post("./Card/" + $scope.selectedCard.Id + "/Label/" + label.Id, null).success(function(result) {
        $scope.selectedCard.Labels.push(label);
      });
    }
  };

  $scope.setUser = function(user) {
    if (!$scope.selectedCard.Users) {
      $scope.selectedCard.Users = [];
    }

    var index = $scope.getIndex($scope.selectedCard.Users, user, 'Id');
    if (index > -1) {
      $http.delete("./Card/" + $scope.selectedCard.Id + "/User/" + user.Id).success(function(result) {
        $scope.selectedCard.Users.splice(index, 1);
      });
    } else {
      $http.post("./Card/" + $scope.selectedCard.Id + "/User/" + user.Id, null).success(function(result) {
        $scope.selectedCard.Users.push(user);
      });
    }
  };

  $scope.setEstimatedPoints = function(estimatedPoints) {
    $scope.selectedCard.EstimatedPoints = estimatedPoints;
    updateCard();
  };

  $scope.addComment = function() {
    if (!$scope.selectedCard.Comments) {
      $scope.selectedCard.Comments = [];
    }

    var newComment = {
      "Text": $scope.newComment.Text,
      "User": $scope.loginUser
    };
    $http.post("./Card/" + $scope.selectedCard.Id + "/Comment/", newComment).success(function(result) {
      result.User = $scope.loginUser;
      $scope.selectedCard.Comments.push(result);
      $scope.newComment = {};
    });
  };

  $scope.deleteComment = function(comment) {
    $http.delete("./Card/" + $scope.selectedCard.Id + "/Comment/" + comment.Id).success(function(result) {
      $scope.selectedCard.Comments.splice($scope.selectedCard.Comments.indexOf(comment), 1);
    });
  };

  $scope.saveTitle = function() {
    updateCard();
  };

  $scope.addCard = function(listItem) {
    if (!listItem.Cards) {
      listItem.Cards = [];
    }
    $scope.newCard.EstimatedPoints= 0;
    $scope.newCard.ListId = listItem.Id;
    $scope.newCard.Order = listItem.Cards.length;

    $scope.selectedList = listItem;
    $http.post("./Card", $scope.newCard).success(function(result) {
      $scope.selectedCard = result;
      listItem.Cards.push(result);
      listItem.showNewCard = false;
      $scope.newCard = {};
    });
  };

  $scope.deleteCard = function() {
    $http.delete("./Card/" + $scope.selectedCard.Id).success(function(result) {
      $scope.selectedList.Cards.splice($scope.selectedList.Cards.indexOf($scope.selectedCard), 1);
      $scope.showModal = false;
    });
  };

  $scope.deleteList = function(listItem) {
    $http.delete("./List/" + listItem.Id).success(function(result) {
      $scope.list.splice($scope.list.indexOf(listItem), 1);
    });
  };

  $scope.addList = function() {
    var newList = {
      "Name": $scope.newListTitle,
      "BoardId": boardId,
      "Cards": []
    };

    $http.post("./List", newList).success(function(result) {
      $scope.list.push(result);
      $scope.newListTitle = null;
    });
  };

  $scope.addNewCard = function(listItem){
    listItem.showNewCard = true;
  };

  $scope.cancelNewCard = function(listItem){
    listItem.showNewCard = false;
    $scope.newCard = {};
  };

  $scope.afterModalClosing = function(){
    $state.go("board");
  };

  function updateCard() {
    $http.put("./Card/" + $scope.selectedCard.Id, $scope.selectedCard);
  }

  function init() {
    $http.get("./Board/" + boardId + "/List").success(function(result) {
      $scope.list = result;
      if($state.current.name === "board.detail"){
        var cardId = $state.params.cardId;
        $http.get("./Card/" + cardId).success(function(result) {
          var listIndex = $scope.getIndex($scope.list, { 'Id': result.ListId }, 'Id');
          var list = $scope.list[listIndex];
          $scope.openDetail(listIndex, result);
        });
      }
    });
    $http.get("./Board/" + boardId +"/User").success(function(result) {
      $scope.users = result;
    });
    $http.get("./Label").success(function(result) {
      $scope.labels = result;
    });
  }

  init();
}]);
