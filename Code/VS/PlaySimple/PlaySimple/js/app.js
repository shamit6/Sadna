!(function () {
    // Declare app level module which depends on filters, and services
    var playSimpleApp = angular.module('myApp', ['ngRoute']);

    playSimpleApp.config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {
        $routeProvider.when('/', {
            templateUrl: 'partials/loginForm.html',
            controller: 'LoginCtrl'
        });

        $routeProvider.when('/login', {
            templateUrl: 'partials/loginForm.html',
            controller: 'LoginCtrl'
        });

        $routeProvider.when('/searchCustomers', {
            templateUrl: 'partials/searchCustomers.html',
            controller: 'LoginCtrl'
        });

        $routeProvider.when('/ownedPendingInvitations', {
            templateUrl: 'partials/ownedPendingInvitations.html',
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
        $routeProvider.when('/reportCustomer', {
            templateUrl: 'partials/reportCustomer.html',
            controller: 'ReportCustomerCtrl'
        });
        $routeProvider.when('/reportComplaint', {
            templateUrl: 'partials/reportComplaint.html',
            controller: 'ReportComplaintCtrl'
        });

        $routeProvider.otherwise({
            redirectTo: '/login'
        });
    }]);

    playSimpleApp.run(function ($rootScope, $route, $location, LoginService) {
        $rootScope.sharedVariables = {
            isLogin: true
        };

        $rootScope.logout = function () {
            LoginService.deleteLogin();
        };

        $rootScope.$on("$routeChangeSuccess", function (event, current, previous, rejection) {
            var currPath = $location.path();

            if (currPath == "/" || currPath == "/login") {
                $rootScope.sharedVariables.isLogin = true;
            }
            else {
                $rootScope.sharedVariables.isLogin = false;
            }
        });

        if (LoginService.hasPreviousLogin()) {
            LoginService.navigateToHomepage();
        }
    });
})();

 
