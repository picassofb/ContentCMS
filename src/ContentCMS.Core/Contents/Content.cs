using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace ContentCMS.Contents
{
    [Table("AppContent")]
    public class Content : AuditedAggregateRoot
    {
        public const int MaxNameLength = 128;
        public const int MaxContentLength = 2048;

        [Required]
        [StringLength(MaxNameLength)]
        public string PageName { get; protected set; }

        [Required]
        [StringLength(MaxContentLength)]
        public string PageContent { get; protected set; }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}