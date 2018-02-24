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
        public RoomViewModel()
        {
            RoomTypes = Enum.GetValues(typeof(RoomType)).Cast<RoomType>().Select(x => x.ToString()).ToList();
        }
        public int RoomId { get; set; }
        public string Description { get; set; }       
        public List<string> RoomTypes { get; set; }
        public string SelectedRoomType { get; set; }
        public string RoomType { get; set; }
        public int RoomFloor { get; set; }
        public double RoomRate { get; set; }
        public bool IsRemoved { get; set; }
    }
}
