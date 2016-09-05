!(function () {
    var myApp = angular.module('myApp');
    myApp.controller('MyCtrl1', ['$scope', '$http', function ($scope, $http) {
        $scope.model = {};

        $scope.submitUser = function () {
            $http({
                method: 'POST',
                url: 'http://localhost:59233/users'
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
            });

            $http.post('http://localhost:59233/users', $scope.model).then(successCallback, errorCallback);
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
            $scope.sizes = ["קטן", "בינוני", "גדול"];
            $scope.types = ["כדורגל", "כדורסל", "טניס"];

            $scope.model = {};
            $scope.originalModel = {};

            if ($routeParams.Id) {
                $scope.isNew = false;

                $http({
                    url: 'http://localhost:59233/api/fields',
                    method: "GET",
                    params: { id: $routeParams.Id },
                }).then(function searchCompleted(response) {
                    $scope.model.Id = response.data.Id;
                    $scope.model.Name = response.data.Name;
                    $scope.model.Size = response.data.Size.toString();
                    $scope.model.Type = response.data.Type.toString();

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
            $scope.model = {};
        };

        init();
    }]);
})();
