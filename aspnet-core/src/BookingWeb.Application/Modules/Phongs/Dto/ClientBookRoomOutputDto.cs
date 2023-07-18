using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.Phongs.Dto
{
    public class ClientBookRoomOutputDto
    {
        public int phongId { get; set; }

        public int donViKinhDoanhId { get; set; }

        public string tenDonVi { get; set; }

        public string nenDiaDiem { get; set; }

        public DateTime ngayNhanPhong { get; set; }

        public DateTime ngayTraPhong { get; set; }

        public int loaiPhongId { get; set; }

        public int sucChuaPhong { get; set; }

        public string moTaPhong { get; set; }

        public string tienNghi { get; set; }

    }
}
