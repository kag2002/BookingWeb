using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using BookingWeb.MultiTenancy;

namespace BookingWeb.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
