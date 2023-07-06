using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;

namespace BookingWeb.DbEntities
{
    public class TrangThaiPhong : FullAuditedEntity
    {
        public string TrangThai { get; set; }

        public ICollection<Phong> Phongs { get; set; }

        public ICollection<ChiTietDatPhong> ChiTietDatPhongs { get; set; }

    }
}
