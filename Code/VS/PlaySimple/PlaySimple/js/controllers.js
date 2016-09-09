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
                url: ServerRoutes.orders.searchownedorders,
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;
            });
        }
    }]); 
    myApp.controller('OrdersCtrl', ['$scope', '$http', '$routeParams', '$location', 'DomainDecodes', 'ServerRoutes', function ($scope, $http, $routeParams, $location, DomainDecodes, ServerRoutes) {

        $scope.getOptionals = function () {

            var optinalOrdersParams = {};
            optinalOrdersParams.orderId = $scope.model.Id;
            optinalOrdersParams.fieldId = $scope.model.Field.Id;
            optinalOrdersParams.fieldType = $scope.model.Field.Type;
            optinalOrdersParams.date = $scope.model.StartDate;

            $http({
                url: ServerRoutes.orders.optionals,
                method: "GET",
                params: optinalOrdersParams,
            }).then(function searchCompleted(response) {
                $scope.optinalOrders = angular.copy(response.data);
            });
        }

        $scope.getOptionalField = function ()
        {
            var optinalFieldsParams = {};
            optinalFieldsParams.fieldId = null;
            optinalFieldsParams.fieldName = null;
            optinalFieldsParams.type = $scope.model.Field.Type;

            $http({
                url: ServerRoutes.fields,
                method: "GET",
                params: optinalFieldsParams,
            }).then(function searchCompleted(response) {
                $scope.optinalFields = angular.copy(response.data);
            });
        }
        
        $scope.fieldTypeChanged = function () {
            var optinalFieldsParams = {};
            optinalFieldsParams.fieldId = null;
            optinalFieldsParams.fieldName = null;
            optinalFieldsParams.type = $scope.model.Field.Type;

            $http({
                url: ServerRoutes.fields,
                method: "GET",
                params: optinalFieldsParams,
            }).then(function searchCompleted(response) {
                $scope.optinalFields = angular.copy(response.data);

                $scope.model.Field.Id = $scope.optinalFields[0].Id;
                $scope.getOptionals();
            });

        }

        $scope.acceptRejectParticipant = function (participant, status) {
            participant.Status = status;

            $http({
                url: ServerRoutes.orders.updatepraticipant,
                method: "PUT",
                params: { id: participant.Id },
                data: participant,
            }).then(function searchCompleted(response) {
                $location.path('/editOrder/' + response.data.Id);
                $scope.model.Participants = angular.copy(response.data.Participants)
                $scope.originalModel.Participants = angular.copy($scope.model.Participants);
            });
        }

        var init = function () {
            $scope.model = {};
            $scope.originalModel = {};

            $scope.fieldType = DomainDecodes.fieldType;
            $scope.optinalOrders = {};

            if ($routeParams.Id) {
                $scope.isNew = false;

                $http({
                    url: ServerRoutes.orders.base,
                    method: "GET",
                    params: { id: $routeParams.Id },
                }).then(function searchCompleted(response) {
                    $scope.model = angular.copy(response.data)
                    $scope.originalModel = angular.copy($scope.model);

                    $scope.getOptionals();
                    $scope.getOptionalField();
                });  
            }
            else {
                $scope.isNew = true;
            }          
        }

        $scope.submitOrder = function (status) {
            //$scope.model.Participants = null;
            $scope.model.Status = status;
            if ($scope.isNew) {
                $http({
                    url: ServerRoutes.orders.base,
                    method: "POST",
                    data: $scope.model,
                }).then(function searchCompleted(response) {
                    $location.path('/editOrder/' + response.data.Id);
                });
            }
            else {
                $http({
                    url: ServerRoutes.orders.base,
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
            $scope.fieldTypeChanged();
        };

        init();
    }]);

    myApp.controller('SearchAvailableOrdersToJoinCrtl', ['$scope', '$route', '$http', 'DomainDecodes', 'ServerRoutes', function ($scope, $route, $http, DomainDecodes, ServerRoutes) {
        $scope.model = {};
        $scope.orderStatuses = DomainDecodes.orderStatus;
        $scope.results;
        $scope.submitSearch = function () {
            $http({
                url: ServerRoutes.orders.availablestojoin,
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;
            });
        }

        $scope.joinToOrder = function (order, index) {
            $http({
                url: ServerRoutes.orders.jointoorder,
                method: "GET",
                params: { orderId: order.Id },
            }).then(function searchCompleted(response) {
                //$location.path('/searchAvailableOrdersToJoin');
                // $route.reload();

                $scope.results.splice(index, 1);
            });
        }
    }]); 

    myApp.controller('SearchOrdersCrtl', ['$scope', '$route', '$http', 'DomainDecodes', 'ServerRoutes', function ($scope, $route, $http, DomainDecodes, ServerRoutes) {
        $scope.model = {};
        $scope.orderStatuses = DomainDecodes.orderStatus;
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

        $scope.submitSearch();

        $scope.acceptRejectOrder = function (order, newStatus) {
            order.Status = newStatus;

            $http({
                url: ServerRoutes.orders.base,
                method: "PUT",
                params: { id: order.Id },
                data: order,
            }).then(function searchCompleted(response) {
                order = response.data;
            });
        }
    }]); 
    myApp.controller('PendingOrdersToJoinCrtl', ['$scope', '$route', '$http', 'DomainDecodes', 'ServerRoutes', function ($scope, $route, $http, DomainDecodes, ServerRoutes) {
        $scope.model = {};
        $scope.orderStatuses = DomainDecodes.orderStatus;
        $scope.invitationStatuses = DomainDecodes.invitationStatus;
        $scope.results;
        $scope.submitSearch = function () {
            $http({
                url: ServerRoutes.participants,
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;
            });
        }

        $scope.submitSearch();

        $scope.cancelJoining = function (participantId, index) {

            $http({
                url: ServerRoutes.participants,
                method: "DELETE",
                params: { id: participantId }
            }).then(function searchCompleted(response) {
                $scope.results.splice(index, 1);
            });
        }
    }]);

    myApp.controller('CustomersCtrl', ['$scope', '$http', '$routeParams', '$location', 'DomainDecodes', 'ServerRoutes', function ($scope, $http, $routeParams, $location, DomainDecodes, ServerRoutes) {
        var init = function () {
            $scope.regionTypes = DomainDecodes.regionDecode;

            $scope.model = {};
            $scope.originalModel = {};

            if ($routeParams.Id) {
                $scope.isNew = false;

                $http({
                    url: ServerRoutes.customers,
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

        $scope.submitCustomer = function () {
            if ($scope.isNew) {
                $http({
                    url: ServerRoutes.customers,
                    method: "POST",
                    data: $scope.model,
                }).then(function searchCompleted(response) {
                    $location.path('/editCustomer/' + response.data.Id);
                });
            }
            else {
                $http({
                    url: ServerRoutes.customers,
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
                url: ServerRoutes.customers,
                method: "DELETE",
                params: { id: $scope.model.Id }
            }).then(function searchCompleted(response) {
                $location.path('/editCustomer');
            });
        };

        init();
    }]);

})();
