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
        private readonly IRepository<HinhThucKinhDoanh> _hinhThuc;
        private readonly IRepository<ChinhSachQuyDinh> _chinhSach;
        private readonly IRepository<Phong> _phong;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HinhThucKinhDoanhAppService(IRepository<HinhThucKinhDoanh> hinhThuc, IRepository<ChinhSachQuyDinh> chinhSach, IRepository<Phong> phong, IHttpContextAccessor httpContextAccessor)
        {
            _hinhThuc = hinhThuc;
            _chinhSach = chinhSach;
            _phong = phong;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<HinhThucKinhDoanhFullDto>> GetAllList()
        {
            try
            {
                var lst = await _hinhThuc.GetAllListAsync();

                var dtoList = lst.Select(entity => new HinhThucKinhDoanhFullDto
                {
                    Id = entity.Id,
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


        public async Task<bool> AddNewItem(HinhThucKinhDoanhDto input)
        {
            try
            {
                var htkd = new HinhThucKinhDoanh
                {
                    TenHinhThuc = input.TenHinhThuc,
                    TenDonViKinhDoanh = input.TenDonViKinhDoanh,
                    DiaChiChiTiet = input.DiaChiChiTiet
                };

                await _hinhThuc.InsertAsync(htkd);

                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateItem(HinhThucKinhDoanhFullDto input)
        {
            try
            {
                var item = await _hinhThuc.FirstOrDefaultAsync(p => p.Id == input.Id);
                if (item == null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong tim thay loai phong voi id = {input.Id}");
                    return false;
                }

                item.TenHinhThuc = input.TenHinhThuc;
                item.TenDonViKinhDoanh = input.TenDonViKinhDoanh;
                item.DiaChiChiTiet = input.DiaChiChiTiet;

                await _hinhThuc.UpdateAsync(item);
                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteItem(int id)
        {
            try
            {
                var checkHt = await _hinhThuc.FirstOrDefaultAsync(p => p.Id == id);
                if (checkHt == null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong tim thay loai phong voi id = {id}");
                    return false;
                }
                else
                {
                    var checkPhong = await _phong.FirstOrDefaultAsync(p=>p.HinhThucKinhDoanhId == checkHt.Id);

                    var checkCs = await _chinhSach.FirstOrDefaultAsync(p=>p.HinhThucKinhDoanhId ==  checkHt.Id);

                    if (checkPhong != null)
                    {
                        checkPhong.HinhThucKinhDoanhId = null;
                    }

                    await _chinhSach.DeleteAsync(checkCs);
                    await _hinhThuc.DeleteAsync(checkHt);
                    return true;

                }

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }
        }

    }
}
