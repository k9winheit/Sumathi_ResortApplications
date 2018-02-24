using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Enums;

namespace ViewModel
{
    public class RoomViewModel
    {
        public int RoomId { get; set; }
        public string Description { get; set; }
        public RoomType RoomType { get; set; }
        public int RoomFloor { get; set; }
    }
}
