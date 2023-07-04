using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.DichVuTienIchs.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.DichVuTienIchs
{
    public class DichVuTienIchAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<DichVuTienIch> _repository;

        private readonly IRepository<LoaiPhong> _repository1;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public DichVuTienIchAppService(IRepository<DichVuTienIch> repository, IRepository<LoaiPhong> repository1, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _repository1 = repository1;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<DichVuTienIchOutputDto>> GetAllDv()
        {
            try
            {
                var lstDv = await _repository.GetAllListAsync();

                var dtoLst = lstDv.Select(entity => new  DichVuTienIchOutputDto
                {
                    Id = entity.Id,
                    TenDichVuTienIch = entity.TenDichVuTienIch,
                    MoTa = entity.MoTa,
                    LoaiPhongId = entity.LoaiPhongId,

                }).ToList();

                return dtoLst;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AddNewDv(DichVuTienIchInputDto input)
        {
            try
            {
                var check = await _repository1.FirstOrDefaultAsync(p => p.Id == input.LoaiPhongId);

                if (check != null)
                {
                    var dv = new DichVuTienIch
                    {
                        TenDichVuTienIch = input.TenDichVuTienIch,
                        MoTa = input.MoTa,
                        LoaiPhongId = input.LoaiPhongId
                    };

                    await _repository.InsertAsync(dv);
                    return true;
                }
                else
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong tim thay loai phong voi id = {input.LoaiPhongId}");
                    return false;
                }
            }
            catch(Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }
        public async Task<bool> UpdateDv(DichVuTienIchDto input)
        {
            try
            {
                var check = await _repository.FirstOrDefaultAsync(p=>p.Id == input.Id);

                if (check != null)
                {
                    check.TenDichVuTienIch = input.TenDichVuTienIch;
                    check.MoTa = input.MoTa;

                    await _repository.UpdateAsync(check);
                    return true;
                }
                else
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong tim thay dvu voi id = {input.Id}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteDv(int id)
        {
            try
            {
                var check = await _repository.FirstOrDefaultAsync(p=>p.Id == id);
                if (check != null)
                {
                    await _repository.DeleteAsync(check);
                    return true;
                }else 
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong tim thay dvu voi id = {id}");
                    return false; 
                }
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }


    }
}
