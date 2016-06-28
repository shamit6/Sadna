!(function () {
    // Declare app level module which depends on filters, and services
    var playSimpleApp = angular.module('myApp', ['ngRoute']);

    playSimpleApp.config(['$routeProvider', function ($routeProvider) {
        $routeProvider.when('/login', {
            templateUrl: 'partials/loginForm.html',
            controller: 'MyCtrl1'
        });

        $routeProvider.when('/ownedInvitations', {
            templateUrl: 'partials/ownedInvitations.html',
            controller: 'MyCtrl1'
        });

        $routeProvider.when('/registration', {
            templateUrl: 'partials/registrationForm.html',
            controller: 'MyCtrl2'
        });

        $routeProvider.otherwise({
            redirectTo: '/login'
        });
    }]);
})();

 
