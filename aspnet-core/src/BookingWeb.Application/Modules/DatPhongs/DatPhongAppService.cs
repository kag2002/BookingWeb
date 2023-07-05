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
        private readonly IRepository<DatPhong> _datPhong;

        private readonly IRepository<NhanVien> _nhanVien;

        private readonly IRepository<KhachHang> _khachHang;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public DatPhongAppService(IRepository<DatPhong> datPhong, IRepository<NhanVien> nhanVien, IRepository<KhachHang> khachHang, IHttpContextAccessor httpContextAccessor)
        {
            _datPhong = datPhong;
            _nhanVien = nhanVien;
            _khachHang = khachHang;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<DatPhongDto>> GetAllList()
        {
            try
            {
                var lstDatPhong = await _datPhong.GetAllListAsync();
                var lstKH = await _khachHang.GetAllListAsync();
                var lstNV = await _nhanVien.GetAllListAsync();

                var dtoLstDP = lstDatPhong.Select(entity => new DatPhongDto
                {
                    Id = entity.Id,
                    NgayDatDuKien = entity.NgayDatDuKien,
                    NgayTraDuKien = entity.NgayTraDuKien,
                    KhachHang = lstKH.Where(p=>p.Id == entity.KhachHangId).Select(p=>p.HoTen).ToString(),
                    NhanVien = lstNV.Where(p=>p.Id == entity.NhanVienId).Select(p=>p.HoTen).ToString()

                }).ToList();



            }
            catch (Exception ex)
            {

            }


        }


    }
}
