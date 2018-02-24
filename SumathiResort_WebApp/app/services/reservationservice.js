app.factory('reservationservice', ['$http', '$q', 'ngAuthSettings', function ($http, $q, ngAuthSettings) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var loginBase = ngAuthSettings.loginServiceBasUri;
    var orgName = ngAuthSettings.clientId;
    var accountServiceFactory = {};  


    //var GetRooms = function () {
    //    var defered = $q.defer();
    //    $http({
    //        method: 'GET',
    //        url: serviceBase + "/Room/GetRoomsDetails",
    //        headers: {
    //            'Content-Type': 'application/json'
    //        }
    //    }).then(function successCallback(response) {
    //        defered.resolve(response.data);
    //        //token = response.data;
    //    }, function errorCallback(response) {
    //        defered.reject(response);
    //    });

    //    return defered.promise;
    //}

    var GetRooms = function () {
        var defered = $q.defer();
        $http({
            method: 'GET',
            url: serviceBase + "/Room/GetRoomsDetails"
        }).then(function successCallback(response) {
            defered.resolve(response.data);
        }, function errorCallback(response) {
            defered.reject(response);
        });

        return defered.promise;
    }

    var SaveRoom = function (roomVm) {
        var defered = $q.defer();
        $http.post(serviceBase + '/Room/SaveRoom', roomVm).then(function successCallback(response) {
            defered.resolve(response.data);
        }, function errorCallback(response) {
            defered.reject(response);
        });

        return defered.promise;
    };

    var GetEmptyReservationOrder = function () {
        var defered = $q.defer();
        $http({
            method: 'GET',
            url: serviceBase + "/ReservationOrder/GetEmptyOrder"
        }).then(function successCallback(response) {
            defered.resolve(response.data);
        }, function errorCallback(response) {
            defered.reject(response);
        });

        return defered.promise;
    }

    var SaveOrder = function (order) {
        var defered = $q.defer();
        $http.post(serviceBase + '/ReservationOrder/SaveReservationOrder', order).then(function successCallback(response) {
            defered.resolve(response.data);
        }, function errorCallback(response) {
            defered.reject(response);
        });

        return defered.promise;
    };

    var GetReservationDetails = function (res_id) {
        var defered = $q.defer();
        $http({
            method: 'GET',
            url: serviceBase + "/ReservationOrder/GetReservationDetails",
            params: {
                res_id: res_id
            }
        }).then(function successCallback(response) {
            defered.resolve(response.data);
        }, function errorCallback(response) {
            defered.reject(response);
        });

        return defered.promise;
    }
   
    accountServiceFactory.GetRooms = GetRooms;
    accountServiceFactory.SaveRoom = SaveRoom; SaveOrder
    accountServiceFactory.GetEmptyReservationOrder = GetEmptyReservationOrder;
    accountServiceFactory.SaveOrder = SaveOrder;
    accountServiceFactory.GetReservationDetails = GetReservationDetails;
   

    return accountServiceFactory;

}]);