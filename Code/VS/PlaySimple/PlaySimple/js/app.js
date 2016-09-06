﻿!(function () {
    // Declare app level module which depends on filters, and services
    var playSimpleApp = angular.module('myApp', ['ngRoute']);

    playSimpleApp.config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {
        //$httpProvider.defaults.headers.common.Authorization = 'Basic Z2lsYWQ6Z2lsYWQ=';

        $routeProvider.when('/', {
            templateUrl: 'partials/loginForm.html',
            controller: 'LoginCtrl'
        });

        $routeProvider.when('/login', {
            templateUrl: 'partials/loginForm.html',
            controller: 'LoginCtrl'
        });

        $routeProvider.when('/ownedInvitations', {
            templateUrl: 'partials/ownedInvitations.html',
            controller: 'LoginCtrl'
        });

        $routeProvider.when('/registration', {
            templateUrl: 'partials/registrationForm.html',
            controller: 'MyCtrl2'
        });

        $routeProvider.when('/editField/:Id?', {
            templateUrl: 'partials/editField.html',
            controller: 'FieldsCtrl'
        });

        $routeProvider.when('/searchFields', {
            templateUrl: 'partials/reportFields.html',
            controller: 'SearchFieldsCtrl'
        });

        $routeProvider.when('/editEmployee/:Id?', {
            templateUrl: 'partials/editEmployee.html',
            controller: 'EmployeesCtrl'
        });
        $routeProvider.when('/searchEmployees', {
            templateUrl: 'partials/searchEmployee.html',
            controller: 'SearchEmployeesCtrl'
        });
        
        $routeProvider.otherwise({
            redirectTo: '/login'
        });
    }]);
})();

 
