using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.LoaiKhachHangs.Dto
{
    public class GetClientByTypeDto
    {
        public int? LoaiKhachHangId { get; set; }

        public string PhanLoai { get; set; }

        public int KhachHangId { get; set; }

        public string CCCD { get; set; }

        public string HoTen { get; set; }

        public string Email { get; set; }

        public long SoDienThoai { get; set; }

        public DateTime NgaySinh { get; set; }

        public string DiaChi { get; set; }

        public int GioiTinh { get; set; }

        public string UserName { get; set; }

    }
}
