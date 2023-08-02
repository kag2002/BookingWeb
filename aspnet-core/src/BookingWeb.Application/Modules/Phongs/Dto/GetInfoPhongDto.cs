using Abp.Application.Services.Dto;
using BookingWeb.Modules.ChinhSachChungs.Dto;
using BookingWeb.Modules.DichVuTienIchChungs.Dto;
using System.Collections.Generic;

namespace BookingWeb.Modules.Phongs.Dto
{
    public class GetInfoPhongDto
    {
        public int DiaDiemId { get; set; }

        public string TenDiaDiem { get; set; }

        public string ThongTinViTri { get; set; }


        public int DonViKinhDoanhId { get; set; }

        public string TenDonVi { get; set; }

        public string DiaChiChiTiet { get; set; }


        public int HinhThucPhongId { get; set; }

        public string HinhThucPhong { get; set; }


        public int PhongId { get; set; }

        public string Mota { get; set; }

        public string TenFileAnhDaiDien { get; set; }


        public int LuoDatPhong { get; set; }

        public double DiemDanhGiaTB { get; set; }

        public double DanhGiaSaoTb { get; set; }


        public List<LoaiPhongSearchingDto> ListLoaiPhong { get; set; }

        public List<ChinhSachChungDto> ChinhSachChung { get; set; }

        public List<DichVuChungDto> DichVuChung { get; set; }

        public List<string> HinhAnh { get; set; }


    }
}
