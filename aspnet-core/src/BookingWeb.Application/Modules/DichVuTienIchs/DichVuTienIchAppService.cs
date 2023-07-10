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
        private readonly IRepository<DichVuTienIch> _dichVuTienIch;

        private readonly IRepository<LoaiPhong> _loaiPhong;

        private readonly IRepository<NhanXetDanhGia> _nhanXet;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public DichVuTienIchAppService(IRepository<DichVuTienIch> dichVuTienIch, IRepository<LoaiPhong> loaiPhong, IRepository<NhanXetDanhGia> nhanXet, IHttpContextAccessor httpContextAccessor)
        {
            _dichVuTienIch = dichVuTienIch;
            _loaiPhong = loaiPhong;
            _nhanXet = nhanXet;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<DichVuTienIchOutputDto>> GetAllDv()
        {
            try
            {
                var lstDv = await _dichVuTienIch.GetAllListAsync();

                var dtoLst = lstDv.Select(entity => new DichVuTienIchOutputDto
                {
                    Id = entity.Id,
                    TenDichVu = entity.TenDichVu,
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
                var check = await _loaiPhong.FirstOrDefaultAsync(p => p.Id == input.LoaiPhongId);

                if (check != null)
                {
                    var dv = new DichVuTienIch
                    {
                        TenDichVu = input.TenDichVu,
                        MoTa = input.MoTa,
                        LoaiPhongId = input.LoaiPhongId
                    };

                    await _dichVuTienIch.InsertAsync(dv);
                    return true;
                }
                else
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong tim thay loai phong voi id = {input.LoaiPhongId}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }
        public async Task<bool> UpdateDv(DichVuTienIchDto input)
        {
            try
            {
                var check = await _dichVuTienIch.FirstOrDefaultAsync(p => p.Id == input.Id);

                if (check != null)
                {
                    check.TenDichVu = input.TenDichVu;
                    check.MoTa = input.MoTa;

                    await _dichVuTienIch.UpdateAsync(check);
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
                var checkDv = await _dichVuTienIch.FirstOrDefaultAsync(p => p.Id == id);
                if (checkDv != null)
                {
                    
                    await _dichVuTienIch.DeleteAsync(checkDv);
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"da xoa dich vu {checkDv}");
                    return true;

                }
                else
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
