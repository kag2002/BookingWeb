using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.LienHes.Dto
{
    public class DanhSachOutputDto
    {
        public int Id { get; set; }

        public string HoTen { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string NoiDung { get; set; }

        public long? userId { get; set; }
    }
}
