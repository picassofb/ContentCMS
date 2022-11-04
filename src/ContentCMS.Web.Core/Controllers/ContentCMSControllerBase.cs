using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace ContentCMS.Controllers
{
    public abstract class ContentCMSControllerBase: AbpController
    {
        protected ContentCMSControllerBase()
        {
            LocalizationSourceName = ContentCMSConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
