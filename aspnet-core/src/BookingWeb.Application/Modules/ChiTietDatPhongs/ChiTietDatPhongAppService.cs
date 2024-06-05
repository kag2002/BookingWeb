using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.ChiTietDatPhongs.Dto;
using BookingWeb.Modules.LoaiPhongs;
using BookingWeb.Modules.LstTrangThaiPhong;
using BookingWeb.Modules.Phongs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.ChiTietDatPhongs
{
    public class ChiTietDatPhongAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<ChiTietDatPhong> _chiTietDatPhong;
        private readonly IRepository<PhieuDatPhong> _phieuDatPhong;
        private readonly LstTrangThaiPhongAppService _lstTrangThaiPhongAppService;
        private readonly LoaiPhongAppService _loaiPhongAppService;
        private readonly PhongAppService _phongAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        

        public ChiTietDatPhongAppService(
            IRepository<ChiTietDatPhong> chiTietDatPhong,
            IRepository<PhieuDatPhong> phieuDatPhong,
            LstTrangThaiPhongAppService lstTrangThaiPhongAppService,
            LoaiPhongAppService loaiPhongAppService,
            PhongAppService phongAppService, 
            IHttpContextAccessor httpContextAccessor)
        {
            _chiTietDatPhong = chiTietDatPhong;
            _phieuDatPhong = phieuDatPhong;
            _lstTrangThaiPhongAppService = lstTrangThaiPhongAppService;
            _loaiPhongAppService = loaiPhongAppService;
            _phongAppService = phongAppService; 

            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<ChiTietDatPhongDto> GetChiTietDatPhongByPhieuDatPhongId(int phieuDatPhongId)
        {
            try
            {
                var chiTietDatPhong = await _chiTietDatPhong.FirstOrDefaultAsync(e => e.PhieuDatPhongId == phieuDatPhongId);
                if (chiTietDatPhong == null)
                {
                    throw new Exception("Record not found");
                }

                var phieuDatPhong = await _phieuDatPhong.FirstOrDefaultAsync(chiTietDatPhong.PhieuDatPhongId);
                var tenTrangThai = await _lstTrangThaiPhongAppService.GetTrangThaiById(chiTietDatPhong.TrangThaiPhongId);
                var tenLoaiPhong = await _loaiPhongAppService.GetTenPhongById(chiTietDatPhong.LoaiPhongId);
               

                var chiTietDatPhongDto = new ChiTietDatPhongDto
                {
                    Id = chiTietDatPhong.Id,
                    PhongId = chiTietDatPhong.PhongId,
                    PhieuDatPhongId = chiTietDatPhong.PhieuDatPhongId,
                    LoaiPhongId = chiTietDatPhong.LoaiPhongId,
                    TrangThaiPhongId = chiTietDatPhong.TrangThaiPhongId,
                    TenTrangThai = tenTrangThai,
                    TenPhong = tenLoaiPhong,
                    SLNguoiLon = chiTietDatPhong.SLNguoiLon,
                    SLTreEm = chiTietDatPhong.SLTreEm,
                    SLPhong = chiTietDatPhong.SLPhong,
                    TienPhongQuaHan = chiTietDatPhong.TienPhongQuaHan,
                    NgayHuy = chiTietDatPhong.NgayHuy,
                    ChiPhiHuyPhong = chiTietDatPhong.ChiPhiHuyPhong,
                    TongTien = chiTietDatPhong.TongTien,

                    // PhieuDatPhong data
                    HoTen = phieuDatPhong?.HoTen,
                    CCCD = phieuDatPhong?.CCCD,
                    SDT = phieuDatPhong?.SDT,
                    Email = phieuDatPhong?.Email,
                    NgayBatDau = phieuDatPhong?.NgayBatDau ?? DateTime.MinValue,
                    NgayHenTra = phieuDatPhong?.NgayHenTra ?? DateTime.MinValue,
                    DatHo = phieuDatPhong?.DatHo ?? 0,
                    YeuCauDacBiet = phieuDatPhong?.YeuCauDacBiet
                };

                return chiTietDatPhongDto;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return null;
            }
        }
        public async Task<bool> AcceptBooking(int phieuDatPhongId)
        {
            try
            {
                var chiTietDatPhong = await _chiTietDatPhong.FirstOrDefaultAsync(e => e.PhieuDatPhongId == phieuDatPhongId);
                if (chiTietDatPhong == null)
                {
                    throw new Exception("Record not found");
                }

                // Update the necessary fields
                chiTietDatPhong.TrangThaiPhongId = 5; // Set the new status ID
                chiTietDatPhong.SLPhong -= 1;

                await _chiTietDatPhong.UpdateAsync(chiTietDatPhong);
                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }
        public async Task<bool> DenyBooking(int phieuDatPhongId)
        {
            try
            {
                var chiTietDatPhong = await _chiTietDatPhong.FirstOrDefaultAsync(e => e.PhieuDatPhongId == phieuDatPhongId);
                if (chiTietDatPhong == null)
                {
                    throw new Exception("Record not found");
                }

               
                chiTietDatPhong.TrangThaiPhongId = 4; // Set the new status ID
               

                await _chiTietDatPhong.UpdateAsync(chiTietDatPhong);
                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }

    }
}
