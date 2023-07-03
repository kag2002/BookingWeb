using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.DbEntities
{
    public class LoaiPhong : FullAuditedEntity, IMayHaveTenant
    {
        public string TenLoaiPhong { get; set; }

        public string MoTa { get; set; }

        public int SucChua { get; set; }

        public string TienNghiPhong { get; set; }

        public float GiaPhongTheoDem { get; set; }

        public float GiaGoiDichVuThem { get; set; }

        public float UuDai { get; set; }

        public ICollection<Phong> Phongs { get; set; }

        public ICollection<DichVuTienIch> DichVuTienIches { get; set; }
        
        public int? TenantId { get; set; }
    }
}
