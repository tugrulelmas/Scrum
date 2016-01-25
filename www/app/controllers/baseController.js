function BaseCtrl($scope, translationService) {
    loadResources();

    $scope.ml = function (resourceName) {
        return translationService.getResource(resourceName);
    };

    $scope.$on('languageChanged', function (event) {
        loadResources(function () {
            if ($scope.$parent.isGlobalController) {
                alert.warning($scope.ml("LanguageChangedMessage"));
            }
        });
    });

    $scope.includes = function(list, value, propertyName){
      return $scope.getIndex(list, value, propertyName) > -1;
    };

    $scope.getIndex = function(list, value, propertyName){
      if(!list)
        return -1;

      return list.map(function(e){return e[propertyName]}).indexOf(value[propertyName]);
    };

    function loadResources(callback) {
        translationService.setGlobalResources(callback);
    }
}
