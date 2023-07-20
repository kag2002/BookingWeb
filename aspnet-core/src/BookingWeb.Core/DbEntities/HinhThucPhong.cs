using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;


namespace BookingWeb.DbEntities
{
    public class HinhThucPhong : FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public string TenHinhThuc { get; set; }

        public string AnhDaiDien { get; set; }

        public ICollection<Phong> Phongs { get; set; }
    }
}
