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
        public string CCCD { get; set; }

        public string HoTen { get; set; }

        public string SDT { get; set; }

        public string Email { get; set; }

        public int DatHo { get; set; }

        public string YeuCauDacBiet { get; set; }

        public DateTime NgayDat { get; set; }

        public DateTime NgayTra { get; set; }

        public int SlNguoiLon { get; set; }

        public int SlTreEm { get; set; }

        public int SlPhong { get; set; }

        public double TongTien { get; set; }

        public int phongId { get; set; }

        public int LoaiPhongId { get; set; }
    }
}
