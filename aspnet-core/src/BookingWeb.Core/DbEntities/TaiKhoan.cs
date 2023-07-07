﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookingWeb.DbEntities
{
    public class TaiKhoan : FullAuditedEntity, IMayHaveTenant
    {
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Username must contain only letters and numbers")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [RegularExpression("^[a-z0-9]+$", ErrorMessage = "Password must contain only lowercase letters and numbers")]
        public string Password { get; set; }

        public int PhanLoai { get; set; }

        public ICollection<NhanVien> NhanViens { get; set; }

        public ICollection<KhachHang> KhachHangs { get; set; }

        public int? TenantId { get; set; }
    }
}
