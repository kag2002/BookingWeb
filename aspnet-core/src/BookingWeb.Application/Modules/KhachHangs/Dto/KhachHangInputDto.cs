
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.KhachHangs.Dto
{
    public class KhachHangInputDto
    {
        public string CCCD { get; set; }

        public string HoTen { get; set; }

        public string Email { get; set; }

        public long SoDienThoai { get; set; }

        public DateTime NgaySinh { get; set; }

        public string DiaChi { get; set; }

        public int GioiTinh { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

    }
}
