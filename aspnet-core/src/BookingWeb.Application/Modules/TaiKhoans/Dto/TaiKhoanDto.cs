using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookingWeb.Modules.TaiKhoans.Dto
{
    public class TaiKhoanDto
    {
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Username must contain only letters and numbers")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [RegularExpression("^[a-z0-9]+$", ErrorMessage = "Password must contain only lowercase letters and numbers")]
        public string Password { get; set; }


        public int PhanLoai { get; set; }
    }
}
