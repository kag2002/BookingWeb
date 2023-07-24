using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.SearchingFilter.Dto
{
    public class SearchingFilterRoomInputDto
    {
        public int DiaDiemid { get; set; }

        public InfoBookingDto infoBooking { get; set; }

        public int pageIndex { get; set; }


        public int MienPhiHuyPhong { get; set; }


        public double GiaPhongNhoNhat { get; set; }
        
        public double DanhGiaSao { get; set; }

        public double GiaPhongLonNhat { get; set; }

        public List<string> HinhThucPhong { get; set; }


        public int GiaCaoNhat { get; set; }

        public int GiaNhoNhat { get; set; }

        public int DiemDanhGia { get; set; }

        public int DoPhoBien { get; set; }


    }
}
