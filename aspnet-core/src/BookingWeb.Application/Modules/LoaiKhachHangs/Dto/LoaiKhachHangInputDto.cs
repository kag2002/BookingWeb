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

        public string PhanLoai { get; set; }

        public double MucGiamGia { get; set; }
    }
}
