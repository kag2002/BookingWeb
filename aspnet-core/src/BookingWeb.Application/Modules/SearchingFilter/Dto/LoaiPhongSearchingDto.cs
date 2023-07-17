﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.SearchingFilter.Dto
{

    public class LoaiPhongSearchingDto
    {
        public int LoaiPhongId { get; set; }

        public string LoaiPhong { get; set; }

        public int TongSLPhong { get; set;  }

        public string TrangThaiPhong { get; set; }

        public bool MienPhiHuyPhong { get; set; }

        public double GiaPhongTheoDem { get; set; }

        public double GiaGoiDVThem { get; set; }

        public List<DichVuSearchingDto> DichVu { get; set; }
    }
}