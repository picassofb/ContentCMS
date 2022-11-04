using System.ComponentModel.DataAnnotations;

namespace ContentCMS.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}