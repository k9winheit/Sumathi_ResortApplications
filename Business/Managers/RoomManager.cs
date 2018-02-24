using Business.Managers.Common;
using Business.Managers.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Business.Managers
{
    public class RoomManager : IRoomManager
    {
        public List<RoomViewModel> GetAllRooms()
        {
            var roomsVm = new List<RoomViewModel>();
            try
            {
                string getRoomDataReader = "SELECT room_no,room_desc FROM tblr_room";
                MySqlDataReader roomReader = DbConnetClass.getData(getRoomDataReader);
                while (roomReader.Read())
                {
                    var room = new RoomViewModel()
                    {
                        RoomId = int.Parse(roomReader["room_no"].ToString()),
                        Description = roomReader["room_desc"].ToString()
                    };
                    roomsVm.Add(room);
                }
            }
            catch (Exception ex)
            {
               
            }

            return roomsVm;
        }
    }
}
