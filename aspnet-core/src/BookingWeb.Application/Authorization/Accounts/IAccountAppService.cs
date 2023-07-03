using System.Threading.Tasks;
using Abp.Application.Services;
using BookingWeb.Authorization.Accounts.Dto;

namespace BookingWeb.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
