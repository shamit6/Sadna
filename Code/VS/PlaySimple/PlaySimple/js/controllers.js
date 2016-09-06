﻿!(function () {
    var myApp = angular.module('myApp');
    myApp.controller('LoginCtrl', ['$rootScope', '$scope', '$http', '$location', function ($rootScope, $scope, $http, $location) {
        $scope.model = {};

        var navigateToHomepage = function navigateToHomepage(role) {
            if (role == "Admin") { // searchCustomers
                $location.path('/searchCustomers');
            }
            else if (role == "Employee") { // 
                $location.path('/ownedPendingInvitations');
            }
            else {
                $location.path('/ownedInvitations');
            }
        };

        $scope.submitUser = function () {
            $http({
                method: 'POST',
                url: 'http://localhost:59233/api/login',
                data: $scope.model
            }).then(function(response) {
                if (response.data.Role == "None") {
                    window.alert("No permissions!");
                }
                else {
                    $http.defaults.headers.common['Authorization'] = response.data.AuthorizationKey;
                    $rootScope.sharedVariables.role = response.data.Role;
                    navigateToHomepage($rootScope.sharedVariables.role);
                }
            });
        };
    }]);

    myApp.controller('MyCtrl2', ['$scope', function ($scope) {
    }]);

    myApp.controller('SearchFieldsCtrl', ['$scope', '$http', function ($scope, $http) {
        $scope.model = {};
        $scope.results;

        $scope.submitSearch = function () {
            $http({
                url: 'http://localhost:59233/api/reports/fields',
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;
            });
        }
    }]);

    myApp.controller('FieldsCtrl', ['$scope', '$http', '$routeParams', '$location', function ($scope, $http, $routeParams, $location) {

        var init = function () {
            $scope.sizes = [
            {
                id: 1,
                name: 'קטן'
            },
            {
                id: 2,
                name: 'בינוני'
            },
            {
                id: 3,
                name: 'גדול'
            }];

            $scope.types = [
            {
                id: 1,
                name: 'כדורגל'
            },
            {
                id: 2,
                name: 'כדורסל'
            },
            {
                id: 3,
                name: 'טניס'
            }];

            $scope.model = {};
            $scope.originalModel = {};

            if ($routeParams.Id) {
                $scope.isNew = false;

                $http({
                    url: 'http://localhost:59233/api/fields',
                    method: "GET",
                    params: { id: $routeParams.Id },
                }).then(function searchCompleted(response) {
                    $scope.model = angular.copy(response.data)
                    $scope.originalModel = angular.copy($scope.model);
                });
            }
            else {
                $scope.isNew = true;
            }
        }

        $scope.submitField = function () {
            if ($scope.isNew) {
                $http({
                    url: 'http://localhost:59233/api/fields',
                    method: "POST",
                    data: $scope.model,
                }).then(function searchCompleted(response) {
                    $location.path('/editField/' + response.data.Id);
                });
            }
            else {
                $http({
                    url: 'http://localhost:59233/api/fields',
                    method: "PUT",
                    params: { id: $scope.model.Id },
                    data: $scope.model,
                }).then(function searchCompleted(response) {
                    $scope.originalModel = angular.copy($scope.model);
                    alert("data saved successfully");
                });
            }
        };

        $scope.cancelChanges = function () {
            $scope.model = angular.copy($scope.originalModel);
        };

        $scope.delete = function () {
            $http({
                url: 'http://localhost:59233/api/fields',
                method: "DELETE",
                params: { id: $scope.model.Id }
            }).then(function searchCompleted(response) {
                $location.path('/editField');
            });
        };

        init();
    }]);

    myApp.controller('SearchEmployeesCtrl', ['$scope', '$http', function ($scope, $http) {
        $scope.model = {};
        $scope.results;

        $scope.submitSearch = function () {
            $http({
                url: 'http://localhost:59233/api/employees',
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;
            });

        }
    }]);

    myApp.controller('EmployeesCtrl', ['$scope', '$http', '$routeParams', '$location', function ($scope, $http, $routeParams, $location) {

        var init = function () {
            $scope.model = {};
            $scope.originalModel = {};

            if ($routeParams.Id) {
                $scope.isNew = false;

                $http({
                    url: 'http://localhost:59233/api/employees',
                    method: "GET",
                    params: { id: $routeParams.Id },
                }).then(function searchCompleted(response) {
                    $scope.model = angular.copy(response.data)
                    $scope.originalModel = angular.copy($scope.model);
                });
            }
            else {
                $scope.isNew = true;
            }
        }

        $scope.submitField = function () {
            if ($scope.isNew) {
                $http({
                    url: 'http://localhost:59233/api/employees',
                    method: "POST",
                    data: $scope.model,
                }).then(function searchCompleted(response) {
                    $location.path('/editEmployee/' + response.data.Id);
                });
            }
            else {
                $http({
                    url: 'http://localhost:59233/api/employees',
                    method: "PUT",
                    params: { id: $scope.model.Id },
                    data: $scope.model,
                }).then(function searchCompleted(response) {
                    $scope.originalModel = angular.copy($scope.model);
                    alert("data saved successfully");
                });
            }
        };

        $scope.cancelChanges = function () {
            $scope.model = angular.copy($scope.originalModel);
        };

        $scope.delete = function () {
            $http({
                url: 'http://localhost:59233/api/employees',
                method: "DELETE",
                params: { id: $scope.model.Id }
            }).then(function searchCompleted(response) {
                $location.path('/editEmployee');
            });
        };

        init();
    }]);

    myApp.controller('ReportCustomerCtrl', ['$scope', '$http', function ($scope, $http) {
        $scope.model = {};
        $scope.results;

        $scope.submitSearch = function () {
            $http({
                url: 'http://localhost:59233/api/reports/customers',
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;
            });

        }
    }]);
})();
