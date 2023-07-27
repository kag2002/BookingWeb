using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.DbEntities
{
    public class LoaiKhachHang : FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public string PhanLoai { get; set; }

        public double MucGiamGia { get; set; }

        public ICollection<KhachHang> KhachHangs { get; set; }
    }

}
