using Abp.Application.Services;
using BookingWeb.MultiTenancy.Dto;

namespace BookingWeb.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

