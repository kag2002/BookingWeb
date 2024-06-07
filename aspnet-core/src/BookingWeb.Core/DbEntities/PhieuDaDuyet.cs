using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace BookingWeb.DbEntities
{
    public class PhieuDaDuyet : FullAuditedEntity<int>
    {
        public int PhieuDatPhongId { get; set; }
        public int PhongId { get; set; }
        public int LoaiPhongId { get; set; }
        public int TrangThaiPhongId { get; set; }
        public string TenTrangThai { get; set; }
        public string TenPhong { get; set; }
        public int SLNguoiLon { get; set; }
        public int SLTreEm { get; set; }
        public int SLPhong { get; set; }
        public double TienPhongQuaHan { get; set; }
        public DateTime? NgayHuy { get; set; }
        public double ChiPhiHuyPhong { get; set; }
        public double TongTien { get; set; }
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
