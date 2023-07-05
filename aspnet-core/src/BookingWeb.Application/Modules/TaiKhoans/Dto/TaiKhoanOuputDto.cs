using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.TaiKhoans.Dto
{
    public class TaiKhoanOuputDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int PhanLoai { get; set; }
    }
}
