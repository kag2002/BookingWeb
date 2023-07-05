using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.KhachHangs.Dto
{
    public class KhachHangDto
    {
        public int Id { get; set; }

        public long CCCD { get; set; }

        public string HoTen { get; set; }

        public long SoDienThoai { get; set; }

        public string Email { get; set; }

        public int? LoaiKhachHangId { get; set; }

    }
}
