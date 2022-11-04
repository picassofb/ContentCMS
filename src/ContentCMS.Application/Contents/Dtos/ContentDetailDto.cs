using Abp.Application.Services.Dto;
using System;

namespace ContentCMS.Contents.Dtos
{
    public class ContentDetailDto
    {
        public int Id { get; set; }
        public string PageName { get; set; }
        public string PageContent { get; set; }
    }
}