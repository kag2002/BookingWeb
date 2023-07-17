using Abp.Application.Services.Dto;
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


        public async Task<PagedResultDto<GetPhongByLocationDto>> GetRoomsByLocation(int id, int pageIndex)
        {
            try
            {
                int pageSize = 10;
                var lstP = await _phong.GetAllListAsync();
                var lstDVKD = await _donViKinhDoanh.GetAllListAsync();

                var dvkds = lstDVKD.Where(p => p.DiaDiemId == id).ToList();

                var dtoList = new List<GetPhongByLocationDto>();

                foreach (var item in dvkds)
                {
                    var phongs = lstP.Where(p => p.DonViKinhDoanhId == item.Id).ToList();

                    if (phongs == null || !phongs.Any())
                    {
                        await _httpContextAccessor.HttpContext.Response.WriteAsync($"Không tìm thấy phòng thuộc địa điểm có id = {id}");
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
                            var diaDiem = await _diaDiem.FirstOrDefaultAsync(p => p.Id == id);
                            var hinhThucPhong = await _hinhThuc.FirstOrDefaultAsync(p => p.Id == i.HinhThucPhongId);

                            var dtoP = new GetPhongByLocationDto
                            {
                                DiaDiemId = diaDiem.Id,
                                DiaDiem = diaDiem.TenDiaDiem,
                                ThongTinViTri = diaDiem.ThongTinViTri,
                                DonViKinhDoanhId = item.Id,
                                TenDonVi = item.TenDonVi,
                                PhongId = i.Id,
                                TenFileAnhDaiDien = i.TenFileAnhDaiDien,
                                DiaChiChiTiet = item.DiaChiChiTiet,
                                Mota = i.Mota,
                                DoPhoBien = chiTiet.Where(p => p.PhongId == i.Id).Count(),
                                DanhGiaSaoTb = /*i.DanhGiaSaoTb,*/ nhanXet.Where(p => p.ChiTietDatPhongId == chiTiet.FirstOrDefault(q => q.PhongId == i.Id).Id).Select(p => p.DanhGiaSao).Sum() / nhanXet.Where(p => p.ChiTietDatPhongId == chiTiet.FirstOrDefault(q => q.PhongId == i.Id).Id).Select(p => p.DanhGiaSao).Count(),
                                DiemDanhGiaTB = /*i.DiemDanhGiaTB,*/ nhanXet.Where(p => p.ChiTietDatPhongId == chiTiet.FirstOrDefault(q => q.PhongId == i.Id).Id).Select(p => p.DiemDanhGia).Sum() / nhanXet.Where(p => p.ChiTietDatPhongId == chiTiet.FirstOrDefault(q => q.PhongId == i.Id).Id).Select(p => p.DiemDanhGia).Count(),
                                HinhThucPhongId = hinhThucPhong.Id,
                                HinhThucPhong = hinhThucPhong?.TenHinhThuc,

                                ListLoaiPhong = loaiPhong.Where(p => p.DonViKinhDoanhId == i.DonViKinhDoanhId).Select(e => new LoaiPhongSearchingDto
                                {
                                    LoaiPhongId = e.Id,
                                    LoaiPhong = e.TenLoaiPhong,
                                    TongSLPhong = 100,/*e.TongSlPhong,*/
                                    TrangThaiPhong = e.TrangThaiPhong,
                                    MienPhiHuyPhong = e.MienPhiHuyPhong,
                                    GiaPhongTheoDem = e.GiaPhongTheoDem,
                                    GiaGoiDVThem = e.GiaGoiDichVuThem,
                                    DichVu = dichVu.Where(p => p.LoaiPhongId == e.Id).Select(q => new DichVuSearchingDto
                                    {
                                        DichVuId = q.Id,
                                        TenDichVu = q.TenDichVu,
                                        MoTa = q.MoTa

                                    }).ToList()

                                }).ToList(),

                                HinhAnh = hinhAnh.Where(p => p.PhongId == i.Id).Select(p => p.TenFileAnh).ToList(),
                                ChinhSachVePhong = item.ChinhSachVePhong,
                                ChinhSachVeTreEm = item.ChinhSachVeTreEm,
                                ChinhSachVeThuCung = item.ChinhSachVeThuCung
                            };

                            dtoList.Add(dtoP);
                        }
                    }
                }

                //sort list by DoPhoBien
                dtoList = dtoList.OrderByDescending(p => p.DoPhoBien).ToList();

                // Apply pagination
                var totalCount = dtoList.Count;
                /*var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);*/
                var paginatedItems = dtoList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();


                return new PagedResultDto<GetPhongByLocationDto>(totalCount, paginatedItems);
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"Error: {ex.Message}");
                return null;
            }
        }



        public async Task<PagedResultDto<GetPhongByLocationDto>> SearchingRoomFilter(SearchingFilterRoomInputDto input)
        {
            try
            {
                int pageSize = 10;

                var lstInput = await GetRoomsByLocation(input.DiaDiemid, input.pageIndex);

                
                if(input.MienPhiHuyPhong == "TRUE")
                {
                    var filteredRooms = lstInput.Items.Where(p => p.ListLoaiPhong.Select(q=>q.MienPhiHuyPhong).ToString().ToLower() == "true").ToList();

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
                    else if (input.DoPhoBien == 1)
                    {
                        filteredRooms = filteredRooms.OrderByDescending(q => q.DoPhoBien).ToList();
                    }

                    var totalCount = filteredRooms.Count;

                    var pagedRooms = filteredRooms
                        .Skip((input.pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                    return new PagedResultDto<GetPhongByLocationDto>(totalCount, pagedRooms);
                }
                else
                {
                    var filteredRooms = lstInput.Items.ToList();

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
                    else if (input.DoPhoBien == 1)
                    {
                        filteredRooms = filteredRooms.OrderByDescending(q => q.DoPhoBien).ToList();
                    }

                    var totalCount = filteredRooms.Count;

                    var pagedRooms = filteredRooms
                        .Skip((input.pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                    return new PagedResultDto<GetPhongByLocationDto>(totalCount, pagedRooms);

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
