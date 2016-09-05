!(function () {
    var myApp = angular.module('myApp');
    myApp.controller('LoginCtrl', ['$scope', '$http', function ($scope, $http) {
        $scope.model = {};

        $http.defaults.headers.common['Authorization'] = "Basic abcdes"

        $scope.submitUser = function () {
            $http({
                method: 'POST',
                url: 'http://localhost:59233/api/login',
                data: $scope.model
            }).then(function(response) {

            },
            function(response) {
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
                url: 'http://localhost:59233/api/reports',
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
})();
