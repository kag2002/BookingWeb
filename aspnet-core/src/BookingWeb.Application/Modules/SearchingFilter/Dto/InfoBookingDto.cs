﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.SearchingFilter.Dto
{
    public class InfoBookingDto
    {
        public DateTime NgayDat { get; set; }

        public DateTime NgayTra { get; set; }

        public int SlNguoiLon { get; set; }

        public int SlTreEm { get; set; }
       /* public List<> TreEm { get; set; }*/

        public int SlPhong { get; set; }
    }
}
