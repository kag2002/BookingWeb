using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.DiaDiems.Dto;
using BookingWeb.Modules.Phongs.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.Phongs
{
    public class PhongAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<Phong> _phong;
        private readonly IRepository<HinhThucKinhDoanh> _hinhThuc;
        private readonly IRepository<HinhAnh> _hinhAnh;
        private readonly IRepository<DiaDiem> _diaDiem;
        private readonly IRepository<ChinhSachQuyDinh> _chinhSach;
        private readonly IRepository<LoaiPhong> _loaiPhong;
        private readonly IRepository<DichVuTienIch> _dichvu;
        private readonly IRepository<NhanXetDanhGia> _nhanXet;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public PhongAppService(IRepository<Phong> phong, IRepository<HinhThucKinhDoanh> hinhThuc,
            IRepository<HinhAnh> hinhAnh, IRepository<DiaDiem> diaDiem, IRepository<ChinhSachQuyDinh> chinhSach,
            IRepository<LoaiPhong> loaiPhong, IRepository<DichVuTienIch> dichvu,
            IRepository<NhanXetDanhGia> nhanXet, IHttpContextAccessor httpContextAccessor)
        {
            _phong = phong;
            _hinhThuc = hinhThuc;
            _hinhAnh = hinhAnh;
            _diaDiem = diaDiem;
            _chinhSach = chinhSach;
            _loaiPhong = loaiPhong;
            _dichvu = dichvu;
            _nhanXet = nhanXet;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<PhongOutputDto>> GetAllRoom()
        {
            try
            {
                var lstP = await _phong.GetAllListAsync();
                var lstHt = await _hinhThuc.GetAllListAsync();
                var lstDd = await _diaDiem.GetAllListAsync();
                var lstLp = await _loaiPhong.GetAllListAsync();
                var lstCs = await _chinhSach.GetAllListAsync();

                var dtoLst = lstP.Select(entity => new PhongOutputDto
                {
                    Id = entity.Id,
                    HinhThucKinhDoanh = lstHt.Where(p => p.Id == entity.HinhThucKinhDoanhId)
                                            .Select(p => p.TenHinhThuc).ToList(),

                    DiaDiem = lstDd.Where(p => p.Id == entity.DiaDiemId)
                                    .Select(p => p.TenDiaDiem).ToList(),
                    
                    LoaiPhong = lstLp.Where(p => p.Id == entity.LoaiPhongId)
                                    .Select(p => p.TenLoaiPhong).ToList(),

                    Mota = entity.Mota,
                    TrangThaiPhong = entity.TrangThaiPhong,
                    DiaChiChiTiet = entity.DiaChiChiTiet,
                    TenFileAnhDaiDien = entity.TenFileAnhDaiDien,
                    ChinhSach = lstCs.Where(b => b.HinhThucKinhDoanhId == entity.HinhThucKinhDoanhId)
                                    .Select(b => $"{b.QuyDinhVeThuCung}, {b.QuyDinhVeTreEm}, {b.QuyDinhVeDatPhong}")
                                    .ToList()
                       

                }).ToList();

                return dtoLst;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return null;
            }
        }


        public async Task<bool> AddNewRoom(PhongDto input)
        {
            try
            {
                var lp = new Phong
                {
                    Mota = input.Mota,
                    TrangThaiPhong = input.TrangThaiPhong,
                    TenFileAnhDaiDien = input.TenFileAnhDaiDien,
                    DiaDiemId = input.DiaDiemId,
                    LoaiPhongId = input.LoaiPhongId,
                    HinhThucKinhDoanhId = input.HinhThucKinhDoanhId
                };
                await _phong.InsertAsync(lp);
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
