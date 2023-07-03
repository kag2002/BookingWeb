using System.ComponentModel.DataAnnotations;

namespace BookingWeb.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}