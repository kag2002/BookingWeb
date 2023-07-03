using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BookingWeb.Configuration;

namespace BookingWeb.Web.Host.Startup
{
    [DependsOn(
       typeof(BookingWebWebCoreModule))]
    public class BookingWebWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public BookingWebWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BookingWebWebHostModule).GetAssembly());
        }
    }
}
