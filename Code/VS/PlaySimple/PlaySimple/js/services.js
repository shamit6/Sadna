!(function () {
    myApp = angular.module('myApp');

    myApp.factory("LoginService", ['$rootScope', '$location', '$http', function ($rootScope, $location, $http) {
        return {
            hasPreviousLogin: function hasPreviousLogin() {
                return localStorage.getItem("currLogin") != null;
            },

            saveLogin: function saveLogin(login) {
                localStorage.setItem("currLogin", JSON.stringify(login))
            },

            deleteLogin: function deleteLogin() {
                localStorage.removeItem("currLogin");
                $location.path('/login');
            },

            navigateToHomepage: function navigateToHomepage() {
                var loginData = JSON.parse(localStorage.getItem("currLogin"));

                $http.defaults.headers.common['Authorization'] = loginData.AuthorizationKey;
                $rootScope.sharedVariables.role = loginData.Role;

                if (loginData.Role == "Admin") { // searchCustomers
                    $location.path('/searchCustomers');
                }
                else if (loginData.Role == "Employee") { // 
                    $location.path('/ownedPendingInvitations');
                }
                else {
                    $location.path('/ownedInvitations');
                }
            }
        };
    }]);

    myApp.factory("ServerRoutes", function () {
        var urlBase = "http://localhost:59233/api/";

        return {
            fields: urlBase + "fields",
            employees: urlBase + "employees",
            costumers: urlBase + "costumers",
            login: urlBase + "login",
            orders: urlBase + "orders",
            reports: {
                fields: urlBase + "reports/fields",
                customers: urlBase + "reports/customers",
                complaints: urlBase + "reports/complaints"
            }
        }
    });

    myApp.factory("DomainDecodes", function(){
        return {
            orderStatus: [
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
            complaintType: [
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
            ],
            fieldSize: [
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
                }
            ],
            fieldType: [
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
            invitationStatus: [
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
                }
            ],
            regionDecode: [
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
         ]};
    });
})();

