using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.SearchingFilter.Dto
{
    public class SearchingFilterRoomOutputDto
    {
        public int Id { get; set; }

        public string TenDonVi { get; set; }

        public string HinhThucPhong { get; set; }

        public string LoaiPhong { get; set; }

        public string DiaChiChiTiet { get; set; }

        public float DanhGiaSao { get; set; }

        public float DiemDanhGia { get; set; }

        public int SLDanhGia { get; set; }

        public float GiaPhongTheoDem { get; set; }

        public List<string> ChinhSachveDatPhong { get; set; }
    }
}
