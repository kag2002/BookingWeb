﻿using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.DiaDiems.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.DiaDiems
{
    public class DiaDiemAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<DiaDiem> _diaDiem;

        private readonly IRepository<Phong> _phong;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public DiaDiemAppService(IRepository<DiaDiem> diaDiem, IRepository<Phong> phong, IHttpContextAccessor httpContextAccessor)
        {
            _diaDiem = diaDiem;
            _phong = phong;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<DiaDiemFullDto>> GetAllLocations()
        {
            try
            {
               var lst = await _diaDiem.GetAllListAsync();

                var dtoLst = lst.Select(entity => new DiaDiemFullDto
                {
                    Id = entity.Id,
                    TenDiaDiem = entity.TenDiaDiem,
                    ThongTinViTriDiaLy = entity.ThongTinViTriDiaLy,
                    DiaDangXungQuanh = entity.DiaDangXungQuanh,
                    MoTa = entity.MoTa,
                    TenFileAnhDD = entity.TenFileAnhDD
                }).ToList();

                return dtoLst;

            }
            catch(Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AddNewLocation(DiaDiemDto input)
        {
            try
            {
                var item = new DiaDiem
                {
                    TenDiaDiem = input.TenDiaDiem,
                    ThongTinViTriDiaLy = input.ThongTinViTriDiaLy,
                    DiaDangXungQuanh = input.DiaDangXungQuanh,
                    MoTa = input.MoTa,
                    TenFileAnhDD = input.TenFileAnhDD
                };

                await _diaDiem.InsertAsync(item);
                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateLocation(DiaDiemFullDto input)
        {
            try
            {
                var item = await _diaDiem.FirstOrDefaultAsync(p=>p.Id == input.Id);

                if(item == null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong ton tai dia danh voi id = {input.Id}");
                    return false;
                }
                else
                {
                    item.TenDiaDiem = input.TenDiaDiem;
                    item.ThongTinViTriDiaLy = input.ThongTinViTriDiaLy;
                    item.DiaDangXungQuanh = input.DiaDangXungQuanh;
                    item.MoTa = input.MoTa;
                    item.TenFileAnhDD = input.TenFileAnhDD;

                    await _diaDiem.UpdateAsync(item);
                    return true;
                }
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteLocation(int id)
        {
            try
            {
                var checkDD = await _diaDiem.FirstOrDefaultAsync(p=>p.Id==id);
                if (checkDD == null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong co dia diem voi id = {id}");
                    return false;
                }
                else
                {
                    var checkPhong = await _phong.FirstOrDefaultAsync(p=>p.DiaDiemId == checkDD.Id  );
                    if (checkPhong == null)
                    {
                        await _diaDiem.DeleteAsync(checkDD);
                        return true;
                    }
                    else
                    {
                        await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong the xoa dia diem nay");
                        return false;
                    }
                }

                
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }


        }


    }
}