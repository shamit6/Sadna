!(function () {
    myApp = angular.module('myApp');
    myApp.factory("MyDecodes", function(){
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

