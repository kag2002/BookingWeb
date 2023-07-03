using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace BookingWeb.Controllers
{
    public abstract class BookingWebControllerBase: AbpController
    {
        protected BookingWebControllerBase()
        {
            LocalizationSourceName = BookingWebConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
