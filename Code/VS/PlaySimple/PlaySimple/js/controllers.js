!(function () {
    var myApp = angular.module('myApp');
    myApp.controller('LoginCtrl', ['$rootScope', '$scope', '$http', '$location', 'LoginService', 'ServerRoutes', function ($rootScope, $scope, $http, $location, LoginService, ServerRoutes) {
        var init = function () {
            $scope.model = {};

            if (LoginService.hasPreviousLogin()) {
                LoginService.navigateToHomepage();
            }
        };

        $scope.submitUser = function () {
            $http({
                method: 'POST',
                url: ServerRoutes.login,
                data: $scope.model
            }).then(function(response) {
                if (response.data.Role == "None") {
                    window.alert("No permissions!");
                }
                else {
                    LoginService.saveLogin(response.data);
                    LoginService.navigateToHomepage();
                }
            });
        };

        init();
    }]);

    myApp.controller('MyCtrl2', ['$scope', function ($scope) {
    }]);

    myApp.controller('SearchFieldsCtrl', ['$scope', '$http', 'ServerRoutes', function ($scope, $http, ServerRoutes) {
        $scope.model = {};
        $scope.results;

        $scope.submitSearch = function () {
            $http({
                url: ServerRoutes.reports.fields,
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;
            });
        }
    }]);

    myApp.controller('FieldsCtrl', ['$scope', '$http', '$routeParams', '$location', 'DomainDecodes', 'ServerRoutes', function ($scope, $http, $routeParams, $location, DomainDecodes, ServerRoutes) {
        var init = function () {
            $scope.sizes = DomainDecodes.fieldSize;
            $scope.types = DomainDecodes.fieldType;

            $scope.model = {};
            $scope.originalModel = {};

            if ($routeParams.Id) {
                $scope.isNew = false;

                $http({
                    url: ServerRoutes.fields,
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
                    url: ServerRoutes.fields,
                    method: "POST",
                    data: $scope.model,
                }).then(function searchCompleted(response) {
                    $location.path('/editField/' + response.data.Id);
                });
            }
            else {
                $http({
                    url: ServerRoutes.fields,
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
                url: ServerRoutes.fields,
                method: "DELETE",
                params: { id: $scope.model.Id }
            }).then(function searchCompleted(response) {
                $location.path('/editField');
            });
        };

        init();
    }]);

    myApp.controller('SearchEmployeesCtrl', ['$scope', '$http', 'ServerRoutes', function ($scope, $http, ServerRoutes) {
        $scope.model = {};
        $scope.results;

        $scope.submitSearch = function () {
            $http({
                url: ServerRoutes.employees,
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;
            });

        }
    }]);

    myApp.controller('EmployeesCtrl', ['$scope', '$http', '$routeParams', '$location', 'ServerRoutes', function ($scope, $http, $routeParams, $location, ServerRoutes) {

        var init = function () {
            $scope.model = {};
            $scope.originalModel = {};

            if ($routeParams.Id) {
                $scope.isNew = false;

                $http({
                    url: ServerRoutes.employees,
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
                    url: ServerRoutes.employees,
                    method: "POST",
                    data: $scope.model,
                }).then(function searchCompleted(response) {
                    $location.path('/editEmployee/' + response.data.Id);
                });
            }
            else {
                $http({
                    url: ServerRoutes.employees,
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
                url: ServerRoutes.employees,
                method: "DELETE",
                params: { id: $scope.model.Id }
            }).then(function searchCompleted(response) {
                $location.path('/editEmployee');
            });
        };

        init();
    }]);

    myApp.controller('ReportCustomerCtrl', ['$scope', '$http', 'ServerRoutes', function ($scope, $http, ServerRoutes) {
        $scope.model = {};
        $scope.results;

        $scope.submitSearch = function () {
            $http({
                url: ServerRoutes.reports.customers,
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;
            });

        }
    }]);

    myApp.controller('ReportComplaintCtrl', ['$scope', '$http', 'ServerRoutes', function ($scope, $http, ServerRoutes) {
        $scope.model = {};
        $scope.types = [
                {
                    id: 1,
                    name: 'אי תשלום'
                },
                {
                    id: 2,
                    name: 'אי הגעה'
                },
                {
                    id: 3,
                    name: 'חוסר ספורטיביות'
                }
        ];
        $scope.results;
        $scope.submitSearch = function () {
            $http({
                url: ServerRoutes.reports.complaints,
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;
            });

        }
    }]); 
    myApp.controller('SearchCustomersCtrl', ['$scope', '$http', 'ServerRoutes', function ($scope, $http, ServerRoutes) {
        $scope.model = {};
        $scope.types = [
                {
                    id: 1,
                    name: 'דן'
                },
                {
                    id: 2,
                    name: 'נגב'
                },
                {
                    id: 3,
                    name: 'חיפה'
                },
                {
                    id: 4,
                    name: 'ירושלים'
                }
        ];
        $scope.results;
        $scope.submitSearch = function () {
            $http({
                url: ServerRoutes.costumers,
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;
            });
        }
    }]); 
    myApp.controller('SearchAvailableOrdersCtrl', ['$scope', '$http', 'ServerRoutes', function ($scope, $http, ServerRoutes) {
        $scope.model = {};
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
                }
        ],
        $scope.results;
        $scope.submitSearch = function () {
            $http({
                url: ServerRoutes.orders.availables,
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;
            });
        }
    }]); 

    myApp.controller('ownedOrdersCrtl', ['$scope', '$http', 'ServerRoutes', function ($scope, $http, ServerRoutes) {
        $scope.model = {};
        $scope.types = [
            {
                id: 1,
                name: 'נשלח'
            },
            {
                id: 2,
                name: 'התקבל'
            },
            {
                id: 3,
                name: 'נדחה'
            },
            {
                id: 4,
                name: 'בוטל'
            }
        ],
        $scope.results;
        $scope.submitSearch = function () {
            $http({
                url: ServerRoutes.orders.search,
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;
            });
        }
    }]);
})();
