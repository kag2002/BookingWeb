using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.LoaiPhongs.Dto
{
    public class LoaiPhongOutputDto
    {
        public int Id { get; set; }

        public string TenLoaiPhong { get; set; }

        public int SucChua { get; set; }

        public string MoTa { get; set; }

        public string TienNghiTrongPhong { get; set; }

        public float GiaPhongTheoDem { get; set; }

        public float GiaGoiDichVuThem { get; set; }

        public float UuDai { get; set; }

        public List<string> TenDichVuTienIch { get; set; }

    }
}
