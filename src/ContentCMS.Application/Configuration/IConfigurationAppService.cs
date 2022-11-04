using System.Threading.Tasks;
using ContentCMS.Configuration.Dto;

namespace ContentCMS.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
