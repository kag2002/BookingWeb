using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.SearchingFilter.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingWeb.Modules.SearchingFilter
{
    public class SearchingFilterAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<Phong> _phong;
        private readonly IRepository<HinhThucPhong> _hinhThuc;
        private readonly IRepository<HinhAnh> _hinhAnh;
        private readonly IRepository<DiaDiem> _diaDiem;
        private readonly IRepository<LoaiPhong> _loaiPhong;
        private readonly IRepository<DichVuTienIch> _dichvu;
        private readonly IRepository<NhanXetDanhGia> _nhanXet;
        private readonly IRepository<ChiTietDatPhong> _chiTietDatPhong;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SearchingFilterAppService(IRepository<Phong> phong,
            IRepository<HinhThucPhong> hinhThuc, IRepository<HinhAnh> hinhAnh,
            IRepository<DiaDiem> diaDiem, IRepository<LoaiPhong> loaiPhong,
            IRepository<DichVuTienIch> dichvu, IRepository<NhanXetDanhGia> nhanXet,
            IRepository<ChiTietDatPhong> chiTietDatPhong, 
            IHttpContextAccessor httpContextAccessor)
        {
            _phong = phong;
            _hinhThuc = hinhThuc;
            _hinhAnh = hinhAnh;
            _diaDiem = diaDiem;
            _loaiPhong = loaiPhong;
            _dichvu = dichvu;
            _nhanXet = nhanXet;
            _chiTietDatPhong = chiTietDatPhong;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<List<GetPhongByLocationDto>> GetRoomsByLocation(int id)
        {
            try
            {
                var phongLst = await _phong.GetAllListAsync();
                var phong = phongLst.Where(p => p.DiaDiemId == id).ToList();
                var dtoLstP = new List<GetPhongByLocationDto>();

                if (phong == null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong tim thay!!");
                    return null;
                }
                else
                {
                    var hinhAnh = await _hinhAnh.GetAllListAsync();
                    var dichVu = await _dichvu.GetAllListAsync();

                    foreach (var i in phong)
                    {
                        var diaDiem = await _diaDiem.FirstOrDefaultAsync(p => p.Id == id);
                        var loaiPhong = await _loaiPhong.FirstOrDefaultAsync(p => p.Id == i.LoaiPhongId);
                        var hinhThucPhong = await _hinhThuc.FirstOrDefaultAsync(p => p.Id == i.HinhThucPhongId);
                        
                        var dtoP = new GetPhongByLocationDto
                        {
                            Id = i.Id,
                            TenDonVi = hinhThucPhong.TenDonVi,
                            TenFileAnhDaiDien = i.TenFileAnhDaiDien,
                            DiaChiChiTiet = hinhThucPhong.DiaChiChiTiet,
                            Mota = i.Mota,
                            TrangThaiPhong = i.TrangThaiPhong,
                            DanhGiaSaoTb = i.DanhGiaSaoTb,
                            DiemDanhGiaTB = i.DiemDanhGiaTB,
                            DiaDiem = diaDiem.TenDiaDiem,
                            LoaiPhong = loaiPhong.TenLoaiPhong,
                            HinhThucPhong = hinhThucPhong.TenHinhThuc,
                            GiaPhongTheoDem = loaiPhong.GiaPhongTheoDem,
                            HinhAnh = hinhAnh.Where(p => p.PhongId == i.Id).Select(p => p.TenFileAnh).ToList(),
                            DichVu = dichVu.Where(p => p.LoaiPhongId == i.LoaiPhongId).Select(p => p.TenDichVu).ToList(),
                            MienPhiHuyPhong = i.MienPhiHuyPhong,
                            ChinhSachVePhong = hinhThucPhong.ChinhSachVePhong,
                            ChinhSachVeTreEm = hinhThucPhong.ChinhSachVeTreEm,
                            ChinhSachVeThuCung = hinhThucPhong.ChinhSachVeThuCung
                        };

                        dtoLstP.Add(dtoP);
                    }
                    

                    return dtoLstP;
                }
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return null;
            }
        }

        public async Task<List<GetPhongByLocationDto>> SearchingRoomFilter(SearchingFilterRoomInputDto input)
        {
            try
            {
                var lstInput = await GetRoomsByLocation(input.DiaDiemid);

                var result = new List<GetPhongByLocationDto>();

                if(input.HinhThucPhong == null && 
                    input.GiaPhong.ToString() == null && 
                    input.DanhGiaSao.ToString() ==null &&
                    input.MienPhiHuyPhong.ToString() == null)
                {
                    return lstInput;
                }
                else
                {
                    foreach(var i in lstInput)
                    {
                       
                    }

                }

                return result;
            }
            catch(Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return null;
            }
        }


    }
}
