using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BookingWeb.Authorization;

namespace BookingWeb
{
    [DependsOn(
        typeof(BookingWebCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class BookingWebApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<BookingWebAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(BookingWebApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
