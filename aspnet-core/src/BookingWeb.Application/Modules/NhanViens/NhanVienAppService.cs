using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.NhanViens.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace BookingWeb.Modules.NhanViens
{
    public class NhanVienAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<NhanVien> _nhanVien;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public NhanVienAppService(IRepository<NhanVien> nhanVien, IHttpContextAccessor httpContextAccessor)
        {
            _nhanVien = nhanVien;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<NhanVienOutputDto>> GetAllListNv()
        {
            try
            {
                var lstNv = await _nhanVien.GetAllListAsync();

                var dtoLst = lstNv.Select(entity => new NhanVienOutputDto
                {
                    Id = entity.Id,
                    HoTen = entity?.HoTen,
                    SoDienThoai = entity.SoDienThoai,
                    QueQuan = entity?.QueQuan,
                    Email = entity?.Email,
                    NgaySinh = entity.NgaySinh,
                    DiaChi = entity?.DiaChi,
                    GioiTinh = entity.GioiTinh,
                    AnhDaiDien = entity.AnhDaiDien,
                    UserName = entity?.UserName
                }).ToList();

                return dtoLst;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> RegisterForStaff(NhanVienDto input)
        {
            try
            {
                var checkUsername = await _nhanVien.FirstOrDefaultAsync(p => p.UserName == input.UserName);
                if (checkUsername != null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"tai khoan {input.UserName} da ton tai");
                    return false;
                }

                var checkSDt = await _nhanVien.FirstOrDefaultAsync(p => p.UserName == input.UserName);
                if (checkSDt != null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"SDT nay da duoc dang ki");
                    return false;
                }

                var checkEmail = await _nhanVien.FirstOrDefaultAsync(p => p.Email == input.Email);
                if (checkEmail != null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"Email nay da duoc dang ki");
                    return false;
                }

                var newStaff = new NhanVien
                {
                    HoTen = input.HoTen,
                    SoDienThoai = input.SoDienThoai,
                    QueQuan = input.QueQuan,
                    Email = input.Email,
                    NgaySinh = input.NgaySinh,
                    DiaChi = input.DiaChi,
                    GioiTinh = input.GioiTinh,
                    UserName = input.UserName,
                    Password = input.Password,
                };

                await _nhanVien.InsertAsync(newStaff);
                return true;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateInfoStaff(NhanVienInputDto input)
        {
            try
            {
                var check = await _nhanVien.FirstOrDefaultAsync(p => p.Id == input.Id);
                if (check != null)
                {
                    check.HoTen = input.HoTen;
                    check.SoDienThoai = input.SoDienThoai;
                    check.QueQuan = input.QueQuan;
                    check.Email = input.Email;
                    check.NgaySinh = input.NgaySinh;
                    check.DiaChi = input.DiaChi;
                    check.GioiTinh = input.GioiTinh;

                    await _nhanVien.UpdateAsync(check);
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

        public async Task<bool> ChangePasswordNV(NhanVienChangePasswordDto input)
        {
            try
            {
                var checkPass = await _nhanVien.FirstOrDefaultAsync(p => p.Id == input.Id);

                if (checkPass != null)
                {
                    if (input.CurrentPassword == checkPass.Password)
                    {
                        if (input.NewPassWord == input.ConfirmPassWord)
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
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }


        }


    }
}
