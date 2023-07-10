using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.DbEntities
{
    public class PhieuDatPhong : FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayHenTra { get; set; }

        public int KhachHangId { get; set; }

        public int NhanVienId { get; set; }

        public ICollection<ChiTietDatPhong> ChiTietDatPhongs { get; set; }
    }
}