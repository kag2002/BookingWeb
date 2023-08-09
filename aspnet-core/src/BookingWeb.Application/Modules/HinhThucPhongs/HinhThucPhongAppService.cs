using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Module.HinhThucKinhDoanhs.Dto;
using BookingWeb.Modules.HinhThucKinhDoanhs.Dto;
using BookingWeb.Modules.HinhThucPhongs.Dto;
using BookingWeb.Modules.SearchingFilter.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingWeb.Modules.HinhThucKinhDoanhs
{
    public class HinhThucPhongAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<HinhThucPhong> _hinhThuc;
        private readonly IRepository<Phong> _phong;
        private readonly IRepository<HinhAnh> _hinhAnh;
        private readonly IRepository<DonViKinhDoanh> _donViKinhDoanh;
        private readonly IRepository<LoaiPhong> _loaiPhong;
        private readonly IRepository<DichVuTienIch> _dichVuTienIch;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public HinhThucPhongAppService(IRepository<HinhThucPhong> hinhThuc,
            IRepository<Phong> phong, IRepository<HinhAnh> hinhAnh, 
            IRepository<DonViKinhDoanh> donViKinhDoanh, 
            IRepository<LoaiPhong> loaiPhong, 
            IRepository<DichVuTienIch> dichVuTienIch, 
            IHttpContextAccessor httpContextAccessor)
        {
            _hinhThuc = hinhThuc;
            _phong = phong;
            _hinhAnh = hinhAnh;
            _donViKinhDoanh = donViKinhDoanh;
            _loaiPhong = loaiPhong;
            _dichVuTienIch = dichVuTienIch;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<PhongSearchinhFilterDto>> GetRoomByForm(int id)
        {
            try
            {
                var lstHtp = await _hinhThuc.GetAllListAsync();
                var lstPhong = await _phong.GetAllListAsync();
                var lstDvkd = await _donViKinhDoanh.GetAllListAsync();
                var lstLp = await _loaiPhong.GetAllListAsync();
                var lstDv = await _dichVuTienIch.GetAllListAsync();
                var lstha = await _hinhAnh.GetAllListAsync();

                var lstP = lstPhong.Where(p => p.HinhThucPhongId == id).ToList();

                var dtoLstP = lstP.Select(e => new PhongSearchinhFilterDto
                {
                    DonViKinhDoanhId = e.DonViKinhDoanhId,
                    TenDonVi = _donViKinhDoanh.FirstOrDefault(p=>p.Id == e.DonViKinhDoanhId).TenDonVi,
                    PhongId = e.Id,
                    TenFileAnhDaiDien = e.TenFileAnhDaiDien,
                    DiaChiChiTiet = _donViKinhDoanh.FirstOrDefault(p => p.Id == e.DonViKinhDoanhId).DiaChiChiTiet,

                    LuotDatPhong = e.LuotDatPhong,
                    DiemDanhGiaTB = e.DiemDanhGiaTB,
                    DanhGiaSaoTb = e.DanhGiaSaoTb,

                    HinhThucPhongId = id,
                    HinhThucPhong = _hinhThuc.FirstOrDefault(p=>p.Id==e.HinhThucPhongId).TenHinhThuc,
                    GiaPhongThapNhat = lstLp.Where(p => p.DonViKinhDoanhId == e.DonViKinhDoanhId).Select(q => q.GiaPhongTheoDem).Min(),
                    ListLoaiPhong = lstLp.Where(p => p.DonViKinhDoanhId == e.DonViKinhDoanhId).Select(x => new LoaiPhongSearchingFilterDto
                    {
                        LoaiPhongId = x.Id,
                        GiaPhongTheoDem = x.GiaPhongTheoDem,
                        UuDai = x.UuDai

                    }).ToList(),

                }).ToList();
                return dtoLstP;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"erorr : {ex.Message}");
                return null;
            }

        }

        public async Task<List<HinhThucPhongFullDto>> GetAllForms()
        {
            try
            {
                var lst = await _hinhThuc.GetAllListAsync();

                var dtoList = lst.Select(entity => new HinhThucPhongFullDto
                {
                    Id = entity.Id,
                    TenHinhThuc = entity.TenHinhThuc,
                    AnhDaiDien = entity.AnhDaiDien
                }).ToList();

                return dtoList;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"erorr : {ex.Message}");
                return null;
            }

        }


        public async Task<bool> AddNewForm(HinhThucPhongDto input)
        {
            try
            {
                var htkd = new HinhThucPhong
                {
                    TenHinhThuc = input.TenHinhThuc,
                    AnhDaiDien = input.AnhDaiDien
                };

                await _hinhThuc.InsertAsync(htkd);

                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateForm(HinhThucPhongFullDto input)
        {
            try
            {
                var item = await _hinhThuc.FirstOrDefaultAsync(p => p.Id == input.Id);
                if (item == null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong tim thay loai phong voi id = {input.Id}");
                    return false;
                }

                item.TenHinhThuc = input.TenHinhThuc;
                item.AnhDaiDien = input.AnhDaiDien;

                await _hinhThuc.UpdateAsync(item);
                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteForm(int id)
        {
            try
            {
                var checkHt = await _hinhThuc.FirstOrDefaultAsync(p => p.Id == id);
                if (checkHt == null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong tim thay loai phong voi id = {id}");
                    return false;
                }
                else
                {
                    var phong = await _phong.GetAllListAsync();
                    var checkPhong = phong.Where(p => p.HinhThucPhongId == checkHt.Id).ToList();

                    if (checkPhong.Any())
                    {
                        foreach (var i in checkPhong)
                        {
                            i.HinhThucPhongId = null;
                        }
                    }

                    await _hinhThuc.DeleteAsync(checkHt);
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"da xoa hinh thuc kinh doanh {checkHt}");
                    return true;
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
