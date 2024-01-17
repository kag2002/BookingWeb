using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.DatPhongs.Dto
{
    public class PhieuDatPhongOutputDto
    {
        public int Id { get; set; }

        public string HoTen { get; set; }

        public string CCCD { get; set; }

        public string SDT { get; set; }

        public string Email { get; set; }   
        public DateTime NgayBatDau { get; set; }

        public DateTime NgayHenTra { get; set; }

        public int DatHo { get; set; }

        public string YeuCauDacBiet { get; set; }
    }
}
