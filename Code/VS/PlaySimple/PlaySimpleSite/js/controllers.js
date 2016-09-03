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

        var authorizationHeader = "Basic " + window.btoa("gilad:gilad");

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
})();
