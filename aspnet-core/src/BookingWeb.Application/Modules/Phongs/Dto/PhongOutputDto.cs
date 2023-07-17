using BookingWeb.Modules.SearchingFilter.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.Phongs.Dto
{
    public class PhongOutputDto
    {
        public int DiaDiemId { get; set; }

        public string DiaDiem { get; set; }

        public string ThongTinViTri { get; set; }

        public int DonViKinhDoanhId { get; set; }

        public string? TenDonVi { get; set; }

        public int PhongId { get; set; }

        public string? DiaChiChiTiet { get; set; }

        public string Mota { get; set; }

        public string TenFileAnhDaiDien { get; set; }

        public double DiemDanhGiaTB { get; set; }

        public double DanhGiaSaoTb { get; set; }

        public int HinhThucPhongId { get; set; }

        public string? HinhThucPhong { get; set; }

        public List<LoaiPhongSearchingDto>? ListLoaiPhong { get; set; }

        public List<string>? HinhAnh { get; set; }

        public string? ChinhSachVePhong { get; set; }

        public string? ChinhSachVeTreEm { get; set; }

        public string? ChinhSachVeThuCung { get; set; }

    }
}
