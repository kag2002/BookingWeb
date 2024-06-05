using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.LstTrangThaiPhong.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.LstTrangThaiPhong
{
    public class LstTrangThaiPhongAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<TrangThaiPhong> _trangThaiPhong;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LstTrangThaiPhongAppService(IRepository<TrangThaiPhong> trangThaiPhong, IHttpContextAccessor httpContextAccessor)
        {
            _trangThaiPhong = trangThaiPhong;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<TrangThaiPhongDto>> GetAllLstTrangThaiPhong()
        {
            try
            {
                var lst = await _trangThaiPhong.GetAllListAsync();
                var dto = lst.Select(e => new TrangThaiPhongDto
                {
                    Id =e.Id,
                    TemTrangThai = e.TenTrangThai
                }).ToList();


                return dto;
            }catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync(ex.Message);
                return null;
            }
        }

        public async Task<string> GetTrangThaiById(int id)
        {
            var tentrangthai = await _trangThaiPhong.GetAsync(id);
            return tentrangthai.TenTrangThai;
        }
        public async Task<bool> AddNewStatus(TrangThaiPhongInputDto input)
        {
            try
            {
                var newStatus = new TrangThaiPhong
                {
                    TenTrangThai = input.TenTrangThai
                };
                await _trangThaiPhong.InsertAsync(newStatus);
                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteStatus(int Id)
        {
            try
            {
                var check = await _trangThaiPhong.FirstOrDefaultAsync(p=>p.Id == Id);
                if (check != null)
                {
                    await _trangThaiPhong.DeleteAsync(check);
                    return true;
                }
                return false;

            }
            catch(Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync(ex.Message);
                return true;
            }
        }
    }
}
