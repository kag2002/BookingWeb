using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.DbEntities
{
    public class DichVuTienIch : FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public string TenDichVu { get; set; }

        public string MoTa { get; set; }

        public int LoaiPhongId { get; set; }
    }
}
