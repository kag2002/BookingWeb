using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.DbEntities
{
    public class LienHe : FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get ; set; }

        public string HoTen { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string NoiDung { get; set; }
    }
}
