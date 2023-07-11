using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.Phongs.Dto
{
    public class PhongByIdDto
    {
        public int Id { get; set; }

        public string TenDonVi { get; set; }

        public string DiaChiChiTiet { get; set; }

        public string Mota { get; set; }

        public string TenFileAnhDaiDien { get; set; }

        public int TrangThaiPhong { get; set; }

        public float DiemDanhGiaTB { get; set; }

        public float DanhGiaSaoTb { get; set; }

        public string DiaDiem { get; set; }

        public string LoaiPhong { get; set; }

        public string HinhThucPhong { get; set; }

        public List<string> DichVu { get; set; }

        public List<string> HinhAnh { get; set; }

        public string ChinhSachVePhong { get; set; }

        public string ChinhSachVeTreEm { get; set; }

        public string ChinhSachVeThuCung { get; set; }

    }
}
