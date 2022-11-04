using AutoMapper;
using ContentCMS.Authorization.Roles;
using ContentCMS.Roles.Dto;

namespace ContentCMS.Contents.Dtos
{
    public class ContentMapProfile : Profile
    {
        public ContentMapProfile()
        {
            CreateMap<Content, ContentDetailDto>();
            CreateMap<UpsertContentInput, Content>();
        }
    }
}