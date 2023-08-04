using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.SearchingFilter.Dto
{
    public class SearchingFilterRoomInputDto
    {
/*        public int pageIndex { get; set; }

        public int pageSize { get; set; }
*/

        public bool MienPhiHuyPhong { get; set; }


        public double GiaPhongNhoNhat { get; set; }
        
        public double GiaPhongLonNhat { get; set; }

        public List<double> DanhGiaSao { get; set; }

        public List<int> HinhThucPhongId { get; set; }


        public int SortCondition { get; set; }

    }
}
