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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SearchingFilterAppService(IRepository<Phong> phong,
            IRepository<HinhThucPhong> hinhThuc, IRepository<HinhAnh> hinhAnh,
            IRepository<DiaDiem> diaDiem, IRepository<LoaiPhong> loaiPhong,
            IRepository<DichVuTienIch> dichvu, 
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
        }


        public async Task<List<GetPhongByLocationDto>> GetRoomsByDiaDiemId(int id)
        {
            try
            {
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
                                DanhGiaSaoTb = i.DanhGiaSaoTb,
                                DiemDanhGiaTB = i.DiemDanhGiaTB,
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
                return dtoList;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"Error: {ex.Message}");
                return null;
            }
        }



        /*public async Task<PagedResultDto<GetPhongByLocationDto>> SearchingRoomFilter(SearchingFilterRoomInputDto input)
        {
            try
            {
                int pageSize = 10;

                var lstInput = await GetRoomsByDiaDiemId(input.DiaDiemid);

                if (input.HinhThucPhong == null && input.MienPhiHuyPhong == 0 &&
                    input.DanhGiaSao == 0 && input.GiaPhongNhoNhat == 0 && input.GiaPhongLonNhat == 0)
                {
                    var result = lstInput.ToList();

                    if (input.GiaCaoNhat == 1 && input.GiaNhoNhat == 0 && input.DanhGiaSao == 0 && input.DoPhoBien == 0)
                    {
                        result = result.OrderByDescending(p => p..ListLoaiPhong.GiaPhongTheoDem)).ToList();
                    }
                    else if (input.GiaNhoNhat == 1 && input.GiaCaoNhat == 0 && input.DanhGiaSao == 0 && input.DoPhoBien == 0)
                    {
                        result = result.OrderBy(p => p.GiaPhongTheoDem).ToList();
                    }
                    else if (input.DanhGiaSao == 1 && input.GiaNhoNhat == 0 && input.GiaCaoNhat == 0 && input.DoPhoBien == 0)
                    {
                        result = result.OrderBy(p => p.DiemDanhGiaTB).ToList();
                    }
                    else
                    {
                        result = result.OrderBy(p => p.DiemDanhGiaTB).ToList();
                    }

                    var totalCount = result.Count;
                    var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                    var pagedRooms = result
                        .Skip((input.pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                    return new PagedResultDto<GetPhongByLocationDto>(totalCount, pagedRooms);
                }
                else
                {
                    if (input.MienPhiHuyPhong == 0)
                    {


                        var filteredRooms = lstInput.Items.Where(room =>
                        (string.IsNullOrEmpty(input.HinhThucPhong) || room.HinhThucPhong == input.HinhThucPhong) &&
                        (input.GiaPhongNhoNhat <= room.GiaPhongTheoDem && room.GiaPhongTheoDem <= input.GiaPhongLonNhat) &&
                        (input.DanhGiaSao <= room.DanhGiaSaoTb)).ToList();

                        if (input.GiaCaoNhat == 1 && input.GiaNhoNhat == 0 && input.DanhGiaSao == 0 && input.DoPhoBien == 0)
                        {
                            filteredRooms = filteredRooms.OrderByDescending(p => p.GiaPhongTheoDem).ToList();
                        }
                        else if (input.GiaNhoNhat == 1 && input.GiaCaoNhat == 0 && input.DanhGiaSao == 0 && input.DoPhoBien == 0)
                        {
                            filteredRooms = filteredRooms.OrderBy(p => p.GiaPhongTheoDem).ToList();
                        }
                        else if (input.DanhGiaSao == 1 && input.GiaNhoNhat == 0 && input.GiaCaoNhat == 0 && input.DoPhoBien == 0)
                        {
                            filteredRooms = filteredRooms.OrderBy(p => p.DiemDanhGiaTB).ToList();
                        }
                        else
                        {
                            filteredRooms = filteredRooms.OrderBy(p => p.DiemDanhGiaTB).ToList();
                        }


                        var totalCount = filteredRooms.Count;
                        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                        var pagedRooms = filteredRooms
                            .Skip((input.pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

                        return new PagedResultDto<GetPhongByLocationDto>(totalCount, pagedRooms);
                    }
                    else if (input.MienPhiHuyPhong == 1)
                    {
                        var filteredRooms = lstInput.Items.Where(room =>
                        (string.IsNullOrEmpty(input.HinhThucPhong) || room.HinhThucPhong == input.HinhThucPhong) &&
                        (input.GiaPhongNhoNhat <= room.GiaPhongTheoDem && room.GiaPhongTheoDem <= input.GiaPhongLonNhat) &&
                        (input.DanhGiaSao <= room.DanhGiaSaoTb) &&
                        (input.MienPhiHuyPhong == room.MienPhiHuyPhong)).ToList();

                        if (input.GiaCaoNhat == 1 && input.GiaNhoNhat == 0 && input.DanhGiaSao == 0 && input.DoPhoBien == 0)
                        {
                            filteredRooms = filteredRooms.OrderByDescending(p => p.GiaPhongTheoDem).ToList();
                        }
                        else if (input.GiaNhoNhat == 1 && input.GiaCaoNhat == 0 && input.DanhGiaSao == 0 && input.DoPhoBien == 0)
                        {
                            filteredRooms = filteredRooms.OrderBy(p => p.GiaPhongTheoDem).ToList();
                        }
                        else if (input.DanhGiaSao == 1 && input.GiaNhoNhat == 0 && input.GiaCaoNhat == 0 && input.DoPhoBien == 0)
                        {
                            filteredRooms = filteredRooms.OrderBy(p => p.DiemDanhGiaTB).ToList();
                        }
                        else
                        {
                            filteredRooms = filteredRooms.OrderBy(p => p.DiemDanhGiaTB).ToList();
                        }

                        var totalCount = filteredRooms.Count;
                        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                        var pagedRooms = filteredRooms
                            .Skip((input.pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

                        return new PagedResultDto<GetPhongByLocationDto>(totalCount, pagedRooms);
                    }
                }
                return null;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"Error: {ex.Message}");
                return null;
            }
        }*/





    }
}
