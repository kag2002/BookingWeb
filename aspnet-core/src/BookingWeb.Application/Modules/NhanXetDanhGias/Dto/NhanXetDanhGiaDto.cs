using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.NhanXetDanhGias.Dto
{
    public class NhanXetDanhGiaDto
    {
        public string NhanXet { get; set; }

        public float DiemDanhGia { get; set; }

        public float DanhGiaSao { get; set; }

        public int PhongId { get; set; }

        public string KhachHang { get; set; }

        public int KhachHangId { get; set; }

    }
}
