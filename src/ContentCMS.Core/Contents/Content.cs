using System;
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

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        [Required]
        [StringLength(MaxNameLength)]
        public string PageName { get; protected set; }

        [Required]
        [StringLength(MaxContentLength)]
        public string PageContent { get; protected set; }
        protected Content()
        {
            
        }

        public static Content Create(string pageName, string pageContent)
        {
            var content = new Content { 
                Id = 0,
                PageName = pageName,
                PageContent = pageContent
            };

            return content;
        }

        public static Content Update(int id, string pageName, string pageContent)
        {
            var content = new Content
            {
                Id = id,
                PageName = pageName,
                PageContent = pageContent
            };

            return content;
        }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}