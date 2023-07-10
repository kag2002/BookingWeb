using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.NhanViens.Dto
{
    public class NhanVienChangePasswordDto
    {
        public int Id { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassWord { get; set; }

        public string ConfirmPassWord { get; set; }
    }
}
