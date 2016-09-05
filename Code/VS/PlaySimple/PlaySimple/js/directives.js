!(function () {
    var myApp = angular.module('myApp');
    myApp.directive('appVersion', ['version', function (version) {
          return function (scope, elm, attrs) {
              elm.text(version);
          };
      }]);
})();
