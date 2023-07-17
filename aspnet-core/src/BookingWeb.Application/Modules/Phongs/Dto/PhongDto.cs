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

        public string TenFileAnhDaiDien { get; set; }

        public int? DonViKinhDoanhId { get; set; }

        public int? HinhThucPhongId { get; set; }

/*        public double DiemDanhGiaTb { get; set; }

        public double DanhGiaSaoTb { get; set; }*/
    }
}
