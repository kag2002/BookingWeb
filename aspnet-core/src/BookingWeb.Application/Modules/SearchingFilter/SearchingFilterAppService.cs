using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.SearchingFilter.Dto;
using BookingWeb.SessionsDefine;
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
        private readonly IRepository<DonViKinhDoanh> _donViKinhDoanh;
        private readonly IRepository<LoaiPhong> _loaiPhong;
        private readonly IRepository<DichVuTienIch> _dichvu;
        private readonly IRepository<ChiTietDatPhong> _chiTietDatPhong;
        private readonly IRepository<NhanXetDanhGia> _nhanXet;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SearchingFilterAppService(IRepository<Phong> phong,
            IRepository<HinhThucPhong> hinhThuc, IRepository<HinhAnh> hinhAnh,
            IRepository<DiaDiem> diaDiem, IRepository<LoaiPhong> loaiPhong,
            IRepository<DichVuTienIch> dichvu,
            IRepository<ChiTietDatPhong> chiTietDatPhong,
            IRepository<NhanXetDanhGia> nhanXet,
            IHttpContextAccessor httpContextAccessor, IRepository<DonViKinhDoanh> donViKinhDoanh)
        {
            _phong = phong;
            _hinhThuc = hinhThuc;
            _hinhAnh = hinhAnh;
            _diaDiem = diaDiem;
            _loaiPhong = loaiPhong;
            _dichvu = dichvu;
            _httpContextAccessor = httpContextAccessor;
            _donViKinhDoanh = donViKinhDoanh;
            _chiTietDatPhong = chiTietDatPhong;
            _nhanXet = nhanXet;
        }


        public async Task<PagedResultDto<PhongSearchinhFilterDto>> GetRoomsByLocationAndFilter(SearchingFilterRoomInputDto input)
        {
            try
            {
                await _httpContextAccessor.HttpContext.Session.ClearAsync();
                await _httpContextAccessor.HttpContext.Session.SetObjectAsync("InfoBooking", input.infoBooking);


                int pageSize = 3;
                var lstP = await _phong.GetAllListAsync();
                var lstDVKD = await _donViKinhDoanh.GetAllListAsync();

                var dvkds = lstDVKD.Where(p => p.DiaDiemId == input.DiaDiemid).ToList();

                var dtoList = new List<PhongSearchinhFilterDto>();

                foreach (var item in dvkds)
                {
                    var phongs = lstP.Where(p => p.DonViKinhDoanhId == item.Id).ToList();

                    if (phongs == null || !phongs.Any())
                    {
                        await _httpContextAccessor.HttpContext.Response.WriteAsync($"Không tìm thấy phòng thuộc địa điểm có id = {input.DiaDiemid}");
                        return null;
                    }
                    else
                    {
                        var hinhAnh = await _hinhAnh.GetAllListAsync();
                        var dichVu = await _dichvu.GetAllListAsync();
                        var loaiPhong = await _loaiPhong.GetAllListAsync();
                        var chiTiet = await _chiTietDatPhong.GetAllListAsync();
                        var nhanXet = await _nhanXet.GetAllListAsync();

                        foreach (var i in phongs)
                        {
                            var diaDiem = await _diaDiem.FirstOrDefaultAsync(p => p.Id == input.DiaDiemid);
                            var hinhThucPhong = await _hinhThuc.FirstOrDefaultAsync(p => p.Id == i.HinhThucPhongId);

                            var dtoP = new PhongSearchinhFilterDto
                            {
                                DonViKinhDoanhId = item.Id,
                                TenDonVi = item.TenDonVi,
                                PhongId = i.Id,
                                TenFileAnhDaiDien = i.TenFileAnhDaiDien,
                                DiaChiChiTiet = item.DiaChiChiTiet,

                                LuotDatPhong = i.LuotDatPhong,
                                DiemDanhGiaTB = i.DiemDanhGiaTB,
                                DanhGiaSaoTb = i.DanhGiaSaoTb,

                                HinhThucPhongId = hinhThucPhong.Id,
                                HinhThucPhong = hinhThucPhong.TenHinhThuc,
                                ListLoaiPhong = loaiPhong.Where(p => p.DonViKinhDoanhId == i.DonViKinhDoanhId).Select(e => new LoaiPhongSearchingFilterDto
                                {
                                    LoaiPhongId = e.Id,
                                    GiaPhongTheoDem = e.GiaPhongTheoDem,
                                    UuDai = e.UuDai

                                }).ToList(),
                            };

                            dtoList.Add(dtoP);
                        }
                    }
                }
                //Filter then sort
                if (input.MienPhiHuyPhong == 1)
                {
                    var lstItem = new List<PhongSearchinhFilterDto>();

                    var filteredRooms = dtoList.Where(p => p.ListLoaiPhong.Select(q => q.MienPhiHuyPhong).ToString().ToLower() == "true").ToList();
                    if (input.GiaPhongNhoNhat != null && input.GiaPhongLonNhat != null)
                    {
                        filteredRooms = filteredRooms.Where(room =>
                                                   (input.GiaPhongNhoNhat <= room.ListLoaiPhong.Select(q => q.GiaPhongTheoDem).Min() &&
                                                   room.ListLoaiPhong.Select(q => q.GiaPhongTheoDem).Min() <= input.GiaPhongLonNhat)
                                               ).ToList();
                    }
                    if (input.DanhGiaSao != null)
                    {
                        filteredRooms = filteredRooms.Where(room => room.DanhGiaSaoTb >= input.DanhGiaSao).ToList();
                    }
                    if (input.HinhThucPhongId != null)
                    {
                        foreach (var i in input.HinhThucPhongId)
                        {
                            var item = filteredRooms.Where(room => room.HinhThucPhongId == i).ToList();

                            lstItem.AddRange(item);
                        }
                    }

                    if (input.GiaCaoNhat == 1)
                    {
                        lstItem = lstItem.OrderByDescending(q => q.ListLoaiPhong.Select(p => p.GiaPhongTheoDem)).ToList();
                    }
                    else if (input.GiaNhoNhat == 1)
                    {
                        lstItem = lstItem.OrderBy(q => q.ListLoaiPhong.Select(p => p.GiaPhongTheoDem)).ToList();
                    }
                    else if (input.DiemDanhGia == 1)
                    {
                        lstItem = lstItem.OrderByDescending(q => q.DiemDanhGiaTB).ToList();
                    }
                    else
                    {
                        lstItem = lstItem.OrderByDescending(q => q.LuotDatPhong).ToList();
                    }

                    var totalCount = lstItem.Count;

                    var pagedRooms = lstItem
                        .Skip((input.pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                    return new PagedResultDto<PhongSearchinhFilterDto>(totalCount, pagedRooms);
                }
                else
                {
                    var lstItem = new List<PhongSearchinhFilterDto>();

                    var filteredRooms = dtoList;

                    if (input.GiaPhongNhoNhat != null && input.GiaPhongLonNhat != null)
                    {
                        filteredRooms = filteredRooms.Where(room =>
                                                   (input.GiaPhongNhoNhat <= room.ListLoaiPhong.Select(q => q.GiaPhongTheoDem).Min() &&
                                                   room.ListLoaiPhong.Select(q => q.GiaPhongTheoDem).Min() <= input.GiaPhongLonNhat)
                                               ).ToList();
                    }
                    if (input.DanhGiaSao != null)
                    {
                        filteredRooms = filteredRooms.Where(room => room.DanhGiaSaoTb >= input.DanhGiaSao).ToList();
                    }
                    if (input.HinhThucPhongId != null)
                    {
                        foreach (var i in input.HinhThucPhongId)
                        {
                            var item = filteredRooms.Where(room => room.HinhThucPhongId == i).ToList();

                            lstItem.AddRange(item);
                        }
                    }

                    if (input.GiaCaoNhat == 1)
                    {
                        lstItem = lstItem.OrderByDescending(q => q.ListLoaiPhong.Select(p => p.GiaPhongTheoDem)).ToList();
                    }
                    else if (input.GiaNhoNhat == 1)
                    {
                        lstItem = lstItem.OrderBy(q => q.ListLoaiPhong.Select(p => p.GiaPhongTheoDem)).ToList();
                    }
                    else if (input.DiemDanhGia == 1)
                    {
                        lstItem = lstItem.OrderByDescending(q => q.DiemDanhGiaTB).ToList();
                    }
                    else
                    {
                        lstItem = lstItem.OrderByDescending(q => q.LuotDatPhong).ToList();
                    }

                    var totalCount = lstItem.Count;

                    var pagedRooms = lstItem
                        .Skip((input.pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                    return new PagedResultDto<PhongSearchinhFilterDto>(totalCount, pagedRooms);

                }
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"Error: {ex.Message}");
                return null;
            }
        }
    }
}
