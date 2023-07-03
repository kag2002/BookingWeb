using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.DiaDiems.Dto;
using BookingWeb.Modules.Phongs.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.Phongs
{
    public class PhongAppservice : BookingWebAppServiceBase
    {
        private readonly IRepository<Phong> _repository;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public PhongAppservice(IRepository<Phong> repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<PhongDto>> GetAllRoom()
        {
            try
            {
                var lst = await _repository.GetAllListAsync();

                var dtoLst = lst.Select(entity => new PhongDto
                {
                    
                }).ToList();

                return dtoLst;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return null;
            }
        }


    }
}
