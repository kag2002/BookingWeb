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

        public double GiaPhongTheoDem { get; set; }

        public double GiaGoiDichVuThem { get; set; }

        public double UuDai { get; set; }

        public int TongSlPhong { get; set; }

        public int SLPhongTrong { get; set; }

        public List<string> TenDichVuTienIch { get; set; }

    }
}
