using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.DbEntities
{
    public class DonViKinhDoanh : FullAuditedEntity, IMayHaveTenant
    {
        public string TenDonVi { get; set; }

        public string DiaChiChiTiet { get; set; }

        public string AnhDaiDien { get; set; }

        public string GioiThieu { get; set; }

        public int? DiaDiemId { get; set; }

        public ICollection<Phong> Phongs { get; set; }

        public ICollection<LoaiPhong> LoaiPhongs { get; set; }

        public ICollection<ChinhSachChung> ChinhSachChungs { get; set; }

        public ICollection<DichVuTienIchChung> DichVuTienIchChungs { get; set; }

        public int? TenantId { get; set; }
    }
}
