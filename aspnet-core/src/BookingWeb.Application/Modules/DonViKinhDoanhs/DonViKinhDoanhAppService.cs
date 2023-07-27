using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.DonViKinhDoanhs.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.DonViKinhDoanhs
{
    public class DonViKinhDoanhAppService : BookingWebAppServiceBase
    {

        private readonly IRepository<DiaDiem> _diaDiem;

        private readonly IRepository<DonViKinhDoanh> _donViKinhDoanh;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public DonViKinhDoanhAppService(IRepository<DiaDiem> diaDiem, IRepository<DonViKinhDoanh> donViKinhDoanh, IHttpContextAccessor httpContextAccessor)
        {
            _diaDiem = diaDiem;
            _donViKinhDoanh = donViKinhDoanh;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<DonViKinhDoanhInputDto>> GetUnitByLocationId(int id)
        {

            try
            {
                var lstDonVi = await _donViKinhDoanh.GetAllListAsync();

                var donVi = lstDonVi.Where(p=>p.DiaDiemId == id).Select(e => new DonViKinhDoanhInputDto
                {
                   DiaDiemId = e.DiaDiemId,
                   Id = e.Id,
                   TenDonVi = e.TenDonVi,
                   DiaChiChiTiet = e.DiaChiChiTiet,
                   AnhDaiDien = e.AnhDaiDien,
                   GioiThieu = e.GioiThieu

                }).ToList();

                return donVi;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"Error: {ex.Message}");
                return null;
            }

        }


        public async Task<bool> AddNewUnit(DonViKinhDoanhDto input)
        {
            try
            {
                var newUnit = new DonViKinhDoanh
                {
                    TenDonVi = input.TenDonVi,
                    DiaChiChiTiet = input.DiaChiChiTiet,
                    AnhDaiDien = input.AnhDaiDien,
                    GioiThieu = input.GioiThieu,
                    DiaDiemId = input.DiaDiemId
                };
                await _donViKinhDoanh.InsertAsync(newUnit);
                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"Error: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> UpdateUnit(DonViKinhDoanhInputDto input)
        {
            try
            {
                var donVi = await _donViKinhDoanh.FirstOrDefaultAsync(p => p.Id == input.Id);
                if(donVi == null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"Không tìm thấy don vi có id = {input.Id}");
                    return false;
                }
                donVi.TenDonVi = input.TenDonVi;
                donVi.DiaChiChiTiet = input.DiaChiChiTiet;
                donVi.AnhDaiDien = input.AnhDaiDien;
                donVi.GioiThieu = input.GioiThieu;
                donVi.DiaDiemId = input.DiaDiemId;

                await _donViKinhDoanh.UpdateAsync(donVi);
                return true;

            }
            catch(Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"Error: {ex.Message}");
                return false;
            }

        }



    }
}
