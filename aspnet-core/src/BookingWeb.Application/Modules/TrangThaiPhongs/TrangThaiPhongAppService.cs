using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.TaiKhoans.Dto;
using BookingWeb.Modules.TrangThaiPhongs.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.TrangThaiPhongs
{
    public class TrangThaiPhongAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<TrangThaiPhong> _trangThaiPhong;

        private readonly IRepository<Phong> _phong;

        private readonly IRepository<ChiTietDatPhong> _chiTietDatPhong;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public TrangThaiPhongAppService(IRepository<TrangThaiPhong> trangThaiPhong, IRepository<Phong> phong, IRepository<ChiTietDatPhong> chiTietDatPhong, IHttpContextAccessor httpContextAccessor)
        {
            _trangThaiPhong = trangThaiPhong;
            _phong = phong;
            _chiTietDatPhong = chiTietDatPhong;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<TrangThaiPhongDto>> GetAllList()
        {
            try
            {
                var lstTrangThai = await _trangThaiPhong.GetAllListAsync();

                var dtoLst = lstTrangThai.Select(entity => new TrangThaiPhongDto
                {
                    Id = entity.Id,
                    TrangThai = entity.TrangThai
                }).ToList();

                return dtoLst;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AddNewStatus(TrangThaiInputDto input)
        {
            try
            {
                var newSatus = new TrangThaiPhong
                {
                    TrangThai = input.TrangThai
                };

                await _trangThaiPhong.InsertAsync(newSatus);
                return true;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateStatus(TrangThaiPhongDto input)
        {
            try
            {
                var check = await _trangThaiPhong.FirstOrDefaultAsync(p => p.Id == input.Id);

                if (check != null)
                {
                    check.TrangThai = input.TrangThai;

                    await _trangThaiPhong.UpdateAsync(check);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }
        }
    }
}

/*        public async Task<bool> DeleteStatus(int id)
        {
            try
            {
                var check = await _trangThaiPhong.FirstOrDefaultAsync(p => p.Id == id);
                if (check != null)
                {
                    var phong = await _phong.GetAllListAsync();
                    var checkP = phong.Where(p=>p.TrangThaiPhongId == check.Id).ToList();

                    var chitiet = await _chiTietDatPhong.GetAllListAsync();
                    var checkCt = chitiet.Where(p=>p.TrangThaiPhongId == check.Id).ToList();

                    if(checkCt.Any() && checkP.Any())
                    {
                        await _trangThaiPhong.DeleteAsync(check);
                        return true;
                    }
                    else
                    {
                        foreach(var i in checkP)
                        {
                            i.TrangThaiPhongId = null;
                        }
                        foreach(var j in checkCt)
                        {
                            j.TrangThaiPhongId = null;
                        }

                        await _trangThaiPhong.DeleteAsync(check);
                        return true;
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }

        }
    }
}
*/