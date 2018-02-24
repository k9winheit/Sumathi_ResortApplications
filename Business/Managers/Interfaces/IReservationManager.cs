using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ReservationOrder;

namespace Business.Managers.Interfaces
{
    public interface IReservationManager
    {
        RoomResultReservationOrderViewModel SaveReservationOrder(RoomReservationOrderViewModel order);
        RoomReservationOrderViewModel GetEmptyReservationOrder();
    }
}
