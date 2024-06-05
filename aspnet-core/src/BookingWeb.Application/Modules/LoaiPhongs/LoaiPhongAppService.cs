﻿using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.LoaiPhongs.Dto;
using BookingWeb.Modules.Phongs.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.LoaiPhongs
{
    public class LoaiPhongAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<LoaiPhong> _loaiPhong;

        private readonly IRepository<DichVuTienIch> _dichVuTienIch;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoaiPhongAppService(IRepository<LoaiPhong> repository, IRepository<DichVuTienIch> repository1,
            IHttpContextAccessor httpContextAccessor)
        {
            _loaiPhong = repository;
            _dichVuTienIch = repository1;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> GetTenPhongById(int id)
        {
            var loaiphong = await _loaiPhong.GetAsync(id);
            return loaiphong.TenLoaiPhong;
        }
   
        public async Task<List<LoaiPhongOutputDto>> GetAllKindOfRoom()
        {
            try
            {
                var lstRoom = await _loaiPhong.GetAllListAsync();
                var lstDv = await _dichVuTienIch.GetAllListAsync();

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
                    TenDichVuTienIch = (from i in lstDv where i.LoaiPhongId == entity.Id select i.TenDichVu).ToList()
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
                    UuDai = input.UuDai
                };
                await _loaiPhong.InsertAsync(lp);

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
                var check = await _loaiPhong.FirstOrDefaultAsync(p => p.Id == input.Id);
                if (check != null)
                {
                    check.TenLoaiPhong = input.TenLoaiPhong;
                    check.MoTa = input.MoTa;
                    check.SucChua = input.SucChua;
                    check.TienNghiTrongPhong = input.TienNghiTrongPhong;
                    check.GiaPhongTheoDem = input.GiaPhongTheoDem;
                    check.GiaGoiDichVuThem = input.GiaGoiDichVuThem;
                    check.UuDai = input.UuDai;

                    await _loaiPhong.UpdateAsync(check);
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
                var checkLP = await _loaiPhong.FirstOrDefaultAsync(p => p.Id == id);

                if (checkLP != null)
                {
                    var dichVu = await _dichVuTienIch.GetAllListAsync();
                    var checkDv = dichVu.Where(p => p.LoaiPhongId == checkLP.Id).ToList();
                    if (checkDv.Any())
                    {
                        foreach(var i in checkDv)
                        {
                            await _dichVuTienIch.DeleteAsync(i);
                            await _httpContextAccessor.HttpContext.Response.WriteAsync($"da xoa dich vu: {i}");
                        }
                    }

                    await _loaiPhong.DeleteAsync(checkLP);
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


    }
}
