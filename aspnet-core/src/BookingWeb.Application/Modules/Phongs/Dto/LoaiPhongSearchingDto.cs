using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.Phongs.Dto
{

    public class LoaiPhongSearchingDto
    {
        public int LoaiPhongId { get; set; }

        public string LoaiPhong { get; set; }

        public int SucChua { get; set; }

        public int TongSLPhong { get; set; }

        public int SLPhongTrong { get; set; }

        public bool MienPhiHuyPhong { get; set; }

        public double GiaPhongTheoDem { get; set; }

        public double UuDai { get; set; }

        public double UuDaiDB { get; set; }

        public double GiaGoiDVThem { get; set; }

        public string AnhDaiDien { get; set; }

        public List<DichVuSearchingDto> DichVu { get; set; }
    }
}
