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
      if(!list)
        return false;

      return list.map(function(e){return e[propertyName]}).indexOf(value[propertyName]) > -1;
    };

    function loadResources(callback) {
        translationService.setGlobalResources(callback);
    }
}
