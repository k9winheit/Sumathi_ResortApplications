app.controller('reservationordercontroller', ['$rootScope', '$scope', 'reservationservice', function ($rootScope, $scope, reservationservice) {

    $scope.Rooms = {};
    $scope.EmptyCompany = [];
    $scope.IsEdit = false;
    $scope.IsView = false;
    $scope.IsDelete = false;
    $scope.IsCreateNew = false;


    $scope.Room = {};
    $scope.SelectRoomType = {};

    getRooms();
    function getRooms() {
        //busyIndicatorService.showBusy("Loading data...");
        reservationservice.GetRooms().then(function (data) {
            $scope.Rooms = data.RoomList;
            $scope.Room = data.Room;
            //busyIndicatorService.stopBusy();
        }, function (error) {
            //busyIndicatorService.stopBusy();
            //toastrService.showErrorMessage("Error", 'Network error occured..Please try again later.');
        });
    };

    function getEmptyCompany() {
        //busyIndicatorService.showBusy("Loading data...");
        reservationservice.getEmptyCompany().then(function (data) {
            data.IndustryCodeFilterViewModel = data.IndustryCodeFilterViewModels[0];
            $scope.EmptyCompany = data;
            //busyIndicatorService.stopBusy();
            getCompanies();
        }, function (error) {
            // busyIndicatorService.stopBusy();
            //toastrService.showErrorMessage("Error", 'Network error occured..Please try again later.');
        });
    };


    $scope.saveroom = function (room) {
        //busyIndicatorService.showBusy("Loading data...");
        reservationservice.SaveRoom(room).then(function (data) {
            //busyIndicatorService.stopBusy();
            if (data.IsSuccess) {
                //toastrService.showSuccessMessage("Success", data.Message);
                $scope.Rooms = data.RoomList;
                $scope.Room = data.Room;
            }
            else {
                //toastrService.showErrorMessage("Error", data.Message);
            }

        }, function (error) {
            //busyIndicatorService.stopBusy();
            //toastrService.showErrorMessage("Error", 'Network error occured..Please try again later.');
        });
    };


    $scope.saveCompany = function (companyVm) {
        //busyIndicatorService.showBusy("Loading data...");
        reservationservice.saveCompany(companyVm).then(function (data) {
            //busyIndicatorService.stopBusy();
            if (data.IsSuccess) {
                //toastrService.showSuccessMessage("Success", data.Message);
                companyVm.Id = data.Id;
                companyVm.IsRowHide = true;
            }
            else {
                toastrService.showErrorMessage("Error", data.Message);
            }

        }, function (error) {
            //busyIndicatorService.stopBusy();
            //toastrService.showErrorMessage("Error", 'Network error occured..Please try again later.');
        });
    };




}]);