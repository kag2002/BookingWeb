using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.ChinhSachQuyDinhs.Dto.Custom
{
    public class ChinhSachQuyDinhFullDto
    {
        public int Id { get; set; }

        public string QuyDinhVeThuCung { get; set; }

        public string QuyDinhVeTreEm { get; set; }

        public string QuyDinhVeDatPhong { get; set; }

        public int HinhThucKinhDoanhId { get; set; }

    }
}
