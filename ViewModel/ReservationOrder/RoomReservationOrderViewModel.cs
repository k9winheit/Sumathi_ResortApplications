using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.ReservationOrder
{
    public class RoomReservationOrderViewModel
    {

        public RoomReservationOrderViewModel()
        {
            roolListVm = new List<RoomViewModel>();
            selectedRooms = new List<RoomViewModel>();
        }

        public int ResNo { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public List<RoomViewModel> roolListVm { get; set; }
        public List<RoomViewModel> selectedRooms { get; set; }



    }
}
