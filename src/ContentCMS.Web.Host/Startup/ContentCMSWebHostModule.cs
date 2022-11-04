using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ContentCMS.Configuration;

namespace ContentCMS.Web.Host.Startup
{
    [DependsOn(
       typeof(ContentCMSWebCoreModule))]
    public class ContentCMSWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ContentCMSWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(ContentCMSWebHostModule).GetAssembly());
        }
    }
}
