using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.KhachHangs.Dto;
using BookingWeb.Modules.NhanViens.Dto;
using BookingWeb.Modules.TaiKhoans.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.KhachHangs
{
    public class KhachHangAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<KhachHang> _khachHang;

        private readonly IRepository<TaiKhoan> _taiKhoan;

        private readonly IRepository<LoaiKhachHang> _loaiKhachHang;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public KhachHangAppService(IRepository<KhachHang> khachHang, IRepository<TaiKhoan> taiKhoan, IRepository<LoaiKhachHang> loaiKhachHang, IHttpContextAccessor httpContextAccessor)
        {
            _khachHang = khachHang;
            _taiKhoan = taiKhoan;
            _loaiKhachHang = loaiKhachHang;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<KhachHangOutputDto>> GetAllListClient()
        {
            try
            {
                var lstKh = await _khachHang.GetAllListAsync();

                var taiKhoan = await _taiKhoan.GetAllListAsync();

                var loaiKhachHang = await _loaiKhachHang.GetAllListAsync();

                var dtoLst = lstKh.Select(entity => new KhachHangOutputDto
                {
                    CCCD = entity.CCCD,
                    HoTen = entity.HoTen,
                    SoDienThoai = entity.SoDienThoai,
                    Email = entity.Email,
                    LoaiKhachHang = loaiKhachHang.Where(p=>p.Id == entity.LoaiKhachHangId).Select(p=>p.TenLoai).ToString(),
                    Username = taiKhoan.Where(p => p.Id == entity.TaiKhoanId).Select(p => p.Username).ToString()
                }).ToList();

                return dtoLst;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> RegisterForClient(RegisterDto input)
        {
            try
            {
                var checkUser = await _taiKhoan.FirstOrDefaultAsync(p => p.Username == input.Username);
                if (checkUser != null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"tai khoan {input.Username} da ton tai");
                    return false;
                }

                var checkCccd = await _khachHang.FirstOrDefaultAsync(p => p.CCCD == input.CCCD);


                if (checkCccd != null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"so cccd nay da duoc dang ki");
                    return false;
                }

                var checkEmail = await _khachHang.FirstOrDefaultAsync(p => p.Email == input.Email);

                if (checkEmail != null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"Email nay da duoc dang ki");
                    return false;
                }

                var newAccount = new TaiKhoan
                {
                    Username = input.Username,
                    Password = input.Password,
                    PhanLoai = 1
                };

                await _taiKhoan.InsertAsync(newAccount);

                var newClient = new KhachHang
                {
                    TaiKhoanId = newAccount.Id,
                    CCCD = input.CCCD,
                    HoTen = input.HoTen,
                    SoDienThoai = input.SoDienThoai,
                    Email = input.Email,
                    NgaySinh = input.NgaySinh,
                    LoaiKhachHangId = 1
                };

                await _khachHang.InsertAsync(newClient);
                return true;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateInfoClient(KhachHangDto input)
        {
            try
            {
                var check = await _khachHang.FirstOrDefaultAsync(p => p.Id == input.Id);
                if (check != null)
                {
                    check.CCCD = input.CCCD;
                    check.HoTen = input.HoTen;
                    check.SoDienThoai= input.SoDienThoai;
                    check.Email = input.Email;
                    check.LoaiKhachHangId = input.LoaiKhachHangId;

                    await _khachHang.UpdateAsync(check);
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
