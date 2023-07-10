using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.LoaiKhachHangs.Dto;
using BookingWeb.Modules.NhanViens.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.LoaiKhachHangs
{
    public class LoaiKhachHangAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<LoaiKhachHang> _loaiKhachHang;

        private readonly IRepository<KhachHang> _khachHang;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoaiKhachHangAppService(IRepository<LoaiKhachHang> loaiKhachHang, IRepository<KhachHang> khachHang, IHttpContextAccessor httpContextAccessor)
        {
            _loaiKhachHang = loaiKhachHang;
            _khachHang = khachHang;
            _httpContextAccessor = httpContextAccessor;
        }

        /*public async Task<List<LoaiKhachHangInputDto>> GetAllList()
        {
            try
            {
                var lst = await _loaiKhachHang.GetAllListAsync();

                var dtoLst = lst.Select(entity => new LoaiKhachHangInputDto
                {
                    Id = entity.Id,
                    TenLoai = entity.TenLoai,
                    MucGiamGia = entity.MucGiamGia
                }).ToList();

                return dtoLst;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AddNewItem(LoaiKhachHangDto input)
        {
            try
            {
                var newItem = new LoaiKhachHang
                {
                    TenLoai = input.TenLoai,
                    MucGiamGia = input.MucGiamGia
                };

                await _loaiKhachHang.InsertAsync(newItem);

                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateInfoItem(LoaiKhachHangInputDto input)
        {
            try
            {
                var check = await _loaiKhachHang.FirstOrDefaultAsync(p => p.Id == input.Id);
                if (check != null)
                {
                   check.TenLoai = input.TenLoai;
                    check.MucGiamGia = input.MucGiamGia;

                    await _loaiKhachHang.UpdateAsync(check);
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

        public async Task<bool> DeleteItem(int id)
        {
            try
            {
                var check = await _loaiKhachHang.FirstOrDefaultAsync(p=>p.Id == id);
                if(check != null)
                {
                    var khachHang = await _khachHang.GetAllListAsync();
                    var checkKH = khachHang.Where(p=>p.LoaiKhachHangId == check.Id).ToList();

                    if (checkKH.Count > 0)
                    {
                        foreach(var i in checkKH)
                        {
                            i.LoaiKhachHangId = null;
                        }
                    }

                    await _loaiKhachHang.DeleteAsync(check);
                    return true;
                }
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong tim thay loai khach hang voi id = {id}");
                return false;

            }
            catch(Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }
        }*/


    }
}
