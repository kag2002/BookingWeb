using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.DbEntities
{
    public class LoaiKhachHang : FullAuditedEntity
    {
        public string PhanLoai { get; set; }

        public double MucGiamGia { get; set; }

        public ICollection<KhachHang> KhachHangs { get; set; }

    }

}
