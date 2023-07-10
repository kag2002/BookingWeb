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

        public int TrangThaiPhong { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int SLNguoiLon { get; set; }

        public int SLTreEm { get; set; }

        public int SLPhong { get; set; }

        public float TienPhongQuaHan { get; set; }

        public float ChiPhiHuyPhong { get; set; }

        public DateTime NgayHuy { get; set; }

        public float TongTien { get; set; }

        public int PhongId { get; set; }

        public int PhieuDatPhongId { get; set; }


    }
}
