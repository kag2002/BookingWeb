using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.DbEntities
{
    public class ChiTietDatPhong : FullAuditedEntity
    {
        public int TrangThaiPhong { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int SLNguoiLon { get; set; }

        public int SLTreEm { get; set; }

        public int SLPhong { get; set; }

        public float TienPhongQuaHan { get; set; }

        public DateTime NgayHuy { get; set; }

        public float ChiPhiHuyPhong { get; set; }

        public float TongTien { get; set; }

        public int PhongId { get; set; }

        public int PhieuDatPhongId { get; set; }

        public ICollection<NhanXetDanhGia> NhanXetDanhGias { get; set; }

    }
}
