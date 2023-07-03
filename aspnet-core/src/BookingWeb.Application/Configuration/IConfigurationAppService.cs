using System.Threading.Tasks;
using BookingWeb.Configuration.Dto;

namespace BookingWeb.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
