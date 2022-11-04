using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ContentCMS.Authorization;

namespace ContentCMS
{
    [DependsOn(
        typeof(ContentCMSCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class ContentCMSApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<ContentCMSAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ContentCMSApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
