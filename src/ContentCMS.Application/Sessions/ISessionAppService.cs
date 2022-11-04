using System.Threading.Tasks;
using Abp.Application.Services;
using ContentCMS.Sessions.Dto;

namespace ContentCMS.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
