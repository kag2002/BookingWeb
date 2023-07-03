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
        private readonly IRepository<LoaiPhong> _repository;

        private readonly IRepository<DichVuTienIch> _repository1;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoaiPhongAppService(IRepository<LoaiPhong> repository, IRepository<DichVuTienIch> repository1, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _repository1 = repository1;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<LoaiPhongOutputDto>> GetAllKindOfRoom()
        {
            try
            {
                var lstRoom = await _repository.GetAllListAsync();
                var lstDv = await _repository1.GetAllListAsync();
                var dtoLst = lstRoom.Select(entity => new LoaiPhongOutputDto
                {
                    TenLoaiPhong = entity.TenLoaiPhong,
                    MoTaLoaiPhong = entity.MoTa,
                    SucChua = entity.SucChua,
                    TienNghiPhong = entity.TienNghiPhong,
                    GiaPhongTheoDem = entity.GiaPhongTheoDem,
                    GiaGoiDichVuThem = entity.GiaGoiDichVuThem,
                    UuDai = entity.UuDai,
                    TenDichVuTienIch = (from i in lstDv where i.Id == entity.Id select i.TenDichVuTienIch).ToString(),
                    MotaDichVU = (from i in lstDv where i.Id == entity.Id select i.MoTa).ToString()
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

                var dv = new DichVuTienIch
                {
                    TenDichVuTienIch = input.TenDichVuTienIch,
                    MoTa = input.MoTaDv,
                    LoaiPhongId = lp.Id
                };

                await _repository.InsertAsync(lp);

                await _repository1.InsertAsync(dv);

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
