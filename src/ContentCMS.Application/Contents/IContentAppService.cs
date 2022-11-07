using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ContentCMS.Contents.Dtos;
using System.Threading.Tasks;

namespace ContentCMS.Contents
{
    public interface IContentAppService : IApplicationService
    {
        Task<ContentDetailDto> GetCMSContent(int id);
        Task<ListResultDto<ContentDetailDto>> GetAll();
        Task<ContentDetailDto> InsertOrUpdateCMSContent(UpsertContentInput input);
    }
}

