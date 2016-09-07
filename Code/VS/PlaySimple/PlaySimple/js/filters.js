!(function () {
    var myApp = angular.module('myApp');
      myApp.filter('interpolate', ['version', function (version) {
          return function (text) {
              return String(text).replace(/\%VERSION\%/mg, version);
          }
      }]);
      myApp.filter('ageFilter', function () {
          function calculateAge(birthday) { // birthday is a date
              var birthday = new Date(birthday);
              var today = new Date();
              var age = ((today - birthday) / (31557600000));
              var age = Math.floor(age);
              return age;
          }

          return function (birthdate) {
              return calculateAge(birthdate);
          };
      });
})();

