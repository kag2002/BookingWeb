using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace BookingWeb.DbEntities
{
    public class ChinhSachQuyDinh : FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public string QuyDinhVeThuCung { get; set; }

        public string QuyDinhVeTreEm { get; set; }

        public string QuyDinhVeDatPhong { get; set; }

        public int HinhThucKinhDoanhId { get; set; }
    }
}
