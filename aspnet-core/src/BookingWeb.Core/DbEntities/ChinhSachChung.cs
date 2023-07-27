using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.DbEntities
{
    public class ChinhSachChung : FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public string KiemTraThongTin { get; set; }

        public string BuaSang { get; set; }

        public string NhanPhong { get; set; }

        public string TraPhong { get; set; }

        public string ChinhSachVePhong { get; set; }

        public string ChinhSachTreEm { get; set; }

        public string ChinhSachVeGiuongPhu { get; set; }

        public string ChinhSachVeThuCung { get; set; }

        public string PhuongThucThanhToan { get; set; }

        public int? DonViKinhDoanhId { get; set; }

    }
}
