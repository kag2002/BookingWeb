using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace BookingWeb.DbEntities
{
    public class Phong : FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public string? Mota { get; set; }

        public string TenFileAnhDaiDien { get; set; }

        public int LuotDatPhong { get; set; }

        public double DiemDanhGiaTB { get; set; }

        public double DanhGiaSaoTb { get; set; }

        public int? DonViKinhDoanhId { get; set; }

        public int? HinhThucPhongId { get; set; }

        public ICollection<HinhAnh> HinhAnhs { get; set; }

        public ICollection<ChiTietDatPhong> ChiTietDatPhongs { get; set; }

    }
}
