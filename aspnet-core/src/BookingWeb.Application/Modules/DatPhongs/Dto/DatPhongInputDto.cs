using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.DatPhongs.Dto
{
    public class DatPhongInputDto
    {
        public DateTime NgayDatDuKien { get; set; }

        public DateTime NgayTraDuKien { get; set; }

        public int KhachHangId { get; set; }

        public int NhanVienId { get; set; }

        public int TrangThaiPhong { get; set; }

    }
}
