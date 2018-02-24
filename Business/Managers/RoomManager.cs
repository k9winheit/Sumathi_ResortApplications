using Business.Managers.Common;
using Business.Managers.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.Room;

namespace Business.Managers
{
    public class RoomManager : IRoomManager
    {
        public RoomResultViewModel GetAllRooms()
        {
            var result = new RoomResultViewModel();
            List<RoomViewModel> roomList = GetRoomDetails();
            result.Room = new RoomViewModel();
            result.RoomList = roomList;

            return result;
        }

        public RoomResultViewModel SaveRoomDetails(RoomViewModel roomVm)
        {
            var result = new RoomResultViewModel();
            if (GetRoomById(roomVm.RoomId))
            {
                result = UpdateRoom(roomVm);
            }
            else
            {
                result = CreateRoom(roomVm);
            }

            return result;
        }

        #region PRIVATE METHODS
        private bool GetRoomById(int roomId)
        {
            bool isRoomAvailable = false;
            try
            {
                string getRoomDataReader = "SELECT room_no,room_desc,room_floor FROM tblr_room WHERE room_no = '" + roomId + "'";
                MySqlDataReader roomReader = DbConnetClass.getData(getRoomDataReader);
                if (roomReader.Read())
                {
                    isRoomAvailable = roomReader.HasRows;
                }
            }
            catch (Exception ex)
            {

            }

            return isRoomAvailable;
        }

        private RoomResultViewModel CreateRoom(RoomViewModel roomVm)
        {
            var result = new RoomResultViewModel();
            List<RoomViewModel> roomList = null;
            try
            {
                string roomInsQuery = string.Format("INSERT INTO tblr_room VALUES({0},'{1}','{2}','{3}',{4} ,{5},'{6}','{7}',{8},{9})", roomVm.RoomId, roomVm.Description, roomVm.SelectedRoomType, roomVm.RoomFloor, 1, 1, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 1,roomVm.RoomRate);
                int val = DbConnetClass.setData(roomInsQuery);
                if (val > 0)
                {
                    result.IsSuccess = true;
                    result.Message = "Room is saved successfully";
                }

                roomList = GetRoomDetails();
                result.Room = new RoomViewModel();
                result.RoomList = roomList;
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        private RoomResultViewModel UpdateRoom(RoomViewModel roomVm)
        {
            var result = new RoomResultViewModel();
            List<RoomViewModel> roomList = null;
            try
            {
                string roomInsQuery = string.Format("UPDATE tblr_room SET room_desc={0},room_type={1},room_floor={2},updated_by={3},updated_on={4},isactive={5}", roomVm.Description, roomVm.SelectedRoomType, roomVm.RoomFloor, 1, DateTime.Now, 1);
                int val = DbConnetClass.setData(roomInsQuery);
                if (val > 0)
                {
                    result.IsSuccess = true;
                    result.Message = "Room is saved successfully";
                }

                roomList = GetRoomDetails();
                result.RoomList = roomList;
            }
            catch (Exception)
            {

                throw;
            }

            return result;
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
