using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.ChiTietDatPhongs.Dto;
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

        private readonly IRepository<TrangThaiPhong> _trangThaiPhong;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChiTietDatPhongAppService(IRepository<TrangThaiPhong> trangThaiPhong,IRepository<ChiTietDatPhong> chiTietDatPhong, IHttpContextAccessor httpContextAccessor)
        {
            _trangThaiPhong = trangThaiPhong;
            _chiTietDatPhong = chiTietDatPhong;
            _httpContextAccessor = httpContextAccessor;
        }

        /*public async Task<List<ChiTietDatPhongDto>> GetAllList()
        {
            try
            {
                var lstChiTiet = await _chiTietDatPhong.GetAllListAsync();
                var checkTt = await _trangThaiPhong.GetAllListAsync();
                var dtoList = lstChiTiet.Select(e => new ChiTietDatPhongDto
                {
                    Id = e.Id,
                    PhongId = e.PhongId,
                    DatPhongId = e.DatPhongId,
                    TrangThaiPhong = checkTt.Where(p=>p.Id == e.TrangThaiPhongId).Select(p=>p.TrangThai).ToString(),
                    CheckIn = e.CheckIn,
                    CheckOut = e.CheckOut,



                }).ToList();

            }
            catch(Exception ex)
            {

            }
        }
*/






    }
}
