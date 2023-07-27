using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.DichVuTienIchChungs.Dto;
using BookingWeb.Modules.DichVuTienIchs.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.DichVuTienIchChungs
{
    public class DichVuTienIchChungAppService : BookingWebAppServiceBase
    {

        private readonly IRepository<DichVuTienIchChung> _dichVuChung;

        private readonly IRepository<DonViKinhDoanh> _donViKinhDoanh;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public DichVuTienIchChungAppService(IRepository<DichVuTienIchChung> dichVuChung,
            IRepository<DonViKinhDoanh> donViKinhDoanh,
            IHttpContextAccessor httpContextAccessor)
        {
            _dichVuChung = dichVuChung;
            _donViKinhDoanh = donViKinhDoanh;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<DichVuChungOutputDto>> GetAllListDichVuChung()
        {
            try
            {
                var lstDvc = await _dichVuChung.GetAllListAsync();

                var dto = lstDvc.Select(e => new DichVuChungOutputDto
                {
                    Id = e.Id,
                    TenDichVu = e.TenDichVu,
                    ChiTiet = e.ChiTiet,
                    DonViKinhDoanhId = e.DonViKinhDoanhId

                }).ToList();

                return dto;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AddNewDvc(DichVuChungInputDto input)
        {
            try
            {
                var check = await _donViKinhDoanh.FirstOrDefaultAsync(p => p.Id == input.DonViKinhDoanhId);

                if (check != null)
                {
                    var dvc = new DichVuTienIchChung
                    {
                        TenDichVu = input.TenDichVu,
                        ChiTiet = input.ChiTiet,
                        DonViKinhDoanhId = input.DonViKinhDoanhId
                    };

                    await _dichVuChung.InsertAsync(dvc);
                    return true;
                }
                else
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong tim thay don vi kinh doanh voi id = {input.DonViKinhDoanhId}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateDvc(DichVuChungOutputDto input)
        {
            try
            {
                var check = await _dichVuChung.FirstOrDefaultAsync(p => p.Id == input.Id);

                if (check != null)
                {
                    check.TenDichVu = input.TenDichVu;
                    check.ChiTiet = input.ChiTiet;
                    check.DonViKinhDoanhId = input.DonViKinhDoanhId;

                    await _dichVuChung.UpdateAsync(check);
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

        public async Task<bool> DeleteDvc(int id)
        {
            try
            {
                var checkDv = await _dichVuChung.FirstOrDefaultAsync(p => p.Id == id);
                if (checkDv != null)
                {

                    await _dichVuChung.DeleteAsync(checkDv);
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
