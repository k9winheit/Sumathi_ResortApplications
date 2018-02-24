using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ReservationOrder
{
    public class RoomResultReservationOrderViewModel
    {
        public long Id { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
       // public List<RoomViewModel> RoomList { get; set; }
    }
}
