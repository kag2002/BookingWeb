using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.DichVuTienIchs.Dto
{
    public class DichVuTienIchOutputDto
    {
        public int Id { get; set; }

        public string TenDichVuTienIch { get; set; }

        public string MoTa { get; set; }

        public int LoaiPhongId { get; set; }

    }
}
