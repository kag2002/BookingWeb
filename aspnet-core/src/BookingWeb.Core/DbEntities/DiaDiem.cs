using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.DbEntities
{
    public class DiaDiem : FullAuditedEntity, IMayHaveTenant
    {
        public string TenDiaDiem { get; set; }

        public string ThongTinViTri { get; set; }

        public string TenFileAnhDD { get; set; }

        public ICollection<DonViKinhDoanh> DonViKinhDoanhs { get; set; }

        public int? TenantId { get; set; }
        
    }
}
