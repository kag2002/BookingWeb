using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.LoaiPhongs.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingWeb.Modules.LoaiPhongs
{
    public class LoaiPhongAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<LoaiPhong> _loaiPhongRepository;
        private readonly IRepository<PhieuDaDuyet> _phieuDaDuyetRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoaiPhongAppService(
            IRepository<LoaiPhong> loaiPhongRepository,
            IRepository<PhieuDaDuyet> phieuDaDuyetRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _loaiPhongRepository = loaiPhongRepository;
            _phieuDaDuyetRepository = phieuDaDuyetRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetTenPhongById(int id)
        {
            var loaiphong = await _loaiPhongRepository.GetAsync(id);
            return loaiphong.TenLoaiPhong;
        }

        public async Task<List<LoaiPhongOutputDto>> GetAllKindOfRoom()
        {
            try
            {
                var lstRoom = await _loaiPhongRepository.GetAllListAsync();
                var dtoLst = lstRoom.Select(entity => new LoaiPhongOutputDto
                {
                    Id = entity.Id,
                    TenLoaiPhong = entity.TenLoaiPhong,
                    SucChua = entity.SucChua,
                    MoTa = entity.MoTa,
                    TienNghiTrongPhong = entity.TienNghiTrongPhong,
                    GiaPhongTheoDem = entity.GiaPhongTheoDem,
                    GiaGoiDichVuThem = entity.GiaGoiDichVuThem,
                    UuDai = entity.UuDai,
                    TongSlPhong = entity.TongSlPhong,
                    SLPhongTrong = entity.SLPhongTrong
                }).ToList();

                return dtoLst;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AddNewKind(LoaiPhongInputDto input)
        {
            try
            {
                var lp = new LoaiPhong
                {
                    TenLoaiPhong = input.TenLoaiPhong,
                    MoTa = input.MoTaLP,
                    SucChua = input.SucChua,
                    TienNghiTrongPhong = input.TienNghiTrongPhong,
                    GiaGoiDichVuThem = input.GiaGoiDichVuThem,
                    GiaPhongTheoDem = input.GiaPhongTheoDem,
                    UuDai = input.UuDai,
                    TongSlPhong = input.TongSlPhong,
                    SLPhongTrong = input.TongSlPhong
                };
                await _loaiPhongRepository.InsertAsync(lp);

                CurrentUnitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateLP(LoaiPhongDto input)
        {
            try
            {
                var check = await _loaiPhongRepository.FirstOrDefaultAsync(p => p.Id == input.Id);
                if (check != null)
                {
                    check.TenLoaiPhong = input.TenLoaiPhong;
                    check.MoTa = input.MoTa;
                    check.SucChua = input.SucChua;
                    check.TienNghiTrongPhong = input.TienNghiTrongPhong;
                    check.GiaPhongTheoDem = input.GiaPhongTheoDem;
                    check.GiaGoiDichVuThem = input.GiaGoiDichVuThem;
                    check.UuDai = input.UuDai;

                    await _loaiPhongRepository.UpdateAsync(check);
                    return true;
                }
                else
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong ton tai loai phong voi id = {input.Id}");
                    return false;
                }

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteLP(int id)
        {
            try
            {
                var checkLP = await _loaiPhongRepository.FirstOrDefaultAsync(p => p.Id == id);

                if (checkLP != null)
                {
                    await _loaiPhongRepository.DeleteAsync(checkLP);
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"da xoa thuc the loai phong: {checkLP}");
                    return true;

                }
                else
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong tim thay loai phong voi id = {id}");
                    return false;
                }

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateAvailableRooms()
        {
            try
            {
                var loaiPhongs = await _loaiPhongRepository.GetAllListAsync();

                foreach (var loaiPhong in loaiPhongs)
                {
                  
                    var bookedRoomsCount = await _phieuDaDuyetRepository
                        .GetAll()
                        .Where(p => p.LoaiPhongId == loaiPhong.Id)
                        .SumAsync(p => p.SLPhong);

                   
                    loaiPhong.SLPhongTrong = loaiPhong.TongSlPhong - bookedRoomsCount;

                    await _loaiPhongRepository.UpdateAsync(loaiPhong);
                }

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
