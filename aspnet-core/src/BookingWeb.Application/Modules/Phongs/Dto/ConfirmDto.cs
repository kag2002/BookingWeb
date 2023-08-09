using BookingWeb.Modules.SearchingFilter.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.Phongs.Dto
{
    public class ConfirmDto
    {
        public GetInfoRoomToBookOutputDto infoRoom { get; set; }

        public ClientBookRoomOutputDto infoClient { get; set; }

        public InfoBookingDto infoBooking { get; set; }
    }
}
