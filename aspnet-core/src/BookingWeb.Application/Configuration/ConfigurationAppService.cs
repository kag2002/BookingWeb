using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using BookingWeb.Configuration.Dto;

namespace BookingWeb.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : BookingWebAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
