using System.Threading.Tasks;
using Abp.Application.Services;
using BookingWeb.Sessions.Dto;

namespace BookingWeb.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
