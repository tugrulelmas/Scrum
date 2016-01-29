function BaseCtrl($scope, translationService) {
    $scope.ml = function (resourceName) {
        return translationService.getResource(resourceName);
    };

    $scope.includes = function(list, value, propertyName){
      return $scope.getIndex(list, value, propertyName) > -1;
    };

    $scope.getIndex = function(list, value, propertyName){
      if(!list)
        return -1;

      return list.map(function(e){return e[propertyName]}).indexOf(value[propertyName]);
    };
}
