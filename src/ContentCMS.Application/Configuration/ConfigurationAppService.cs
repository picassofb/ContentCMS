using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using ContentCMS.Configuration.Dto;

namespace ContentCMS.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ContentCMSAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
