app.controller('reservationordercontroller', ['$rootScope', '$scope', 'reservationservice', function ($rootScope, $scope, reservationservice) {

    $scope.GuestDetails = {};
    $scope.RoomNoList = {};
    $scope.EmptyCompany = [];
    $scope.IsEdit = false;
    $scope.IsView = false;
    $scope.IsDelete = false;
    $scope.IsCreateNew = false;
    $scope.SelectedRooms = [];
    $scope.SelectedRoom = {};

    $scope.Room = {};
    $scope.SelectRoomType = {};

    getEmptyRecord();
    function getEmptyRecord() {
        
        reservationservice.GetEmptyReservationOrder().then(function (data) {
            $scope.GuestDetails = data;
            $scope.RoomNoList = data.roolListVm;
            //busyIndicatorService.stopBusy();
        }, function (error) {
            //busyIndicatorService.stopBusy();
            //toastrService.showErrorMessage("Error", 'Network error occured..Please try again later.');
        });
    };

    $scope.add = function (selectedroom) {
        $scope.SelectedRooms.push(angular.copy(selectedroom));
        $scope.GuestDetails.selectedRooms = $scope.SelectedRooms;
    }
     
    $scope.delete = function (room) {
        for (var i = 0; i < $scope.GuestDetails.selectedRooms.length; i++) {
            if($scope.GuestDetails.selectedRooms[i].RoomId == room.RoomId){
                $scope.GuestDetails.selectedRooms[i].IsRemoved = true;
            }
        }   

        var index = functiontofindIndexByKeyValue($scope.GuestDetails.selectedRooms,"RoomId",room.RoomId);
        //$scope.GuestDetails.selectedRooms.splice(index, 1);  
        $scope.SelectedRooms.splice(index, 1);  
    }

    $scope.search = function(resid){
     reservationservice.GetReservationDetails(resid).then(function (data) {
            $scope.GuestDetails = data;
            $scope.RoomNoList = data.roolListVm;
            $scope.SelectedRooms = $scope.GuestDetails.selectedRooms;
            //busyIndicatorService.stopBusy();
        }, function (error) {
            //busyIndicatorService.stopBusy();
            //toastrService.showErrorMessage("Error", 'Network error occured..Please try again later.');
        });
    }

    $scope.saveorder = function () {
        //busyIndicatorService.showBusy("Loading data...");
        $scope.GuestDetails.CheckInDate = $('#checkindate').val();
        $scope.GuestDetails.CheckOutDate = $('#checkoutdate').val();

        reservationservice.SaveOrder($scope.GuestDetails).then(function (data) {
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

    var functiontofindIndexByKeyValue = function (arraytosearch, key, valuetosearch) {
        for (var i = 0; i < arraytosearch.length; i++) {
            if (arraytosearch[i][key] == valuetosearch) {
                return i;
            }
        }
        return null;
    }


}]);