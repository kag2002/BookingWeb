using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace BookingWeb.DbEntities
{
    public class HinhThucKinhDoanh : FullAuditedEntity,  IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public string TenHinhThuc { get; set; }

        public string TenDonViKinhDoanh { get; set; }

        public string DiaChiChiTiet { get; set; }

        public ICollection<ChinhSachQuyDinh> ChinhSachQuyDinhs { get; set; }

        public ICollection<Phong> Phongs { get; set; }
    }
}
