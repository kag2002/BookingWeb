using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.SearchingFilter.Dto
{
    public class SearchingFilterRoomInputDto
    {
        public int DiaDiemid { get; set; }

/*        public int pageIndex { get; set; }
        
        public int pageSize { get; set; }*/

        public string HinhThucPhong { get; set; }

        public int MienPhiHuyPhong { get; set; }

        public float GiaPhong { get; set; }

        public float DanhGiaSao { get; set; }

    }
}
