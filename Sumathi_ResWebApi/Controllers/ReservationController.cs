using Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel.ReservationOrder;

namespace Sumathi_ResWebApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/ReservationOrder")]
    public class ReservationController : ApiController
    {

        private ReservationManager reservationManager;      

        public ReservationController()
        {
           
        }


        [AllowAnonymous]
        //[AllowAnonymous]
        [HttpPost]
        [Route("SaveReservationOrder")]
        public RoomResultReservationOrderViewModel SaveReservationOrder(RoomReservationOrderViewModel order)
        {
            reservationManager = new ReservationManager();
            return reservationManager.SaveReservationOrder(order);
        }

        [AllowAnonymous]
        //[AllowAnonymous]
        [HttpGet]
        [Route("GetEmptyOrder")]
        public RoomReservationOrderViewModel GetEmptyOrder()
        {
            reservationManager = new ReservationManager();
            return reservationManager.GetEmptyReservationOrder();
        }
    }
}
