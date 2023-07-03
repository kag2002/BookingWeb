using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using BookingWeb.EntityFrameworkCore;
using BookingWeb.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace BookingWeb.Web.Tests
{
    [DependsOn(
        typeof(BookingWebWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class BookingWebWebTestModule : AbpModule
    {
        public BookingWebWebTestModule(BookingWebEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BookingWebWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(BookingWebWebMvcModule).Assembly);
        }
    }
}