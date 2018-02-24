using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Business.Managers.Interfaces
{
    public interface IRoomManager
    {
        List<RoomViewModel> GetAllRooms();
    }
}
