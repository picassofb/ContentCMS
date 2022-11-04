using System.Threading.Tasks;
using Abp.Application.Services;
using ContentCMS.Authorization.Accounts.Dto;

namespace ContentCMS.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
