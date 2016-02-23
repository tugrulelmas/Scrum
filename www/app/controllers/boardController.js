(function() {
  'use strict';

  angular.module('abioka')
    .controller('BoardController', BoardController);

  BoardController.$inject = ['$filter', '$stateParams', 'translationService', '$http', 'userService', '$state'];

  function BoardController($filter, $stateParams, translationService, $http, userService, $state) {
    var vm = this;
    BaseCtrl.call(this, vm, translationService);

    var boardId = $stateParams.boardId;
    vm.sortableOptions = {
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

    vm.showModal = false;
    vm.loginUser = userService.getUser();
    vm.estimatedPoints = [0, 0.5, 1, 2, 3, 5, 8, 13, 21];
    vm.newCard = {};
    vm.getTotalEstimatedPoints = getTotalEstimatedPoints;
    vm.getUserNames = getUserNames;
    vm.openDetail = openDetail;
    vm.setLabel = setLabel;
    vm.setUser = setUser;
    vm.setEstimatedPoints = setEstimatedPoints;
    vm.addComment = addComment;
    vm.deleteComment = deleteComment;
    vm.saveTitle = saveTitle;
    vm.addCard = addCard;
    vm.deleteCard = deleteCard;
    vm.deleteList = deleteList;
    vm.addList = addList;
    vm.addNewCard = addNewCard;
    vm.cancelNewCard = cancelNewCard;
    vm.afterModalClosing = afterModalClosing;

    activate();

    function activate() {
      $http.get("./Board/" + boardId + "/List").success(function(result) {
        vm.list = result;
        if ($state.current.name === "board.detail") {
          var cardId = $state.params.cardId;
          $http.get("./Card/" + cardId).success(function(result) {
            var listIndex = vm.getIndex(vm.list, {
              'Id': result.ListId
            }, 'Id');
            var list = vm.list[listIndex];
            openDetail(listIndex, result);
          });
        }
      });
      $http.get("./Board/" + boardId + "/User").success(function(result) {
        vm.users = result;
      });
      $http.get("./Label").success(function(result) {
        vm.labels = result;
      });
    }

    function getTotalEstimatedPoints(cards) {
      var filteredCards = $filter('filter')(cards, vm.search);

      if (!filteredCards || filteredCards.length === 0)
        return "";

      var result = 0;
      angular.forEach(filteredCards, function(card) {
        result += card.EstimatedPoints;
      });
      return result;
    }

    function getUserNames(card) {
      if (!card.Users)
        return "";

      return card.Users.map(function(e) {
        return e.Name
      }).join(',');
    }

    function addCard(listItem) {
      if (!listItem.Cards) {
        listItem.Cards = [];
      }
      vm.newCard.EstimatedPoints = 0;
      vm.newCard.ListId = listItem.Id;
      vm.newCard.Order = listItem.Cards.length;

      vm.selectedList = listItem;
      $http.post("./Card", vm.newCard).success(function(result) {
        vm.selectedCard = result;
        listItem.Cards.push(result);
        listItem.showNewCard = false;
        vm.newCard = {};
      });
    }

    function addComment() {
      if (!vm.selectedCard.Comments) {
        vm.selectedCard.Comments = [];
      }

      var newComment = {
        "Text": vm.newComment.Text,
        "User": vm.loginUser
      };
      $http.post("./Card/" + vm.selectedCard.Id + "/Comment/", newComment).success(function(result) {
        result.User = vm.loginUser;
        vm.selectedCard.Comments.push(result);
        vm.newComment = {};
      });
    }

    function addList() {
      var newList = {
        "Name": vm.newListTitle,
        "BoardId": boardId,
        "Cards": []
      };

      $http.post("./List", newList).success(function(result) {
        vm.list.push(result);
        vm.newListTitle = null;
      });
    }

    function addNewCard(listItem) {
      listItem.showNewCard = true;
    }

    function afterModalClosing() {
      $state.go("board");
    }

    function cancelNewCard(listItem) {
      listItem.showNewCard = false;
      vm.newCard = {};
    }

    function deleteCard() {
      $http.delete("./Card/" + vm.selectedCard.Id).success(function(result) {
        vm.selectedList.Cards.splice(vm.selectedList.Cards.indexOf(vm.selectedCard), 1);
        vm.showModal = false;
      });
    };

    function deleteComment(comment) {
      $http.delete("./Card/" + vm.selectedCard.Id + "/Comment/" + comment.Id).success(function(result) {
        vm.selectedCard.Comments.splice(vm.selectedCard.Comments.indexOf(comment), 1);
      });
    }

    function deleteList(listItem) {
      $http.delete("./List/" + listItem.Id).success(function(result) {
        vm.list.splice(vm.list.indexOf(listItem), 1);
      });
    }

    function openDetail(listItem, card) {
      $http.get("./Card/" + card.Id + "/Comment").success(function(result) {
        card.Comments = result;
        vm.selectedList = listItem;
        vm.selectedCard = card;
        vm.showModal = true;
        vm.newComment = {};
        $state.go("board.detail", {
          cardId: card.Id
        });
      });
    }

    function saveTitle() {
      updateCard();
    }

    function setEstimatedPoints(estimatedPoints) {
      vm.selectedCard.EstimatedPoints = estimatedPoints;
      updateCard();
    }

    function setLabel(label) {
      if (!vm.selectedCard.Labels) {
        vm.selectedCard.Labels = [];
      }

      var index = vm.getIndex(vm.selectedCard.Labels, label, 'Id');
      if (index > -1) {
        $http.delete("./Card/" + vm.selectedCard.Id + "/Label/" + label.Id).success(function(result) {
          vm.selectedCard.Labels.splice(index, 1);
        });
      } else {
        $http.post("./Card/" + vm.selectedCard.Id + "/Label/" + label.Id, null).success(function(result) {
          vm.selectedCard.Labels.push(label);
        });
      }
    }

    function setUser(user) {
      if (!vm.selectedCard.Users) {
        vm.selectedCard.Users = [];
      }

      var index = vm.getIndex(vm.selectedCard.Users, user, 'Id');
      if (index > -1) {
        $http.delete("./Card/" + vm.selectedCard.Id + "/User/" + user.Id).success(function(result) {
          vm.selectedCard.Users.splice(index, 1);
        });
      } else {
        $http.post("./Card/" + vm.selectedCard.Id + "/User/" + user.Id, null).success(function(result) {
          vm.selectedCard.Users.push(user);
        });
      }
    }

    function updateCard() {
      $http.put("./Card/" + vm.selectedCard.Id, vm.selectedCard);
    }
  }
})();
