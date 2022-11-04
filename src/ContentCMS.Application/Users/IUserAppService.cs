using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ContentCMS.Roles.Dto;
using ContentCMS.Users.Dto;

namespace ContentCMS.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
