using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.DbEntities
{
    public class LoaiPhong : FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        public string TenLoaiPhong { get; set; }

        public int TongSlPhong { get; set; }

        public int SucChua { get; set; }

        public int SLPhongTrong { get; set; }

        public string MoTa { get; set; }

        public string AnhDaiDien { get; set; }

        public string TienNghiTrongPhong { get; set; }

        public double GiaPhongTheoDem { get; set; }

        public double GiaGoiDichVuThem { get; set; }

        public bool MienPhiHuyPhong { get; set; }

        public double ChiPhiHuyPhong { get; set; }

        public double UuDai { get; set; }

        public double UuDaiDacBiet { get; set; }

        public int DonViKinhDoanhId { get; set; }

        public ICollection<DichVuTienIch> DichVuTienIches { get; set; }
        
    }
}
