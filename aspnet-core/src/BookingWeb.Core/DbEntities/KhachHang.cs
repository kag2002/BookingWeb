using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace BookingWeb.DbEntities
{
    public class KhachHang : FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public string CCCD { get; set; }

        public string HoTen { get; set; }

        public string Email { get; set; }

        public long SoDienThoai { get; set; }

        public DateTime NgaySinh { get; set; }

        public string DiaChi { get; set; }

        public int GioiTinh { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string AnhDaiDien { get; set; }

        public int? LoaiKhachHangId { get; set; }

    }
}
