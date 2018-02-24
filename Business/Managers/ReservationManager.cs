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

        public RoomReservationOrderViewModel GetReservationDetails(int resId)
        {
            var reservationOrder = new RoomReservationOrderViewModel();
            reservationOrder.roolListVm = GetRoomDetails();
            try
            {
                string getOrderDetails = "SELECT * FROM tblt_resheader WHERE Res_no = '" + resId + "'";
                MySqlDataReader orderReader = DbConnetClass.getData(getOrderDetails);
                if (orderReader.Read())
                {
                    reservationOrder.ResNo = int.Parse(orderReader["Res_no"].ToString());
                    reservationOrder.Title = orderReader["Cus_title"].ToString();
                    reservationOrder.FirstName = orderReader["Cus_FName"].ToString();
                    reservationOrder.LastName = orderReader["Cus_LName"].ToString();
                    reservationOrder.Address = orderReader["Cus_Addr"].ToString();
                    reservationOrder.ContactNo = orderReader["Cus_Cont"].ToString();
                    reservationOrder.CheckInDate = DateTime.Parse(orderReader["checkindate"].ToString());
                    reservationOrder.CheckOutDate = DateTime.Parse(orderReader["checkoutdate"].ToString());

                    string getRooms = "SELECT r.room_no,r.room_desc,r.room_type,r.room_floor,r.room_rate FROM tblt_resdet o, tblr_room r WHERE o.Room_no = r.room_no AND o.Res_no = '" + resId + "'";
                    MySqlDataReader orderRoomReader = DbConnetClass.getData(getRooms);
                    while (orderRoomReader.Read())
	                {
                        var val = orderRoomReader["room_no"].ToString();
                        var room = new RoomViewModel()
                        {
                            RoomId = int.Parse(orderRoomReader["room_no"].ToString()),
                            Description = orderRoomReader["room_desc"].ToString(),
                            RoomType = orderRoomReader["room_type"].ToString(),
                            RoomFloor = int.Parse(orderRoomReader["room_floor"].ToString()),
                            RoomRate = double.Parse(orderRoomReader["room_rate"].ToString()),
                        };

                        reservationOrder.selectedRooms.Add(room);


                    }
                }
            }
            catch (Exception ex)
            {

            }

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
                string insertGuestInfo = string.Format("INSERT INTO tblt_resheader VALUES({0},'{1}','{2}','{3}','{4}' ,'{5}',{6},{7},'{8}','{9}',{10},'{11}','{12}')", order.ResNo, order.Title, order.FirstName, order.LastName, order.Address, order.ContactNo, 1, 1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 1,order.CheckInDate.ToString("yyyy-MM-dd HH:mm:ss"), order.CheckInDate.ToString("yyyy-MM-dd HH:mm:ss"));
                int val = DbConnetClass.setData(insertGuestInfo);
                if (val > 0)
                {
                    order.selectedRooms.ForEach(t =>
                    {
                        string insertRoomReseDetails = string.Format("INSERT INTO tblt_resdet VALUES({0},{1},{2},'{3}',{4} ,{5},'{6}','{7}',{8})", order.ResNo, t.RoomId, t.RoomRate, "LKR", 1, 1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 1);
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
                string insertGuestInfo = string.Format("UPDATE tblt_resheader SET Cus_title='{0}',Cus_FName='{1}',Cus_LName='{2}',Cus_Addr='{3}',Cus_Cont='{4}' ,updated_by={5},updated_on={6},isactive={7} WHERE Res_no = '"+ order.ResNo + "'", order.Title, order.FirstName, order.LastName, order.Address, order.ContactNo, 1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 1);
                int val = DbConnetClass.setData(insertGuestInfo);
                if (val > 0)
                {
                    order.roolListVm.ForEach(t =>
                    {
                        string getOrderDetails = "SELECT * FROM tblt_resdet WHERE Res_no = '" + order.ResNo + "' AND Room_no = '"+ t.RoomId + "'";
                        MySqlDataReader orderReader = DbConnetClass.getData(getOrderDetails);
                        if (!orderReader.HasRows)
                        {
                            string insertRoomReseDetails = string.Format("INSERT INTO tblt_resdet VALUES({0},{1},{2},'{3}',{4} ,{5},'{6}','{7}',{8})", order.ResNo, t.RoomId, t.RoomRate, "LKR", 1, 1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 1);
                            DbConnetClass.setData(insertRoomReseDetails);
                        }
                        else
                        {
                            if (t.IsRemoved)
                            {
                                string deleteRoomReserved = "DELETE FROM tblt_resdet WHERE WHERE Res_no = '" + order.ResNo + "' AND Room_no = '" + t.RoomId + "'";
                                DbConnetClass.setData(deleteRoomReserved); 
                            }
                        }
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
                string getRoomDataReader = "SELECT room_no,room_desc,room_floor,room_rate FROM tblr_room";
                MySqlDataReader roomReader = DbConnetClass.getData(getRoomDataReader);
                while (roomReader.Read())
                {
                    var room = new RoomViewModel()
                    {
                        RoomId = int.Parse(roomReader["room_no"].ToString()),
                        Description = roomReader["room_desc"].ToString(),
                        //RoomType = ViewModel.Enums.RoomType,
                        RoomFloor = int.Parse(roomReader["room_floor"].ToString()),
                        RoomRate = double.Parse(roomReader["room_rate"].ToString())
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
