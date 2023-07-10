using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.Phongs.Dto
{
    public class PhongDto
    {
        public string Mota { get; set; }

        public int TrangThaiPhong { get; set; }

        public string DiaChiChiTiet { get; set; }

        public string TenFileAnhDaiDien { get; set; }

        public int? DiaDiemId { get; set; }

        public int? LoaiPhongId { get; set; }

        public int? HinhThucPhongId { get; set; }
    }
}
