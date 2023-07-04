using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.Phongs.Dto
{
    public class PhongOutputDto
    {
        public int Id { get; set; }

        public List<string> HinhThucKinhDoanh { get; set; }

        public List<string> DiaDiem { get; set; }

        public List<string> LoaiPhong { get; set; }

        public string Mota { get; set; }

        public string TrangThaiPhong { get; set; }

        public string DiaChiChiTiet { get; set; }

        public string TenFileAnhDaiDien { get; set; }

        public List<string> DichVuTienIch { get; set; }

        public List<string> ChinhSach { get; set; }

        public List<string> Anh { get; set; }

    }
}
