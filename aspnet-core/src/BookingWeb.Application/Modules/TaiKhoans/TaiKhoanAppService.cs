using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.TaiKhoans.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.TaiKhoans
{
    public class TaiKhoanAppService : BookingWebAppServiceBase
    {

        private readonly IRepository<KhachHang> _khachHang;

        private readonly IRepository<NhanVien> _nhanVien;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public TaiKhoanAppService(IRepository<KhachHang> khachHang, IRepository<NhanVien> nhanVien, IHttpContextAccessor httpContextAccessor)
        {
            _khachHang = khachHang;
            _nhanVien = nhanVien;
            _httpContextAccessor = httpContextAccessor;
        }

        

        /*public async Task<bool> AddNewAccount(TaiKhoanDto input)
        {
            try
            {
                var checkUsername = await _taiKhoan.FirstOrDefaultAsync(p => p.Username == input.Username);
                if(checkUsername == null )
                {
                    var account = new TaiKhoan
                    {
                        Username = input.Username,
                        Password = input.Password,
                        PhanLoai = input.PhanLoai
                    };
                    await _taiKhoan.InsertAsync(account);
                    return true;
                }
                else
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"da ton tai tai khoan {input.Username}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateAccount(TaiKhoanOuputDto input)
        {
            try
            {
                var check = await _taiKhoan.FirstOrDefaultAsync(p=>p.Id == input.Id);
                if (check != null)
                {
                    check.Username = input.Username;
                    check.Password = input.Password;
                    check.PhanLoai = input.PhanLoai;

                    await _taiKhoan.UpdateAsync(check);
                    return true;
                }
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"Khong ton tai tai khoan voi id = {input.Id}");
                return false;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAccount(int id)
        {
            try
            {
                var check =await _taiKhoan.FirstOrDefaultAsync(p=>p.Id == id);
                if (check != null)
                {
                    var checkKH = await _khachHang.FirstOrDefaultAsync(p=>p.TaiKhoanId == check.Id);
                    var checkNV = await _nhanVien.FirstOrDefaultAsync(p=>p.TaiKhoanId==check.Id);

                    if(checkKH == null && checkNV == null)
                    {
                        await _taiKhoan.DeleteAsync(check);
                    }

                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"Khong the xoa tai khoan voi id = {id}");
                    return false;
                }
                else
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"Khong ton tai tai khoan voi id = {id}");
                    return false;
                }

            }
            catch(Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> Login(LoginDto input)
        {
            try
            {
                var check = await _taiKhoan.FirstOrDefaultAsync(p=>p.Username == input.Username);

                if (check != null)
                {
                    if (check.Password == input.Password)
                    {
                        return true;
                    }
                }
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"sai tai khoan hoac mat khau");
                return false;
            }
            catch(Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }
        }
*/
        


    }
}
