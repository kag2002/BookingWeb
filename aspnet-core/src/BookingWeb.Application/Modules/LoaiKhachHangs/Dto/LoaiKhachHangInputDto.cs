using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.LoaiKhachHangs.Dto
{
    public class LoaiKhachHangInputDto
    {
        public int Id { get; set; }

        public string TenLoai { get; set; }

        public float MucGiamGia { get; set; }
    }
}
