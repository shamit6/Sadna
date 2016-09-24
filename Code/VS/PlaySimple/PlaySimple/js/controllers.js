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

    myApp.controller('SearchFieldsCtrl', ['$scope', '$http', 'ServerRoutes', 'toaster', function ($scope, $http, ServerRoutes, toaster) {
        $scope.model = {};
        $scope.results;

        $scope.propertyName = 'FieldId';
        $scope.reverse = false;

        $scope.sortBy = function (propertyName) {
            // reverse current or false for new property
            $scope.reverse = ($scope.propertyName === propertyName) ? !$scope.reverse : false;
            $scope.propertyName = propertyName;
        };

        $scope.submitSearch = function () {
            $http({
                url: ServerRoutes.reports.fields,
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;

                if (angular.equals($scope.results,[])) {
                    toaster.info('לא נמצאו מגרשים העונים על הדרישה');
                }
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
                    if (response.status == 200) {
                        $location.path('/editField/' + response.data.Id);
                        toaster.success('המגרש נשמר בהצלחה');
                    }
                });
            }
            else {
                $http({
                    url: ServerRoutes.fields,
                    method: "PUT",
                    params: { id: $scope.model.Id },
                    data: $scope.model,
                }).then(function searchCompleted(response) {
                    if (response.status == 200) {
                        $scope.originalModel = angular.copy($scope.model);
                        toaster.success('נתוני המגרש עודכנו בהצלחה');
                    }
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
                if (response.status == 200) {
                    $location.path('/reportFields');
                    toaster.success('המגרש נמחק בהצלחה');
                }
            });
        };

        init();
    }]);

    myApp.controller('SearchEmployeesCtrl', ['$scope', '$http', 'ServerRoutes', 'toaster', function ($scope, $http, ServerRoutes) {
        $scope.model = {};
        $scope.results;

        $scope.submitSearch = function () {
            $http({
                url: ServerRoutes.employees,
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;

                if (angular.equals($scope.results, [])) {
                    toaster.info('לא נמצאו עובדים העונים על הדרישה');
                }
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
                    if (response.status == 200) {
                        if (response.data.AlreadyExists) {
                            toaster.error("אופס!", "שם משתמש כבר קיים באתר!", 5000);
                            return;
                        }

                        // TODO: is this line needed?
                        $scope.model.verifiedPassword = $scope.model.employee.Password;
                        toaster.success('העובד נשמר בהצלחה');
                        $location.path('/editEmployee/' + response.data.Employee.Id);
                    }
                });
            }
            else {
                $http({
                    url: ServerRoutes.employees,
                    method: "PUT",
                    params: { id: $scope.model.employee.Id },
                    data: $scope.model.employee,
                }).then(function searchCompleted(response) {
                    if (response.status == 200) {
                        $scope.originalModel = angular.copy($scope.model.employee);
                        toaster.success('פרטי העובד עודכנו בהצלחה');
                    }
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
                if (response.status == 200) {
                    $location.path('/editEmployee');
                    toaster.success('העובד נמחק בהצלחה');
                }
            });
        };

        init();
    }]);

    myApp.controller('ReportCustomerCtrl', ['$scope', '$http', 'ServerRoutes', 'toaster', function ($scope, $http, ServerRoutes, toaster) {
        $scope.model = {};
        $scope.results;

        $scope.submitSearch = function () {
            $http({
                url: ServerRoutes.reports.customers,
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;

                if (angular.equals($scope.results, [])) {
                    toaster.info('לא נמצאו לקוחות העונים על הדרישה');
                }
            });
        }
    }]);

    myApp.controller('ReportComplaintCtrl', ['$scope', '$http', 'ServerRoutes', 'DomainDecodes', 'toaster', function ($scope, $http, ServerRoutes, DomainDecodes, toaster) {
        $scope.model = {};
        $scope.types = DomainDecodes.complaintType;
        $scope.results;
        $scope.submitted = false;
        $scope.model.untilDate = new Date();

        $scope.submitSearch = function (isValid) {
            $scope.submitted = true;

            if (!isValid)
                return;

            $http({
                url: ServerRoutes.reports.complaints,
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;

                if (angular.equals($scope.results, [])) {
                    toaster.info('לא נמצאו נתונים העונים על הדרישה');
                }

                $scope.submitted = false;
            });
        }
    }]);

    myApp.controller('SearchCustomersCtrl', ['$scope', '$http', 'ServerRoutes', 'DomainDecodes', 'toaster', function ($scope, $http, ServerRoutes, DomainDecodes, toaster) {
        $scope.model = {};
        $scope.types = DomainDecodes.regionDecode;
        $scope.results;
        $scope.submitSearch = function () {
            $http({
                url: ServerRoutes.customers,
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;

                if (angular.equals($scope.results, [])) {
                    toaster.info('לא נמצאו לקוחות העונים על הדרישה');
                }
            });
        }
    }]); 

    myApp.controller('SearchAvailableOrdersCtrl', ['$scope', '$http', 'ServerRoutes', 'DomainDecodes', 'toaster', function ($scope, $http, ServerRoutes, DomainDecodes, toaster) {
        $scope.model = {};
        $scope.types = DomainDecodes.fieldType;
        $scope.submitted = false;
        $scope.results;

        var today = new Date();
        today.setHours(0, 0, 0, 0);

        $scope.model.date = today;

        $scope.submitSearch  = function (isValid) {
            $scope.submitted = true;

            if (!isValid)
                return;

            $http({
                url: ServerRoutes.orders.availables,
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = angular.copy(response.data);
                $scope.submitted = false;

                if (angular.equals($scope.results, [])) {
                    toaster.info('לא נמצאו הזמנות העונות על הדרישה');
                }
            });
        }
    }]); 

    myApp.controller('ownedOrdersCrtl', ['$scope', '$http', 'ServerRoutes', 'DomainDecodes', 'toaster', function ($scope, $http, ServerRoutes, DomainDecodes, toaster) {
        $scope.model = {};
        $scope.types = DomainDecodes.orderStatus;
        $scope.results;
        var today = new Date();
        today.setHours(0, 0, 0, 0);
        $scope.model.fromDate = today;

        $scope.submitSearch = function () {
            $http({
                url: ServerRoutes.orders.searchmyorders,
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;

                if (angular.equals($scope.results, [])) {
                    toaster.info('לא נמצאו הזמנות העונים על הדרישה');
                }
            });
        }
    }]); 

    myApp.controller('OrdersCtrl', ['$rootScope', '$scope', '$http', '$routeParams', '$location', 'DomainDecodes', 'ServerRoutes', 'toaster', function ($rootScope, $scope, $http, $routeParams, $location, DomainDecodes, ServerRoutes, toaster) {

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

                if (angular.equals($scope.optinalOrders, [])) {
                    toaster.info('לא נמצאו מועדים פנויים');
                }
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
            $scope.fieldType = DomainDecodes.fieldType;

            $scope.model = {};
            $scope.originalModel = {};
            $scope.optinalOrders = {};

            $scope.lessThen24Hours = false;

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


                    var tempDate = new Date($scope.model.StartDate);
                    tempDate.setDate(tempDate.getDate() - 1);

                    $scope.lessThen24Hours = new Date() > tempDate;
                });  
            }
            else {
                $scope.isNew = true;
                $scope.model = angular.fromJson($routeParams.order);
                $scope.model.Owner = { Id: $rootScope.sharedVariables.userId };
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
                    if (response.status == 200) {
                        $scope.model = angular.copy(response.data);
                        $scope.originalModel = angular.copy($scope.model);
                        toaster.success('ההזמנה נשמרה בהצלחה');
                    }
                });
            }
            else {
                $http({
                    url: ServerRoutes.orders.base,
                    method: "PUT",
                    params: { id: $scope.model.Id },
                    data: modalToSend,
                }).then(function searchCompleted(response) {
                    if (response.status == 200) {
                        $scope.originalModel = angular.copy($scope.model);
                        toaster.success('ההזמנה עודכנה בהצלחה');
                    }
                });
            }
        };

        $scope.cancelOrder = function () {

            $scope.originalModel.Status = 4;
            var modalToSend = angular.copy($scope.originalModel);
            //modalToSend.StartDate = new Date(modalToSend.StartDate).getTime();

            $http({
                url: ServerRoutes.orders.base,
                method: "PUT",
                params: { id: $scope.originalModel.Id },
                data: modalToSend,
            }).then(function searchCompleted(response) {
                //$scope.originalModel = angular.copy($scope.model);
                if (response.status == 200) {
                    toaster.success('ההזמנה בוטלה בהצלחה');
                    $location.path('/ownedOrders');
                }
            });
        }

        $scope.cancelChanges = function () {           
            $scope.model = angular.copy($scope.originalModel);
            $scope.fieldTypeChanged();
        };


        $scope.acceptRejectOrder = function (newStatus) {
            $scope.model.Status = newStatus;

            $http({
                url: ServerRoutes.orders.base,
                method: "PUT",
                params: { id: $scope.model.Id },
                data: $scope.model,
            }).then(function searchCompleted(response) {
                if (response.status == 200) {
                    toaster.success('הפעולה בוצעה בהצלחה');
                    $location.path('/searchOrders');
                }
            });
        }

        init();
    }]);

    myApp.controller('SearchAvailableOrdersToJoinCrtl', ['$scope', '$route', '$http', 'DomainDecodes', 'ServerRoutes', 'toaster', function ($scope, $route, $http, DomainDecodes, ServerRoutes, toaster) {
        $scope.model = {};
        $scope.orderStatuses = DomainDecodes.orderStatus;
        $scope.results;

        var today = new Date();
        today.setHours(0, 0, 0, 0);
        $scope.model.fromDate = today;
        
        $scope.submitSearch = function (isValid) {
            $scope.submitted = true;

            if (!isValid)
                return;

            $http({
                url: ServerRoutes.orders.availablestojoin,
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;

                if (angular.equals($scope.results, [])) {
                    toaster.info('לא נמצאו הזמנות העונות על הדרישה');
                }
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
                if (response.status == 200) {
                    toaster.success('בקשת ההצטרפות נשמרה בהצלחה');
                    $scope.results.splice(index, 1);
                }
            });
        }
    }]); 

    myApp.controller('SearchOrdersCrtl', ['$scope', '$route', '$http', 'DomainDecodes', 'ServerRoutes', 'toaster',
    function ($scope, $route, $http, DomainDecodes, ServerRoutes, toaster) {

        $scope.model = {};
        $scope.orderStatuses = DomainDecodes.orderStatus;
        $scope.results;

        var today = new Date();
        today.setHours(0, 0, 0, 0);
        $scope.model.fromDate = today;

        $scope.submitSearch = function () {

            $http({
                url: ServerRoutes.orders.search,
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;

                if (angular.equals($scope.results, [])) {
                    toaster.info('לא נמצאו הזמנות העונות על הדרישה');
                }
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

                toaster.success('הפעולה בוצעה בהצלחה');
            });
        }
    }]); 

    myApp.controller('PendingOrdersToJoinCrtl', ['$scope', '$route', '$http', 'DomainDecodes', 'ServerRoutes', 'toaster', function ($scope, $route, $http, DomainDecodes, ServerRoutes, toaster) {
        $scope.model = {};
        $scope.orderStatuses = DomainDecodes.orderStatus;
        $scope.invitationStatuses = DomainDecodes.invitationStatus;
        $scope.results;

        var today = new Date();
        today.setHours(0, 0, 0, 0);
        $scope.model.fromDate = today;

        $scope.submitSearch = function () {
            $http({
                url: ServerRoutes.participants,
                method: "GET",
                params: $scope.model,
            }).then(function searchCompleted(response) {
                $scope.results = response.data;

                if (angular.equals($scope.results, [])) {
                    toaster.info('לא נמצאו הזמנות העונות על הדרישה');
                }
            });
        }

        //$scope.submitSearch();

        $scope.cancelJoining = function (participantId, index) {

            $http({
                url: ServerRoutes.participants,
                method: "DELETE",
                params: { id: participantId }
            }).then(function searchCompleted(response) {
                if (response.status == 200) {
                    $scope.results.splice(index, 1);
                    toaster.success('הפעולה בוצעה בהצלחה');
                }
            });
        }
    }]);

    myApp.controller('CustomersCtrl', ['$scope', '$http', '$routeParams', '$location', 'DomainDecodes', 'ServerRoutes', 'toaster', function ($scope, $http, $routeParams, $location, DomainDecodes, ServerRoutes, toaster) {
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
                if (response.status == 200) {
                    $scope.originalCustomer = angular.copy($scope.model.customer);
                    toaster.success('נתוני הלקוח נשמרו בהצלחה');
                    $scope.submitted = false;
                }
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
                if (response.status == 200) {
                    toaster.success('פרטי חוות הדעת נשמרו בהצלחה');
                    $scope.model.review = {};
                }
            });
        };

        $scope.saveComplaint = function () {

            $scope.model.complaint.Date = new Date();
            $scope.model.complaint.OffendingCustomer = {};
            $scope.model.complaint.OffendingCustomer.Id = $scope.model.customer.Id;
            $http({
                url: ServerRoutes.complaints.base,
                method: "POST",
                data: $scope.model.complaint,
            }).success(function searchCompleted(response) {
                if (response.status == 200) {
                    toaster.success('פרטי התלונה נשמרו בהצלחה');
                    $scope.model.complaint = {};
                }
            });
        };

        $scope.freezeCustomer = function () {
            var freezeDate = new Date();
            freezeDate.setDate(freezeDate.getDate() + 30);
            $scope.model.customer.FreezeDate = freezeDate;
            $scope.submitCustomer(true);
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
        $scope.model = {};

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
