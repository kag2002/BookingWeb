using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.DbEntities
{
    public class TrangThaiPhong : FullAuditedEntity, IMayHaveTenant
    {
        public string TenTrangThai { get; set; }
        
        public int? TenantId { get ; set; }
    }
}
