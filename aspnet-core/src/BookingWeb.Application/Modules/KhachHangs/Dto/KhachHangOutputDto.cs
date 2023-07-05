using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.KhachHangs.Dto
{
    public class KhachHangOutputDto
    {
        public int Id { get; set; }

        public long CCCD { get; set; }

        public string HoTen { get; set; }

        public long SoDienThoai { get; set; }

        public string Email { get; set; }

        public string LoaiKhachHang { get; set; }

        public string Username { get; set; }
    }
}
