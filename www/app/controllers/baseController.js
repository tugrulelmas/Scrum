function BaseCtrl($scope, translationService) {
    loadResources();

    $scope.ml = function (resourceName) {
        return translationService.getResource(resourceName);
    };

    $scope.$on('languageChanged', function (event) {
        loadResources(function () {
            if ($scope.$parent.isGlobalController) {
                //noty({ text: $scope.ml("LanguageChangedMessage"), layout: 'topRight', type: 'warning', timeout: 15000 });
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
