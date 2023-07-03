using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.DbEntities
{
    public class TaiKhoan : FullAuditedEntity, IMayHaveTenant
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public int PhanLoai { get; set; }

        public ICollection<NhanVien> NhanViens { get; set; }

        public ICollection<KhachHang> KhachHangs { get; set; }

        public int? TenantId { get; set; }
    }
}
