using BookingWeb.Modules.SearchingFilter.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.Phongs.Dto
{
    public class ClientBookRoomOutputDto
    {
        public int donViKinhDoanhId { get; set; }

        public string tenDonVi { get; set; }

        public string tenDiaDiem { get; set; }

        public int phongId { get; set; }

        public int loaiPhongId { get; set; }

        public string tenLoaiPhong { get; set; }

        public InfoBookingDto infoBookingDto { get; set; }

        public int sucChuaPhong { get; set; }

        public string moTaPhong { get; set; }

        public string tienNghi { get; set; }

        public double giaPhongTheoDem { get; set; }

        public string mienPhiHuyPhong { get; set; }

        public double TongTien { get; set; }

        public int DatHo { get; set; }

        public long CCCD { get; set; }

        public string HoTen { get; set; }

        public long SDT { get; set; }

        public string Email { get; set; }

        public string YeuCauDacBiet { get; set; }


    }
}
