using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Module.HinhThucKinhDoanhs.Dto;
using BookingWeb.Modules.HinhThucKinhDoanhs.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingWeb.Module.HinhThucKinhDoanhs
{
    public class HinhThucKinhDoanhAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<HinhThucKinhDoanh> _repository;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HinhThucKinhDoanhAppService(IRepository<HinhThucKinhDoanh> repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<HinhThucKinhDoanhAddDto>> GetAllList()
        {
            try
            {
                var lst = await _repository.GetAllListAsync();

                var dtoList = lst.Select(entity => new HinhThucKinhDoanhAddDto
                {
                    TenHinhThuc = entity.TenHinhThuc,
                    TenDonViKinhDoanh = entity.TenDonViKinhDoanh,
                    DiaChiChiTiet = entity.DiaChiChiTiet
                }).ToList();

                return dtoList;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"erorr : {ex.Message}");
                return null;
            }

        }


        public async Task<bool> AddNewItem(HinhThucKinhDoanhAddDto input)
        {
            try
            {
                var csqd = new HinhThucKinhDoanh
                {
                    TenHinhThuc = input.TenHinhThuc,
                    TenDonViKinhDoanh = input.TenDonViKinhDoanh,
                    DiaChiChiTiet = input.DiaChiChiTiet
                };

                await _repository.InsertAsync(csqd);

                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateItem(HinhThucKinhDoanhDto input)
        {
            try
            {
                var item = await _repository.FirstOrDefaultAsync(p => p.Id == input.Id);
                if (item == null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync("Product not found");
                    return false;
                }

                item.TenHinhThuc = input.TenHinhThuc;
                item.TenDonViKinhDoanh = input.TenDonViKinhDoanh;
                item.DiaChiChiTiet = input.DiaChiChiTiet;

                await _repository.UpdateAsync(item);
                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"Internal server error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteItem(int id)
        {
            try
            {
                var product = await _repository.FirstOrDefaultAsync(p => p.Id == id);
                if (product == null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync("Product not found");
                    return false;
                }

                await _repository.DeleteAsync(product);
                await _httpContextAccessor.HttpContext.Response.WriteAsync("Successfully Deleted !");
                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"Internal server error: {ex.Message}");
                return false;
            }
        }

    }
}
