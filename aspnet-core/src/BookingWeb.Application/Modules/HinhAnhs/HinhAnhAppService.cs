using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.HinhAnhs.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.HinhAnhs
{
    public class HinhAnhAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<HinhAnh> _hinhAnh;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HinhAnhAppService(IRepository<HinhAnh> hinhAnh, IHttpContextAccessor httpContextAccessor)
        {
            _hinhAnh = hinhAnh;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<HinhAnhInputDto>> GetImageByRoom(int id)
        {
            try
            {
                var lstHa = await _hinhAnh.GetAllListAsync();

                var lst = lstHa.Where(p=>p.PhongId == id).ToList();

                var dtoLst = lst.Select(entity => new HinhAnhInputDto
                {
                    ID = entity.Id,
                    TenFileAnh = entity.TenFileAnh,
                    PhongId = entity.PhongId
                }).ToList();
                return dtoLst;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return null;
            }
        }


        public async Task<List<HinhAnhInputDto>> GetALlListImage()
        {
            try
            {
                var lstAnh = await _hinhAnh.GetAllListAsync();
                var dtoLst = lstAnh.Select(entity => new HinhAnhInputDto
                {
                    TenFileAnh = entity.TenFileAnh,
                    PhongId = entity.PhongId
                }).ToList();
                return dtoLst;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return null;
            }

        }

        public async Task<bool> AddNewImage(HinhAnhDto input)
        {
            try
            {
                var ha = new HinhAnh
                {
                    TenFileAnh = input.TenFileAnh,
                    PhongId = input.PhongId
                };

                await _hinhAnh.InsertAsync(ha);
                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateImage(HinhAnhInputDto input)
        {
            try
            {
                var item = await _hinhAnh.FirstOrDefaultAsync(p => p.Id == input.ID);
                if (item == null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong tim thay id: {input.ID}");
                    return false;
                }

                item.TenFileAnh = input.TenFileAnh;

                await _hinhAnh.UpdateAsync(item);
                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteImage(int id)
        {
            try
            {
                var checkHa = await _hinhAnh.FirstOrDefaultAsync(p => p.Id == id);
                if (checkHa == null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong tim thay hinh anh voi id = {id}");
                    return false;
                }
                await _hinhAnh.DeleteAsync(checkHa);
                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }
        }
    }
}
