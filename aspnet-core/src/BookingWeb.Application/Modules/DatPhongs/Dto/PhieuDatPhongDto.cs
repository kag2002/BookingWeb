using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.DatPhongs.Dto
{
    public class PhieuDatPhongDto
    {
        public int Id { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayHenTra { get; set; }

        public string KhachHang { get; set; }

        public string NhanVien { get; set; }
    }
}
