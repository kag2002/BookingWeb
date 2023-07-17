using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.DonViKinhDoanhs.Dto
{
    public class DonViKinhDoanhInputDto
    {
        public int Id { get; set; }

        public string TenDonVi { get; set; }

        public string DiaChiChiTiet { get; set; }

        public string AnhDaiDien { get; set; }

        public string ChinhSachVePhong { get; set; }

        public string ChinhSachVeTreEm { get; set; }

        public string ChinhSachVeThuCung { get; set; }

        public string GioiThieu { get; set; }

        public int? DiaDiemId { get; set; }
    }
}
