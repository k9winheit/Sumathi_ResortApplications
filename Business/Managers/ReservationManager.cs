using Business.Managers.Common;
using Business.Managers.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.ReservationOrder;

namespace Business.Managers
{
    public class ReservationManager : IReservationManager
    {
        public RoomResultReservationOrderViewModel SaveReservationOrder(RoomReservationOrderViewModel order)
        {
            var result = new RoomResultReservationOrderViewModel();
            if (GetReservationById(order.ResNo))
            {
                result = UpdateReservationOrder(order);
            }
            else
            {
                result = CreateReservationOrder(order);
            }

            return result;
        }

        public RoomReservationOrderViewModel GetEmptyReservationOrder()
        {
            var reservationOrder = new RoomReservationOrderViewModel();
            reservationOrder.roolListVm = GetRoomDetails();

            return reservationOrder;
        }

        #region PRIVATE METHODS
        private bool GetReservationById(int resId)
        {
            bool isOrderAvailable = false;
            try
            {
                string getOrderDetails = "SELECT * FROM tblt_resheader WHERE Res_no = '" + resId + "'";
                MySqlDataReader orderReader = DbConnetClass.getData(getOrderDetails);
                if (orderReader.Read())
                {
                    isOrderAvailable = orderReader.HasRows;
                }
            }
            catch (Exception ex)
            {

            }

            return isOrderAvailable;
        }

        private RoomResultReservationOrderViewModel CreateReservationOrder(RoomReservationOrderViewModel order)
        {
            var reservationResult = new RoomResultReservationOrderViewModel();
            try
            {
                string insertGuestInfo = string.Format("INSERT INTO tblt_resheader VALUES('{0}','{1}',{2}','{3}',{4} ,{5},{6},{7},{8},{9},{10})", order.ResNo, order.Title, order.FirstName, order.LastName, order.Address, order.ContactNo, 1, 1, DateTime.Now, DateTime.Now, 1);
                int val = DbConnetClass.setData(insertGuestInfo);
                if (val > 0)
                {
                    order.roolListVm.ForEach(t =>
                    {
                        string insertRoomReseDetails = string.Format("INSERT INTO tblt_resdet VALUES({0},'{1}',{2}','{3}',{4} ,{5},{6},{7},{8})", order.ResNo, t.RoomId, t.RoomRate, "LKR", 1, 1, DateTime.Now, DateTime.Now, 1);
                        DbConnetClass.setData(insertRoomReseDetails);
                    });

                    reservationResult.IsSuccess = true;
                    reservationResult.Message = "Reservation is saved successfully";
                }
            }
            catch (Exception ex)
            {

            }

            return reservationResult;
        }

        private RoomResultReservationOrderViewModel UpdateReservationOrder(RoomReservationOrderViewModel order)
        {
            var reservationResult = new RoomResultReservationOrderViewModel();
            try
            {
                string insertGuestInfo = string.Format("UPDATE tblt_resheader Cus_title='{0}',Cus_FName='{1}',Cus_LName={2}',Cus_Addr='{3}',Cus_Cont={4} ,updated_by={5},updated_on={6},isactive={7}", order.Title, order.FirstName, order.LastName, order.Address, order.ContactNo, 1, DateTime.Now, 1);
                int val = DbConnetClass.setData(insertGuestInfo);
                if (val > 0)
                {
                    order.roolListVm.ForEach(t =>
                    {

                    });

                    reservationResult.IsSuccess = true;
                    reservationResult.Message = "Reservation is saved successfully";
                }
            }
            catch (Exception ex)
            {

            }

            return reservationResult;
        }

        private List<RoomViewModel> GetRoomDetails()
        {
            var roomsVm = new List<RoomViewModel>();
            try
            {
                string getRoomDataReader = "SELECT room_no,room_desc,room_floor FROM tblr_room";
                MySqlDataReader roomReader = DbConnetClass.getData(getRoomDataReader);
                while (roomReader.Read())
                {
                    var room = new RoomViewModel()
                    {
                        RoomId = int.Parse(roomReader["room_no"].ToString()),
                        Description = roomReader["room_desc"].ToString(),
                        //RoomType = ViewModel.Enums.RoomType,
                        RoomFloor = int.Parse(roomReader["room_floor"].ToString()),
                    };
                    roomsVm.Add(room);
                }
            }
            catch (Exception ex)
            {

            }

            return roomsVm;
        } 

        #endregion


    }
}
