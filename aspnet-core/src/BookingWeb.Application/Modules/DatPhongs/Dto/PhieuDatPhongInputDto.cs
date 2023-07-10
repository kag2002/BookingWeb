using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.DatPhongs.Dto
{
    public class PhieuDatPhongInputDto
    {
        public DateTime NgayBatDau { get; set; }

        public DateTime NgayHenTra { get; set; }

        public int KhachHangId { get; set; }

        public int NhanVienId { get; set; }

    }
}
