using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ContentCMS.MultiTenancy.Dto;

namespace ContentCMS.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

