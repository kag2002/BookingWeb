using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.SearchingFilter.Dto
{
    public class LoaiPhongSearchingFilterDto
    {
        public int LoaiPhongId { get; set; }

        public bool MienPhiHuyPhong { get; set; }

        public double GiaPhongTheoDem { get; set; }

        public double UuDai { get; set; }

        public string AnhDaiDien { get; set; }

    }
}
