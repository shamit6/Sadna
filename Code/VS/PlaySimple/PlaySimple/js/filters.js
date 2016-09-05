﻿!(function () {
    var myApp = angular.module('myApp');
      myApp.filter('interpolate', ['version', function (version) {
          return function (text) {
              return String(text).replace(/\%VERSION\%/mg, version);
          }
      }]);
})();

