using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.ChiTietDatPhongs.Dto
{
    public class ChiTietDatPhongDto
    {
        public int Id { get; set; }

        public string TrangThaiPhong { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int SLNguoiLon { get; set; }

        public int SLTreEm { get; set; }

        public int SLPhong { get; set; }

        public double TienPhongQuaHan { get; set; }

        public double ChiPhiHuyPhong { get; set; }

        public DateTime NgayHuy { get; set; }

        public double TongTien { get; set; }

        public int PhongId { get; set; }

        public int PhieuDatPhongId { get; set; }


    }
}
