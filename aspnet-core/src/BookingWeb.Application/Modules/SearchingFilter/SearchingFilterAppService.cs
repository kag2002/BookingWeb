using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.SearchingFilter.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BookingWeb.SessionsDefine;

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
                await _httpContextAccessor.HttpContext.Session.SetObjectAsync("InfoBooking",input.infoBooking);


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
                            var lstCt = chiTiet.Where(p=>p.PhongId==i.Id).ToList();
                            var lstCtIds = lstCt.Select(p=> p.Id).ToList();

                            var dtoP = new PhongSearchinhFilterDto
                            {
                                DonViKinhDoanhId = item.Id,
                                TenDonVi = item.TenDonVi,
                                PhongId = i.Id,
                                TenFileAnhDaiDien = i.TenFileAnhDaiDien,
                                DiaChiChiTiet = item.DiaChiChiTiet,

                                DoPhoBien = chiTiet.Where(p => p.PhongId == i.Id).ToList().Count(),
                                DiemDanhGiaTB = nhanXet.Where(p => lstCtIds.Contains(p.ChiTietDatPhongId)).Select(p => p.DiemDanhGia).ToList().Sum() / nhanXet.Where(p => lstCtIds.Contains(p.ChiTietDatPhongId)).Select(p => p.DiemDanhGia).ToList().Count(),
                                DanhGiaSaoTb = nhanXet.Where(p => lstCtIds.Contains(p.ChiTietDatPhongId)).Select(p => p.DanhGiaSao).ToList().Sum() / nhanXet.Where(p => lstCtIds.Contains(p.ChiTietDatPhongId)).Select(p => p.DanhGiaSao).ToList().Count(),

                                HinhThucPhongId = hinhThucPhong.Id,
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
                if (input.MienPhiHuyPhong == "TRUE")
                {
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
                        filteredRooms = filteredRooms.Where(room => room.HinhThucPhongId == input.HinhThucPhongId).ToList();
                    }

                    if (input.GiaCaoNhat == 1)
                    {
                        filteredRooms = filteredRooms.OrderByDescending(q => q.ListLoaiPhong.Select(p => p.GiaPhongTheoDem)).ToList();
                    }
                    else if (input.GiaNhoNhat == 1)
                    {
                        filteredRooms = filteredRooms.OrderBy(q => q.ListLoaiPhong.Select(p => p.GiaPhongTheoDem)).ToList();
                    }
                    else if (input.DiemDanhGia == 1)
                    {
                        filteredRooms = filteredRooms.OrderByDescending(q => q.DiemDanhGiaTB).ToList();
                    }
                    else
                    {
                        filteredRooms = filteredRooms.OrderByDescending(q => q.DoPhoBien).ToList();
                    }

                    var totalCount = filteredRooms.Count;

                    var pagedRooms = filteredRooms
                        .Skip((input.pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                    return new PagedResultDto<PhongSearchinhFilterDto>(totalCount, pagedRooms);
                }
                else
                {
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
                        filteredRooms = filteredRooms.Where(room => room.HinhThucPhongId == input.HinhThucPhongId).ToList();
                    }

                    if (input.GiaCaoNhat == 1)
                    {
                        filteredRooms = filteredRooms.OrderByDescending(q => q.ListLoaiPhong.Select(p => p.GiaPhongTheoDem)).ToList();
                    }
                    else if (input.GiaNhoNhat == 1)
                    {
                        filteredRooms = filteredRooms.OrderBy(q => q.ListLoaiPhong.Select(p => p.GiaPhongTheoDem)).ToList();
                    }
                    else if (input.DiemDanhGia == 1)
                    {
                        filteredRooms = filteredRooms.OrderByDescending(q => q.DiemDanhGiaTB).ToList();
                    }
                    else
                    {
                        filteredRooms = filteredRooms.OrderByDescending(q => q.DoPhoBien).ToList();
                    }

                    var totalCount = filteredRooms.Count;

                    var pagedRooms = filteredRooms
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
