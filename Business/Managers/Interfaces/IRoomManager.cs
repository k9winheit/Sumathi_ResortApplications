using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.Room;

namespace Business.Managers.Interfaces
{
    public interface IRoomManager
    {
        RoomResultViewModel GetAllRooms();
        RoomResultViewModel SaveRoomDetails(RoomViewModel roomVm);
    }
}
