using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.ChiTietDatPhongs.Dto;
using BookingWeb.Modules.LoaiPhongs;
using BookingWeb.Modules.LstTrangThaiPhong;
using BookingWeb.Modules.Phongs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingWeb.Modules.ChiTietDatPhongs
{
    public class ChiTietDatPhongAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<ChiTietDatPhong> _chiTietDatPhongRepository;
        private readonly IRepository<PhieuDatPhong> _phieuDatPhongRepository;
        private readonly IRepository<PhieuDaDuyet> _phieuDaDuyetRepository;
        private readonly LstTrangThaiPhongAppService _lstTrangThaiPhongAppService;
        private readonly LoaiPhongAppService _loaiPhongAppService;
        private readonly PhongAppService _phongAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChiTietDatPhongAppService(
            IRepository<ChiTietDatPhong> chiTietDatPhongRepository,
            IRepository<PhieuDatPhong> phieuDatPhongRepository,
            IRepository<PhieuDaDuyet> phieuDaDuyetRepository,
            LstTrangThaiPhongAppService lstTrangThaiPhongAppService,
            LoaiPhongAppService loaiPhongAppService,
            PhongAppService phongAppService,
            IHttpContextAccessor httpContextAccessor)
        {
            _chiTietDatPhongRepository = chiTietDatPhongRepository;
            _phieuDatPhongRepository = phieuDatPhongRepository;
            _phieuDaDuyetRepository = phieuDaDuyetRepository;
            _lstTrangThaiPhongAppService = lstTrangThaiPhongAppService;
            _loaiPhongAppService = loaiPhongAppService;
            _phongAppService = phongAppService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ChiTietDatPhongDto> GetChiTietDatPhongByPhieuDatPhongId(int phieuDatPhongId)
        {
            try
            {
                var chiTietDatPhong = await _chiTietDatPhongRepository.FirstOrDefaultAsync(e => e.PhieuDatPhongId == phieuDatPhongId);
                if (chiTietDatPhong == null)
                {
                    throw new Exception("Record not found");
                }

                var phieuDatPhong = await _phieuDatPhongRepository.FirstOrDefaultAsync(chiTietDatPhong.PhieuDatPhongId);
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


        public async Task<List<PhieuDaDuyetDto>> GetPhieuDaDuyetByCccd(string cccd)
        {
            try
            {
                var phieuDaDuyetList = await _phieuDaDuyetRepository.GetAllListAsync(e => e.CCCD == cccd);
                if (phieuDaDuyetList == null || !phieuDaDuyetList.Any())
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync("Records not found");
                    return null;
                }

                return phieuDaDuyetList.Select(phieuDaDuyet => new PhieuDaDuyetDto
                {
                    Id = phieuDaDuyet.Id,
                    PhieuDatPhongId = phieuDaDuyet.PhieuDatPhongId,
                    PhongId = phieuDaDuyet.PhongId,
                    LoaiPhongId = phieuDaDuyet.LoaiPhongId,
                    TrangThaiPhongId = phieuDaDuyet.TrangThaiPhongId,
                    TenTrangThai = phieuDaDuyet.TenTrangThai,
                    TenPhong = phieuDaDuyet.TenPhong,
                    SLNguoiLon = phieuDaDuyet.SLNguoiLon,
                    SLTreEm = phieuDaDuyet.SLTreEm,
                    SLPhong = phieuDaDuyet.SLPhong,
                    TienPhongQuaHan = phieuDaDuyet.TienPhongQuaHan,
                    NgayHuy = phieuDaDuyet.NgayHuy,
                    ChiPhiHuyPhong = phieuDaDuyet.ChiPhiHuyPhong,
                    TongTien = phieuDaDuyet.TongTien,
                    HoTen = phieuDaDuyet.HoTen,
                    CCCD = phieuDaDuyet.CCCD,
                    SDT = phieuDaDuyet.SDT,
                    Email = phieuDaDuyet.Email,
                    NgayBatDau = phieuDaDuyet.NgayBatDau,
                    NgayHenTra = phieuDaDuyet.NgayHenTra,
                    DatHo = phieuDaDuyet.DatHo,
                    YeuCauDacBiet = phieuDaDuyet.YeuCauDacBiet
                }).ToList();
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"Error in GetPhieuDaDuyetByCccd: {ex.Message}");
                return null;
            }
        }


        public async Task<bool> AcceptBooking(int phieuDatPhongId)
        {
            try
            {
                var chiTietDatPhong = await _chiTietDatPhongRepository.FirstOrDefaultAsync(e => e.PhieuDatPhongId == phieuDatPhongId);
                if (chiTietDatPhong == null)
                {
                    throw new Exception("Record not found");
                }

                chiTietDatPhong.TrangThaiPhongId = 5;
               

               
                await _chiTietDatPhongRepository.UpdateAsync(chiTietDatPhong);

               
                var storeResult = await StoreBookingInPhieuDaDuyet(phieuDatPhongId);
                if (!storeResult)
                {
                    throw new Exception("Failed to store booking in PhieuDaDuyet");
                }

                await DeleteBooking(phieuDatPhongId);

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
            
                var chiTietDatPhong = await _chiTietDatPhongRepository.FirstOrDefaultAsync(e => e.PhieuDatPhongId == phieuDatPhongId);
                if (chiTietDatPhong == null)
                {
                    throw new Exception("Record not found in ChiTietDatPhong");
                }

               
                chiTietDatPhong.TrangThaiPhongId = 4;
                await _chiTietDatPhongRepository.UpdateAsync(chiTietDatPhong);

                
                var deleteResult = await DeleteBooking(phieuDatPhongId);
                if (!deleteResult)
                {
                    throw new Exception("Failed to delete booking from PhieuDatPhong");
                }

                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"Error in DenyBooking: {ex.Message}");
                return false;
            }
        }


        private async Task<bool> DeleteBooking(int phieuDatPhongId)
        {
            try
            {
               
                var chiTietDatPhong = await _chiTietDatPhongRepository.FirstOrDefaultAsync(e => e.PhieuDatPhongId == phieuDatPhongId);
                if (chiTietDatPhong != null)
                {
                    await _chiTietDatPhongRepository.DeleteAsync(chiTietDatPhong);
                }

            
                var phieuDatPhong = await _phieuDatPhongRepository.FirstOrDefaultAsync(phieuDatPhongId);
                if (phieuDatPhong != null)
                {
                    await _phieuDatPhongRepository.DeleteAsync(phieuDatPhong);
                }

                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"Error in DeleteBooking: {ex.Message}");
                return false;
            }
        }


        private async Task<bool> StoreBookingInPhieuDaDuyet(int phieuDatPhongId)
        {
            try
            {
                var chiTietDatPhong = await _chiTietDatPhongRepository.FirstOrDefaultAsync(e => e.PhieuDatPhongId == phieuDatPhongId);
                if (chiTietDatPhong == null)
                {
                    throw new Exception("ChiTietDatPhong record not found");
                }

                var phieuDatPhong = await _phieuDatPhongRepository.FirstOrDefaultAsync(chiTietDatPhong.PhieuDatPhongId);
                if (phieuDatPhong == null)
                {
                    throw new Exception("PhieuDatPhong record not found");
                }

                var phieuDaDuyet = new PhieuDaDuyet
                {
                    PhieuDatPhongId = phieuDatPhong.Id,
                    HoTen = phieuDatPhong.HoTen,
                    CCCD = phieuDatPhong.CCCD,
                    SDT = phieuDatPhong.SDT,
                    Email = phieuDatPhong.Email,
                    NgayBatDau = phieuDatPhong.NgayBatDau,
                    NgayHenTra = phieuDatPhong.NgayHenTra,
                    DatHo = phieuDatPhong.DatHo,
                    YeuCauDacBiet = phieuDatPhong.YeuCauDacBiet,
                    TrangThaiPhongId = chiTietDatPhong.TrangThaiPhongId,
                    LoaiPhongId = chiTietDatPhong.LoaiPhongId,
                    SLNguoiLon = chiTietDatPhong.SLNguoiLon,
                    SLTreEm = chiTietDatPhong.SLTreEm,
                    SLPhong = chiTietDatPhong.SLPhong,
                    TienPhongQuaHan = chiTietDatPhong.TienPhongQuaHan,
                    NgayHuy = chiTietDatPhong.NgayHuy,
                    ChiPhiHuyPhong = chiTietDatPhong.ChiPhiHuyPhong,
                    TongTien = chiTietDatPhong.TongTien
                };

                await _phieuDaDuyetRepository.InsertAsync(phieuDaDuyet);

                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }


        public async Task<List<PhieuDaDuyetDto>> GetAllPhieuDaDuyet()
        {
            try
            {
                var phieuDaDuyetList = await _phieuDaDuyetRepository.GetAllListAsync();

                if (phieuDaDuyetList == null || !phieuDaDuyetList.Any())
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync("No records found in PhieuDaDuyet");
                    return new List<PhieuDaDuyetDto>();
                }

                return phieuDaDuyetList.Select(phieuDaDuyet => new PhieuDaDuyetDto
                {
                    Id = phieuDaDuyet.Id,
                    PhieuDatPhongId = phieuDaDuyet.PhieuDatPhongId,
                    PhongId = phieuDaDuyet.PhongId,
                    LoaiPhongId = phieuDaDuyet.LoaiPhongId,
                    TrangThaiPhongId = phieuDaDuyet.TrangThaiPhongId,
                    TenTrangThai = phieuDaDuyet.TenTrangThai,
                    TenPhong = phieuDaDuyet.TenPhong,
                    SLNguoiLon = phieuDaDuyet.SLNguoiLon,
                    SLTreEm = phieuDaDuyet.SLTreEm,
                    SLPhong = phieuDaDuyet.SLPhong,
                    TienPhongQuaHan = phieuDaDuyet.TienPhongQuaHan,
                    NgayHuy = phieuDaDuyet.NgayHuy,
                    ChiPhiHuyPhong = phieuDaDuyet.ChiPhiHuyPhong,
                    TongTien = phieuDaDuyet.TongTien,
                    HoTen = phieuDaDuyet.HoTen,
                    CCCD = phieuDaDuyet.CCCD,
                    SDT = phieuDaDuyet.SDT,
                    Email = phieuDaDuyet.Email,
                    NgayBatDau = phieuDaDuyet.NgayBatDau,
                    NgayHenTra = phieuDaDuyet.NgayHenTra,
                    DatHo = phieuDaDuyet.DatHo,
                    YeuCauDacBiet = phieuDaDuyet.YeuCauDacBiet
                }).ToList();
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"Error in GetAllPhieuDaDuyet: {ex.Message}");
                return null;
            }
        }



        public async Task<bool> DeleteBookingFromPhieuDaDuyet(int phieuDatPhongId)
        {
            try
            {
                var phieuDaDuyet = await _phieuDaDuyetRepository.FirstOrDefaultAsync(e => e.PhieuDatPhongId == phieuDatPhongId);
                if (phieuDaDuyet != null)
                {
                    await _phieuDaDuyetRepository.DeleteAsync(phieuDaDuyet);
                }

                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }

        public async Task<bool> FinishBooking(int phieuDatPhongId)
        {
            try
            {
             
                var result = await DeleteBookingFromPhieuDaDuyet(phieuDatPhongId);

           
                if (result)
                {
                   
                    return true;
                }

               
                throw new Exception("Failed to delete booking from PhieuDaDuyet");
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"Error in FinishBooking: {ex.Message}");
                return false;
            }
        }

    }
}
