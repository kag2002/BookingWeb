using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.DbEntities
{
    public class HinhAnh : FullAuditedEntity, IMayHaveTenant
    {
        public string TenFileAnh { get; set; }

        public int PhongId { get; set; }

        public int? TenantId { get; set; }
    }
}
