using Business.Managers;
using Business.Managers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModel;

namespace Sumathi_ResWebApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Room")]
    public class RoomController : ApiController
    {
        //private IRoomManager roomManager;
        private RoomManager roomManager;
        //public RoomController(IRoomManager roomManager)
        //{
        //    this.roomManager = roomManager;
        //}

        public RoomController()
        {
        }

        #region Get Methods

        //[Authorize]
        [AllowAnonymous]
        [HttpGet]
        [Route("GetRoomsDetails")]
        public List<RoomViewModel> GetRoomsDetails()
        {
            roomManager = new RoomManager();
            return roomManager.GetAllRooms();
        }

        #endregion
    }
}
