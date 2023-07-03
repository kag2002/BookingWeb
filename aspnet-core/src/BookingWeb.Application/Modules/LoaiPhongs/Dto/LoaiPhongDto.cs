using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.LoaiPhongs.Dto
{
    public class LoaiPhongDto
    {
        public string TenLoaiPhong { get; set; }

        public string MoTaLoaiPhong { get; set; }

        public int SucChua { get; set; }

        public string TienNghiPhong { get; set; }

        public float GiaPhongTheoDem { get; set; }

        public float GiaGoiDichVuThem { get; set; }

        public float UuDai { get; set; }
    }
}
