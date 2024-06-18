using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.Phongs.Dto
{
    public class ConfirmBookRoomResultDto
    {
        public bool Success { get; set; }
        public int? IdPhieuDatPhong { get; set; }
        public string ErrorMessage { get; set; }
        public string DiaChiChiTiet { get; set; }
    }
}

