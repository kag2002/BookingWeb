using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.NhanViens.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        /*public async Task<List<NhanVienOutputDto>> GetAllList()
        {
            try
            {
                var lstNv = await _nhanVien.GetAllListAsync();

                var taiKhoan = await _taiKhoan.GetAllListAsync();


                var dtoLst = lstNv.Select(entity => new NhanVienOutputDto
                {
                    HoTen = entity.HoTen,
                    SoDienThoai = entity.SoDienThoai,
                    Que = entity.Que,
                    Email = entity.Email,
                    Username = taiKhoan.Where(p=>p.Id == entity.TaiKhoanId).Select(p=>p.Username).ToString()
                }).ToList();

                return dtoLst;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return null;
            }
        }
*/
        /*public async Task<bool> RegisterForStaff(NhanVienDto input)
        {
            try
            {
                var check = await _taiKhoan.FirstOrDefaultAsync(p=>p.Username == input.Username);
                if (check == null)
                {
                    var newAccount = new TaiKhoan
                    {
                        Username = input.Username,
                        Password = input.Password,
                        PhanLoai = 10
                    };

                    await _taiKhoan.InsertAsync(newAccount);

                    var newStaff = new NhanVien
                    {
                        TaiKhoanId = newAccount.Id,
                        HoTen = input.HoTen,
                        SoDienThoai = input.SoDienThoai,
                        Que = input.Que,
                        Email = input.Email,
                        NgaySinh = input.NgaySinh
                    };

                    await _nhanVien.InsertAsync(newStaff);
                    return true;
                }
                return false;
                
            }
            catch(Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }
        }*/

        /*public async Task<bool> UpdateInfoStaff(NhanVienInputDto input)
        {
            try
            {
                var check = await _nhanVien.FirstOrDefaultAsync(p => p.Id == input.Id);
                if (check != null)
                {
                    check.HoTen = input.HoTen;
                    check.SoDienThoai = input.SoDienThoai;
                    check.Que = input.Que;
                    check.Email = input.Email;

                    await _nhanVien.UpdateAsync(check);
                    return true;
                }
                return false;
            }
            catch (Exception ex )
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }
        }*/


    }
}
