using System;

namespace BookingWeb.Modules.ChiTietDatPhongs.Dto
{
    public class ChiTietDatPhongDto
    {
        public int Id { get; set; }

        public int TrangThaiPhongId { get; set; }
        public string TenPhong { get; set; }
        public string TenTrangThai { get; set; }

        public int SLNguoiLon { get; set; }

        public int SLTreEm { get; set; }

        public int SLPhong { get; set; }

        public double TienPhongQuaHan { get; set; }

        public double ChiPhiHuyPhong { get; set; }

        public DateTime? NgayHuy { get; set; }

        public double TongTien { get; set; }

        public int PhongId { get; set; }

        public int LoaiPhongId { get; set; }
        public int PhieuDatPhongId { get; set; }

        // Add new properties from PhieuDatPhong
        public string HoTen { get; set; }
        public string CCCD { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayHenTra { get; set; }
        public int DatHo { get; set; }
        public string YeuCauDacBiet { get; set; }
    }
}
