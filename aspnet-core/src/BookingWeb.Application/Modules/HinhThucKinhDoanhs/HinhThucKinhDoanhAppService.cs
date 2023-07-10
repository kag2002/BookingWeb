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
        private readonly IRepository<HinhThucPhong> _hinhThuc;
        private readonly IRepository<Phong> _phong;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HinhThucKinhDoanhAppService(IRepository<HinhThucPhong> hinhThuc, IRepository<Phong> phong, IHttpContextAccessor httpContextAccessor)
        {
            _hinhThuc = hinhThuc;
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
                var htkd = new HinhThucPhong
                {
                    TenHinhThuc = input.TenHinhThuc,
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

        /*public async Task<bool> DeleteItem(int id)
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
                    var phong = await _phong.GetAllListAsync();
                    var checkPhong = phong.Where(p => p.HinhThucKinhDoanhId == checkHt.Id).ToList();

                    if (checkPhong.Count() != 0)
                    {
                        foreach(var i in checkPhong)
                        {
                            i.HinhThucKinhDoanhId = null;
                        }
                    }

                    var chinhSach = await _chinhSach.GetAllListAsync();
                    var checkCs = chinhSach.Where(p => p.HinhThucKinhDoanhId == checkHt.Id).ToList();
                    if(checkCs.Count() != 0)
                    {
                        foreach(var i in checkCs)
                        {
                            await _chinhSach.DeleteAsync(i);
                            await _httpContextAccessor.HttpContext.Response.WriteAsync($"da xoa chinh sanh {i}");
                        }
                    }

                    await _hinhThuc.DeleteAsync(checkHt);
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"da xoa hinh thuc kinh doanh {checkHt}");
                    return true;
                }

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }
        }*/

    }
}
