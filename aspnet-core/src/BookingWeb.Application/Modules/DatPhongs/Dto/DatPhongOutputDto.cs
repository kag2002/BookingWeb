using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.DatPhongs.Dto
{
    public class DatPhongOutputDto
    {
        public int Id { get; set; }

        public DateTime NgayDatDuKien { get; set; }

        public DateTime NgayTraDuKien { get; set; }

    }
}
