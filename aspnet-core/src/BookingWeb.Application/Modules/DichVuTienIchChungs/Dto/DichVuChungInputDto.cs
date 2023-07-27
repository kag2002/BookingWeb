using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.DichVuTienIchChungs.Dto
{
    public class DichVuChungInputDto
    {
        public string TenDichVu { get; set; }

        public string ChiTiet { get; set; }

        public int? DonViKinhDoanhId { get; set; }

    }
}
