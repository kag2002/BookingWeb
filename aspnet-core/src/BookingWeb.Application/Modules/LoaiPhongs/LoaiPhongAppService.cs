﻿using Abp.Domain.Repositories;
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

        private readonly IRepository<NhanXetDanhGia> _nhanXetDanhGia;

        private readonly IRepository<Phong> _phong;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoaiPhongAppService(IRepository<LoaiPhong> repository, IRepository<DichVuTienIch> repository1, IRepository<NhanXetDanhGia> repository2,
            IRepository<Phong> repository3, IHttpContextAccessor httpContextAccessor)
        {
            _loaiPhong = repository;
            _dichVuTienIch = repository1;
            _nhanXetDanhGia = repository2;
            _phong = repository3;
            _httpContextAccessor = httpContextAccessor;
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
                    MoTaLoaiPhong = entity.MoTa,
                    SucChua = entity.SucChua,
                    TienNghiPhong = entity.TienNghiPhong,
                    GiaPhongTheoDem = entity.GiaPhongTheoDem,
                    GiaGoiDichVuThem = entity.GiaGoiDichVuThem,
                    UuDai = entity.UuDai,
                    TenDichVuTienIch = (from i in lstDv where i.LoaiPhongId == entity.Id select i.TenDichVuTienIch).ToList()
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
                    MoTa = input.MoTaLoaiPhong,
                    SucChua = input.SucChua,
                    TienNghiPhong = input.TienNghiPhong,
                    GiaGoiDichVuThem = input.GiaGoiDichVuThem,
                    GiaPhongTheoDem = input.GiaPhongTheoDem,
                    UuDai = input.UuDai

                };
                await _loaiPhong.InsertAsync(lp);
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
                var check = await _loaiPhong.FirstOrDefaultAsync(p=>p.Id == input.Id);
                if(check != null)
                {
                    check.TenLoaiPhong = input.TenLoaiPhong;
                    check.MoTa = input.MoTaLoaiPhong;
                    check.SucChua = input.SucChua;
                    check.TienNghiPhong = input.TienNghiPhong;
                    check.GiaPhongTheoDem = input .GiaPhongTheoDem;
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
            catch(Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteLP(int id)
        {
            try
            {
                var checkLP = await _loaiPhong.FirstOrDefaultAsync(p=>p.Id == id);

                if(checkLP != null)
                {
                    var checkPhong = await _phong.FirstOrDefaultAsync(p=>p.LoaiPhongId == checkLP.Id);

                    if(checkPhong == null)
                    {
                        var checkDv = await _dichVuTienIch.FirstOrDefaultAsync(p=>p.LoaiPhongId == checkLP.Id);
                        var checkNX = await _nhanXetDanhGia.FirstOrDefaultAsync(p=>p.DichVuTienIchId == checkDv.Id);

                        await _nhanXetDanhGia.DeleteAsync(checkNX);
                        await _dichVuTienIch.DeleteAsync(checkDv);
                        await _loaiPhong.DeleteAsync(checkLP);

                        return true;
                    }
                    else
                    {
                        await _httpContextAccessor.HttpContext.Response.WriteAsync("khong the xoa loai phong nay vi dang ton tai phong");
                        return false;
                    }
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