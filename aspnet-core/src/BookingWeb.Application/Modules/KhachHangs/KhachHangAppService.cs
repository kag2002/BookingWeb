using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.KhachHangs.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingWeb.Modules.KhachHangs
{
    public class KhachHangAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<KhachHang> _khachHang;

        private readonly IRepository<LoaiKhachHang> _loaiKhachHang;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public KhachHangAppService(IRepository<KhachHang> khachHang, IRepository<LoaiKhachHang> loaiKhachHang, IHttpContextAccessor httpContextAccessor)
        {
            _khachHang = khachHang;
            _loaiKhachHang = loaiKhachHang;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<KhachHangOutputDto>> GetAllListClient()
        {
            try
            {
                var lstKh = await _khachHang.GetAllListAsync();

                var loaiKhachHang = await _loaiKhachHang.GetAllListAsync();

                var dtoLst = lstKh.Select(entity => new KhachHangOutputDto
                {
                    Id = entity.Id,
                    CCCD = entity.CCCD,
                    HoTen = entity.HoTen,
                    SoDienThoai = entity.SoDienThoai,
                    Email = entity.Email,
                    NgaySinh = entity.NgaySinh,
                    DiaChi = entity.DiaChi,
                    GioiTinh = entity.GioiTinh,
                    LoaiKhachHang = loaiKhachHang.FirstOrDefault(p => p.Id == entity.LoaiKhachHangId).PhanLoai,
                    UserName = entity.UserName
                }).ToList();

                return dtoLst;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> RegisterForClient(KhachHangInputDto input)
        {
            try
            {
                var checkUser = await _khachHang.FirstOrDefaultAsync(p => p.UserName == input.UserName);
                if (checkUser != null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"tai khoan {input.UserName} da ton tai");
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

                var checkSDt = await _khachHang.FirstOrDefaultAsync(p => p.SoDienThoai == input.SoDienThoai);
                if (checkSDt != null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"SDT nay da duoc dang ki");
                    return false;
                }

                var newClient = new KhachHang
                {
                    UserName = input.UserName,
                    Password = input.Password,
                    CCCD = input.CCCD,
                    HoTen = input.HoTen,
                    SoDienThoai = input.SoDienThoai,
                    Email = input.Email,
                    NgaySinh = input.NgaySinh,
                    GioiTinh = input.GioiTinh,
                    DiaChi = input.DiaChi,
                    LoaiKhachHangId = 1
                };

                await _khachHang.InsertAsync(newClient);
                //CurrentUnitOfWork.SaveChanges();
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
                    check.DiaChi = input.DiaChi;
                    check.GioiTinh = input.GioiTinh;
                    check.NgaySinh = input.NgaySinh;

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


        public async Task<bool> ChangePasswordKH(KhachHangChangePasswordDto input)
        {
            try
            {
                var checkPass = await _khachHang.FirstOrDefaultAsync(p=>p.Id == input.Id);

                if (checkPass != null)
                {
                    if(input.CurrentPassword == checkPass.Password)
                    {
                        if(input.NewPassWord == input.ConfirmPassWord)
                        {
                            checkPass.Password = input.NewPassWord;
                            return true;
                        }
                        else
                        {
                            await _httpContextAccessor.HttpContext.Response.WriteAsync("Xac nhan mat khau moi that bai!");
                            return false;
                        }
                    }
                    else
                    {
                        await _httpContextAccessor.HttpContext.Response.WriteAsync("Mat khau cu khong chinh xac!");
                        return false;
                    }
                }
                return false;
            }
            catch(Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }


        }

    }
}
