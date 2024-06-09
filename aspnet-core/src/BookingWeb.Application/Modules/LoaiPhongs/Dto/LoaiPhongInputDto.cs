namespace BookingWeb.Modules.LoaiPhongs.Dto
{
    public class LoaiPhongInputDto
    {
        public string TenLoaiPhong { get; set; }
        public int SucChua { get; set; }
        public string MoTaLP { get; set; }
        public string TienNghiTrongPhong { get; set; }
        public float GiaPhongTheoDem { get; set; }
        public float GiaGoiDichVuThem { get; set; }
        public float UuDai { get; set; }
        public int TongSlPhong { get; set; } 
    }
}
