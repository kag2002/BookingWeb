using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.HinhThucPhongs.Dto
{
    public class GetRoomByFormDto
    {
        public int? HinhThucPhongId { get; set; }

        public string TenHinhThuc { get; set; }

        public int PhongId { get; set; }

        public string TenDonVi { get; set; }

        public List<string> LoaiPhong { get; set; }

        public string DiaChi { get; set; }

        public string AnhDaiDien { get; set; }

        public string ChinhSachVePhong { get; set; }

        public string ChinhSachVeTreEm { get; set; }

        public string ChinhSachVeThuCung { get; set; }

        public List<string> DichVu { get; set; }

        public List<string> HinhAnh { get; set; } 

    }
}
