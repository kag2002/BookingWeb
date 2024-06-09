using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.DatPhongs.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.DatPhongs
{
    public class DatPhongAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<PhieuDatPhong> _datPhong;

        private readonly IRepository<NhanVien> _nhanVien;

        private readonly IRepository<KhachHang> _khachHang;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public DatPhongAppService(IRepository<PhieuDatPhong> datPhong, IRepository<NhanVien> nhanVien, IRepository<KhachHang> khachHang, IHttpContextAccessor httpContextAccessor)
        {
            _datPhong = datPhong;
            _nhanVien = nhanVien;
            _khachHang = khachHang;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<PhieuDatPhongOutputDto>> GetAllList()
        {
            try
            {
                var lstDatPhong = await _datPhong.GetAllListAsync();
                var lstKH = await _khachHang.GetAllListAsync();
                var lstNV = await _nhanVien.GetAllListAsync();

                var dtoLstDP = lstDatPhong.Select(entity => new PhieuDatPhongOutputDto
                {
                    Id = entity.Id,
                    HoTen = entity.HoTen,
                    CCCD = entity.CCCD,
                    SDT = entity.SDT,
                    Email = entity.Email,
                    NgayBatDau = entity.NgayBatDau,
                    NgayHenTra = entity.NgayHenTra,
                    DatHo =     entity.DatHo,
                    YeuCauDacBiet = entity.YeuCauDacBiet,

                    /*KhachHang = lstKH.Where(p => p.Id == entity.KhachHangId).Select(p => p.HoTen).ToString(),
                    NhanVien = lstNV.Where(p => p.Id == entity.NhanVienId).Select(p => p.HoTen).ToString()*/

                }).ToList();

                return dtoLstDP;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return null;
            }
        }

        public async Task<List<PhieuDatPhongOutputDto>> GetPhieuById(int id)
        {
            try
            {
                var phieu = await _datPhong.GetAsync(id); 

                if (phieu != null)
                {
                    var dtoPhieu = new PhieuDatPhongOutputDto
                    {
                        Id = phieu.Id,
                        HoTen = phieu.HoTen,
                        CCCD = phieu.CCCD,
                        SDT = phieu.SDT,
                        Email = phieu.Email,
                        NgayBatDau = phieu.NgayBatDau,
                        NgayHenTra = phieu.NgayHenTra,
                        DatHo = phieu.DatHo,
                        YeuCauDacBiet = phieu.YeuCauDacBiet
                    };

                  
                    return new List<PhieuDatPhongOutputDto> { dtoPhieu };
                }
                else
                {
                   
                    return new List<PhieuDatPhongOutputDto>(); 
                }
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return null;
            }
        }

        public async Task<List<PhieuDatPhongOutputDto>> GetPhieuByCccd(string cccd)
        {
            try
            {
                var phieuList = await _datPhong.GetAllListAsync(p => p.CCCD == cccd);

                if (phieuList != null && phieuList.Any())
                {
                    var dtoPhieuList = phieuList.Select(phieu => new PhieuDatPhongOutputDto
                    {
                        Id = phieu.Id,
                        HoTen = phieu.HoTen,
                        CCCD = phieu.CCCD,
                        SDT = phieu.SDT,
                        Email = phieu.Email,
                        NgayBatDau = phieu.NgayBatDau,
                        NgayHenTra = phieu.NgayHenTra,
                        DatHo = phieu.DatHo,
                        YeuCauDacBiet = phieu.YeuCauDacBiet
                    }).ToList();

                    return dtoPhieuList;
                }
                else
                {
                    return new List<PhieuDatPhongOutputDto>();
                }
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return null;
            }
        }

        /*public async Task<bool> CreateNewTicket(PhieuDatPhongInputDto input)
        {
            try
            {
                var newTicket = new PhieuDatPhong
                {
                    NgayBatDau = input.NgayBatDau,
                    NgayHenTra = input.NgayHenTra,
                    KhachHangId = input.KhachHangId,
                    NhanVienId = input.NhanVienId
                };

                await _datPhong.InsertAsync(newTicket);
                return true;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }*/

        public async Task<bool> UpdateTicket(PhieuDatPhongOutputDto input)
        {
            try
            {
                var check = await _datPhong.FirstOrDefaultAsync(p => p.Id == input.Id);
                if (check != null)
                {
                    check.NgayBatDau = input.NgayBatDau;
                    check.NgayHenTra = input.NgayHenTra;

                    await _datPhong.UpdateAsync(check);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }



    }
}
