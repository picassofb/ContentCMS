using System.ComponentModel.DataAnnotations;

namespace ContentCMS.Contents.Dtos
{
    public class CreateContentInput
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(Content.MaxNameLength)]
        public string PageName { get; set; }

        [Required]
        [StringLength(Content.MaxContentLength)]
        public string PageContent { get; set; }
    }
}