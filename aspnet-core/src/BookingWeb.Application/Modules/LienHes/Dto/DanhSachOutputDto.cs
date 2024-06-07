using System;

namespace BookingWeb.Modules.LienHes.Dto
{
    public class DanhSachOutputDto
    {
        public int Id { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NoiDung { get; set; }
        public long? UserId { get; set; }
        public string CreationDate { get; set; } 
    }
}
