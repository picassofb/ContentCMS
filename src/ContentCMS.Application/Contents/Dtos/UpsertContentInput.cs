using System.ComponentModel.DataAnnotations;
using ContentCMS.Authorization;

namespace ContentCMS.Contents.Dtos
{
    public class UpsertContentInput
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(Content.MaxNameLength, ErrorMessage = "{0} cannot exceed {1} characters length")]
        public string PageName { get; set; }

        [Required]
        [StringLength(Content.MaxContentLength, ErrorMessage = "{0} cannot exceed {1} characters length")]
        public string PageContent { get; set; }
    }
}