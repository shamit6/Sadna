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

        //$routeProvider.when('/searchCustomers', {
        //    templateUrl: 'partials/searchCustomers.html',
        //    controller: 'LoginCtrl'
        //});

        $routeProvider.when('/ownedPendingInvitations', {
            templateUrl: 'partials/ownedPendingInvitations.html',
            controller: 'LoginCtrl'
        });

        //$routeProvider.when('/ownedOrders', {
        //    templateUrl: 'partials/ownedOrders.html',
        //    controller: 'LoginCtrl'
        //});

        $routeProvider.when('/registration', {
            templateUrl: 'partials/registrationForm.html',
            controller: 'MyCtrl2'
        });

        $routeProvider.when('/editField/:Id?', {
            templateUrl: 'partials/editField.html',
            controller: 'FieldsCtrl'
        });
        $routeProvider.when('/editCustomer/:Id?', {
            templateUrl: 'partials/editCustomer.html',
            controller: 'CustomersCtrl'
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
        $routeProvider.when('/searchCustomers', {
            templateUrl: 'partials/searchCustomers.html',
            controller: 'SearchCustomersCtrl'
        }); 
        $routeProvider.when('/searchAvailableOrders', {
            templateUrl: 'partials/searchAvailableOrders.html',
            controller: 'SearchAvailableOrdersCtrl'
        });
        $routeProvider.when('/ownedOrders', {
            templateUrl: 'partials/ownedOrders.html',
            controller: 'ownedOrdersCrtl'
        }); 
        $routeProvider.when('/editOrder/:Id?', {
            templateUrl: 'partials/editOrder.html',
            controller: 'OrdersCtrl'
        }); 
        $routeProvider.when('/searchAvailableOrdersToJoin', {
            templateUrl: 'partials/searchAvailableOrdersToJoin.html',
            controller: 'SearchAvailableOrdersToJoinCrtl'
        });
        $routeProvider.when('/searchOrders', {
            templateUrl: 'partials/searchOrders.html',
            controller: 'SearchOrdersCrtl'
        });
        $routeProvider.when('/pendingOrdersToJoin', {
            templateUrl: 'partials/pendingOrdersToJoin.html',
            controller: 'PendingOrdersToJoinCrtl'
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

            if (currPath == "/" || currPath == "/login" || currPath == "/registration") {
                $rootScope.sharedVariables.isLogin = true;
            }
            else {
                $rootScope.sharedVariables.isLogin = false;
            }
        });

        //var currUrl = window.location.href.substring(window.location.href.indexOf("#") + 1);

        if (LoginService.hasPreviousLogin()) {
            LoginService.navigateToHomepage($location.path());
        }
    });
})();

 
