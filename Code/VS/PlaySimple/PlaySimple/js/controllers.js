!(function () {
    var myApp = angular.module('myApp');
    myApp.controller('LoginCtrl', ['$rootScope', '$scope', '$http', '$location', 'LoginService', 'ServerRoutes', function ($rootScope, $scope, $http, $location, LoginService, ServerRoutes) {
        var init = function () {
            $scope.model = {};
            $scope.noUser = false;
            $scope.userFrozen = false;

            if (LoginService.hasPreviousLogin()) {
                LoginService.navigateToHomepage();
            }
        };

        $scope.submitUser = function () {
            $scope.noUser = false;
            $scope.userFrozen = false;

            $http({
                method: 'POST',
                url: ServerRoutes.login.login,
                data: $scope.model
            }).then(function(response) {
                if (response.data.Role == "None") {
                    $scope.noUser = true;
                }
                else if (response.data.IsUserFrozen){
                    $scope.userFrozen = true;
                }
                else {
                    LoginService.saveLogin(response.data);
                    LoginService.navigateToHomepage();
                }
            });
        };

        init();
    }]);

    //myApp.controller('MyCtrl2', ['$scope', function ($scope) {
    //}]);

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

    myApp.controller('FieldsCtrl', ['$scope', '$http', '$routeParams', '$location', 'DomainDecodes', 'ServerRoutes', 'toaster', function ($scope, $http, $routeParams, $location, DomainDecodes, ServerRoutes, toaster) {
        var init = function () {
            $scope.sizes = DomainDecodes.fieldSize;
            $scope.types = DomainDecodes.fieldType;

            $scope.submitted = false;
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

        $scope.submitField = function (isValid) {
            $scope.submitted = true;

            if (!isValid)
                return;

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

    myApp.controller('EmployeesCtrl', ['$scope', '$http', '$routeParams', '$location', 'ServerRoutes', 'toaster', function ($scope, $http, $routeParams, $location, ServerRoutes, toaster) {

        var init = function () {

            $scope.submitted = false;
            $scope.model = {};
            $scope.model.employee = {};
            $scope.model.passwordChanging = {};
            $scope.model.passwordChanging.new = null;
            $scope.model.passwordChanging.newVerify = null;

            $scope.originalModel = {};

            if ($routeParams.Id) {
                $scope.isNew = false;

                $http({
                    url: ServerRoutes.employees,
                    method: "GET",
                    params: { id: $routeParams.Id },
                }).then(function searchCompleted(response) {
                    $scope.model.employee = angular.copy(response.data)
                    $scope.originalModel = angular.copy($scope.model.employee);
                    $scope.model.verifiedPassword = $scope.model.employee.Password;
                });
            }
            else {
                $scope.isNew = true;
            }
        }

        $scope.submitEmployee = function (isValid) {
            $scope.submitted = true;

            if (!isValid)
                return;

            if ($scope.model.passwordChanging.new != null && $scope.model.passwordChanging.new != "") {
                $scope.model.employee.Password = $scope.model.passwordChanging.new;
                $scope.model.verifiedPassword = $scope.model.employee.Password;
            }
            
            if ($scope.isNew) {
                $http({
                    url: ServerRoutes.employees,
                    method: "POST",
                    data: $scope.model.employee,
                }).then(function searchCompleted(response) {
                    $scope.model.verifiedPassword = $scope.model.employee.Password;
                    $location.path('/editEmployee/' + response.data.Id);
                });
            }
            else {
                $http({
                    url: ServerRoutes.employees,
                    method: "PUT",
                    params: { id: $scope.model.employee.Id },
                    data: $scope.model.employee,
                }).then(function searchCompleted(response) {
                    $scope.originalModel = angular.copy($scope.model.employee);
                    alert("data saved successfully");
                });
            }
        };

        $scope.cancelChanges = function () {
            $scope.model.employee = angular.copy($scope.originalModel);
        };

        $scope.delete = function () {
            $http({
                url: ServerRoutes.employees,
                method: "DELETE",
                params: { id: $scope.model.employee.Id }
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
        $scope.types = ServerRoutes.complaintType;
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
        $scope.types = ServerRoutes.regionDecode;
        $scope.results;
        $scope.submitSearch = function () {
            $http({
                url: ServerRoutes.customers,
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;
            });
        }
    }]); 

    myApp.controller('SearchAvailableOrdersCtrl', ['$scope', '$http', 'ServerRoutes', function ($scope, $http, ServerRoutes) {
        $scope.model = {};
        $scope.types = ServerRoutes.fieldType;
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
        $scope.types = ServerRoutes.orderStatus;
        $scope.results;
        $scope.submitSearch = function () {
            $http({
                url: ServerRoutes.orders.searchmyorders,
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
            optinalOrdersParams.date = $scope.datePicker;
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
                $scope.model.Participants = angular.copy(response.data.Participants);
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
                    $scope.model = angular.copy(response.data);
                    $scope.originalModel = angular.copy($scope.model);
                    $scope.datePicker = new Date($scope.model.StartDate);
                    $scope.getOptionals();
                    $scope.getOptionalField();
                });  
            }
            else {
                $scope.isNew = true;
                $scope.model = angular.fromJson($routeParams.order);
                $scope.datePicker = new Date($scope.model.StartDate);
                $scope.getOptionals();
                $scope.getOptionalField();
            }
        }

        $scope.submitOrder = function (isValid, status) {
            $scope.submitted = true;

            if (!isValid)
                return;

            //$scope.model.Participants = null;
            $scope.model.Status = status;
            var modalToSend = angular.copy($scope.model);
            modalToSend.StartDate = new Date(modalToSend.StartDate).getTime();
            if ($scope.isNew) {
                $http({
                    url: ServerRoutes.orders.base,
                    method: "POST",
                    data: modalToSend,
                }).then(function searchCompleted(response) {
                    $location.path('/ownedOrders');
                });
            }
            else {
                $http({
                    url: ServerRoutes.orders.base,
                    method: "PUT",
                    params: { id: $scope.model.Id },
                    data: modalToSend,
                }).then(function searchCompleted(response) {
                    $scope.originalModel = angular.copy($scope.model);
                    alert("data saved successfully");
                });
            }
        };

        $scope.cancelOrder = function () {

            $scope.originalModel.Status = 4;
            var modalToSend = angular.copy($scope.originalModel);
            modalToSend.StartDate = new Date(modalToSend.StartDate).getTime();

            $http({
                url: ServerRoutes.orders.base,
                method: "PUT",
                params: { id: $scope.originalModel.Id },
                data: modalToSend,
            }).then(function searchCompleted(response) {
                $scope.originalModel = angular.copy($scope.model);
                alert("data saved successfully");
                $location.path('/ownedOrders');
            });
        }

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

    myApp.controller('SearchOrdersCrtl', ['$scope', '$route', '$http', 'DomainDecodes', 'ServerRoutes',
    function ($scope, $route, $http, DomainDecodes, ServerRoutes) {

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
            $scope.complaintTypes = DomainDecodes.complaintType;

            $scope.model = {};
            $scope.model.customer = {};
            $scope.model.review = {};
            $scope.model.complaint = {};
            $scope.model.passwordChanging = {};
            $scope.originalCustomer = {};
            $scope.submitted = false;

            $http({
                url: ServerRoutes.customers,
                method: "GET",
                params: { id: $routeParams.Id },
            }).then(function searchCompleted(response) {
                $scope.model.customer = angular.copy(response.data)
                $scope.originalCustomer = angular.copy($scope.model.customer);
            });
        }

        $scope.submitCustomer = function (isValid) {
            $scope.submitted = true;

            if (!isValid)
                return;


            if ($scope.model.passwordChanging.new != null && $scope.model.passwordChanging.new != "") {
                $scope.model.customer.Password = $scope.model.passwordChanging.new;
                $scope.model.verifiedPassword = $scope.model.customer.Password;
            }

            $http({
                url: ServerRoutes.customers,
                method: "PUT",
                params: { id: $scope.model.customer.Id },
                data: $scope.model.customer,
            }).then(function searchCompleted(response) {
                $scope.originalCustomer = angular.copy($scope.model.customer);
                alert("data saved successfully");
                $scope.submitted = false;
            });
        };

        $scope.saveReview = function () {

            $scope.model.review.Date = new Date();
            $scope.model.review.ReviewedCustomer = {};
            $scope.model.review.ReviewedCustomer.Id = $scope.model.customer.Id;
            $http({
                url: ServerRoutes.reviews.base,
                method: "POST",
                data: $scope.model.review,
            }).then(function searchCompleted(response) {
                alert("data saved successfully");
                $scope.model.review = {};
            });
        };

        $scope.saveComplaint = function () {

            $scope.model.complaint.Date = new Date();
            $scope.model.complaint.OffendingCustomer = {};
            $scope.model.complaint.OffendingCustomer.Id = $scope.model.customer.Id;
            $http({
                url: ServerRoutes.reviews.base,
                method: "POST",
                data: $scope.model.review,
            }).then(function searchCompleted(response) {
                alert("data saved successfully");
                $scope.model.complaint = {};
            });
        };

        $scope.freezeCustomer = function () {
            var freezeDate = new Date();
            freezeDate.setDate(freezeDate.getDate() + 30);
            $scope.model.customer.FreezeDate = freezeDate;
            $scope.submitCustomer();
        }

        $scope.cancelChanges = function () {
            $scope.model.customer = angular.copy($scope.originalCustomer);
        };

        $scope.getComplaints = function (scope) {
            scope.customerComplaints = {};
            $http({
                url: ServerRoutes.complaints.search,
                method: "GET",
                params: { customerId: $routeParams.Id },
            }).then(function searchCompleted(response) {
                scope.customerComplaints = angular.copy(response.data)
            });
        };

        $scope.getReviews = function (scope) {
            scope.customerReviews = {};
            $http({
                url: ServerRoutes.reviews.search,
                method: "GET",
                params: { customerId: $routeParams.Id },
            }).then(function searchCompleted(response) {
                scope.customerReviews = angular.copy(response.data)
            });
        };

        init();
    }]);

    myApp.controller('CustomerModalCtrl', function ($uibModal, $log) {
        var $ctrl = this;

        $ctrl.open = function (func, template) {

            var modalInstance = $uibModal.open({
                animation: true,
                //ariaLabelledBy: 'modal-title',
                //ariaDescribedBy: 'modal-body',
                templateUrl: template,
                controller: 'CustomerModalInstanceCtrl',
                controllerAs: '$ctrl',
                size: 'lg',
                resolve: {
                    action: function () {
                        return func;
                    }
                }
            });

        };
    });

    myApp.controller('CustomerModalInstanceCtrl', function ($uibModalInstance, action, $http, $routeParams, $scope, ServerRoutes) {
        action($scope);

        $scope.close = function () {
            $uibModalInstance.close();
        };
    });

    myApp.controller('RegistrationFormCtrl', ['$scope', '$http', '$routeParams', '$location', 'DomainDecodes', 'ServerRoutes', 'toaster',
    function ($scope, $http, $routeParams, $location, DomainDecodes, ServerRoutes, toaster) {
        $scope.regionTypes = DomainDecodes.regionDecode;
        $scope.submitted = false;

        $scope.submitCustomer = function (isValid) {
            $scope.submitted = true;

            if (!isValid)
                return;

            $http({
                url: ServerRoutes.login.registration,
                method: "POST",
                data: $scope.model,
            }).success(function searchCompleted(response) {
                if (response.AlreadyExists) {
                    toaster.error("אופס!", "שם משתמש כבר קיים באתר!", 5000);
                }
                else {
                    $location.path("/login");
                }
            });
        };
    }]);
})();
