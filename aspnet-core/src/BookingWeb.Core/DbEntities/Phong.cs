using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.DbEntities
{
    public class Phong : FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public string Mota { get; set; }

        public string TrangThaiPhong { get; set; }

        public string DiaChiChiTiet { get; set; }

        public string TenFileAnhDaiDien { get; set; }

        public int? DiaDiemId { get; set; }

        public int? LoaiPhongId { get; set; }

        public int? HinhThucKinhDoanhId { get; set; }
        
        public ICollection<HinhAnh> HinhAnhs { get; set; }

        public ICollection<ChiTietDatPhong> ChiTietDatPhongs { get; set; }

    }
}
