!(function () {
    var myApp = angular.module('myApp');
    myApp.directive('equals', function() {
        return {
            restrict: 'A', // only activate on element attribute
            require: '?ngModel', // get a hold of NgModelController
            link: function(scope, elem, attrs, ngModel) {
                if(!ngModel) return; // do nothing if no ng-model

                // watch own value and re-validate on change
                scope.$watch(attrs.ngModel, function() {
                    validate();
                });

                // observe the other value and re-validate on change
                attrs.$observe('equals', function (val) {
                    validate();
                });

                var validate = function() {
                    // values
                    var val1 = ngModel.$viewValue;
                    var val2 = attrs.equals;

                    // set validity
                    ngModel.$setValidity('equals', val1 === val2 || (!val1 && !val2));
                };
            }
        }
    });

    myApp.directive('equalsornull', function () {
        return {
            restrict: 'A', // only activate on element attribute
            require: '?ngModel', // get a hold of NgModelController
            link: function (scope, elem, attrs, ngModel) {
                if (!ngModel) return; // do nothing if no ng-model

                // watch own value and re-validate on change
                scope.$watch(attrs.ngModel, function () {
                    validate();
                });

                // observe the other value and re-validate on change
                attrs.$observe('equals', function (val) {
                    validate();
                });

                var validate = function () {
                    // values
                    var val1 = ngModel.$viewValue;
                    var val2 = attrs.equalsornull;

                    // set validity
                    ngModel.$setValidity('equalsornull', val1 == null || val1 === val2);
                };
            }
        }
    });

    myApp.directive('past', function () {
        var today = new Date();
        today.setHours(0, 0, 0, 0);

        return {
            restrict: 'A', // only activate on element attribute
            require: '?ngModel', // get a hold of NgModelController
            link: function (scope, elem, attrs, ngModel) {
                if (!ngModel) return; // do nothing if no ng-model

                // watch own value and re-validate on change
                scope.$watch(attrs.ngModel, function () {
                    validate();
                });

                // observe the other value and re-validate on change
                attrs.$observe('equals', function (val) {
                    validate();
                });

                var validate = function () {
                    var val = ngModel.$viewValue;
                    var valDate = new Date(val);
                    valDate.setHours(0, 0, 0, 0);
                    // set validity
                    ngModel.$setValidity('past', valDate <= today);
                };
            }
        }
    });

    myApp.directive('future', function () {
        var today = new Date();
        today.setHours(0, 0, 0, 0);

        return {
            restrict: 'A', // only activate on element attribute
            require: '?ngModel', // get a hold of NgModelController
            link: function (scope, elem, attrs, ngModel) {
                if (!ngModel) return; // do nothing if no ng-model

                // watch own value and re-validate on change
                scope.$watch(attrs.ngModel, function () {
                    validate();
                });

                // observe the other value and re-validate on change
                attrs.$observe('equals', function (val) {
                    validate();
                });

                var validate = function () {
                    var val = ngModel.$viewValue;

                    // set validity
                    ngModel.$setValidity('future', new Date(val) >= today);
                };
            }
        }
    });
})();
