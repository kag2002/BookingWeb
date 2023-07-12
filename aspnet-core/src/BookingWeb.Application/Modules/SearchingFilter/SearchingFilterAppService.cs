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
        private readonly IRepository<LoaiPhong> _loaiPhong;
        private readonly IRepository<DichVuTienIch> _dichvu;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SearchingFilterAppService(IRepository<Phong> phong,
            IRepository<HinhThucPhong> hinhThuc, IRepository<HinhAnh> hinhAnh,
            IRepository<DiaDiem> diaDiem, IRepository<LoaiPhong> loaiPhong,
            IRepository<DichVuTienIch> dichvu, 
            IHttpContextAccessor httpContextAccessor)
        {
            _phong = phong;
            _hinhThuc = hinhThuc;
            _hinhAnh = hinhAnh;
            _diaDiem = diaDiem;
            _loaiPhong = loaiPhong;
            _dichvu = dichvu;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<PagedResultDto<GetPhongByLocationDto>> GetRoomsByLocation(int id, int pageIndex)
        {
            try
            {
                int pageSize = 10;

                var phongLst = await _phong.GetAllListAsync();
                var phong = phongLst.Where(p => p.DiaDiemId == id).ToList();

                if (!phong.Any())
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"Không tìm thấy phòng thuộc địa điểm có Id = {id}!");
                    return null;
                }

                var totalItemCount = phong.Count;
                var totalPages = (int)Math.Ceiling((double)totalItemCount / pageSize);

                var pagedPhong = phong
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var hinhAnh = await _hinhAnh.GetAllListAsync();
                var dichVu = await _dichvu.GetAllListAsync();

                var dtoLstP = new List<GetPhongByLocationDto>();

                foreach (var i in pagedPhong)
                {
                    var diaDiem = await _diaDiem.FirstOrDefaultAsync(p => p.Id == id);
                    var loaiPhong = await _loaiPhong.FirstOrDefaultAsync(p => p.Id == i.LoaiPhongId);
                    var hinhThucPhong = await _hinhThuc.FirstOrDefaultAsync(p => p.Id == i.HinhThucPhongId);

                    var dtoP = new GetPhongByLocationDto
                    {
                        Id = i.Id,
                        TenDonVi = hinhThucPhong?.TenDonVi,
                        TenFileAnhDaiDien = i?.TenFileAnhDaiDien,
                        DiaChiChiTiet = hinhThucPhong?.DiaChiChiTiet,
                        Mota = i.Mota,
                        TrangThaiPhong = i.TrangThaiPhong,
                        DanhGiaSaoTb = i.DanhGiaSaoTb,
                        DiemDanhGiaTB = i.DiemDanhGiaTB,
                        DiaDiem = diaDiem?.TenDiaDiem,
                        LoaiPhong = loaiPhong?.TenLoaiPhong,
                        HinhThucPhong = hinhThucPhong?.TenHinhThuc,
                        GiaPhongTheoDem = loaiPhong.GiaPhongTheoDem,
                        HinhAnh = hinhAnh.Where(p => p.PhongId == i.Id).Select(p => p.TenFileAnh).ToList(),
                        DichVu = dichVu.Where(p => p.LoaiPhongId == i.LoaiPhongId).Select(p => p.TenDichVu).ToList(),
                        MienPhiHuyPhong = i.MienPhiHuyPhong,
                        ChinhSachVePhong = hinhThucPhong?.ChinhSachVePhong,
                        ChinhSachVeTreEm = hinhThucPhong?.ChinhSachVeTreEm,
                        ChinhSachVeThuCung = hinhThucPhong?.ChinhSachVeThuCung
                    };

                    dtoLstP.Add(dtoP);
                }

                return new PagedResultDto<GetPhongByLocationDto>(totalItemCount, dtoLstP);
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
                int pageSize = 10 ;

                var lstInput = await GetRoomsByLocation(input.DiaDiemid, input.pageIndex);

                if (input.HinhThucPhong == null && input.MienPhiHuyPhong == 0 &&
                    input.DanhGiaSao == 0 && input.GiaPhongNhoNhat == 0 && input.GiaPhongLonNhat == 0)
                {
                    var result = lstInput.Items.ToList();

                    if (input.GiaCaoNhat == 1 && input.GiaNhoNhat == 0 && input.DanhGiaSao == 0 && input.DoPhoBien == 0)
                    {
                        result = result.OrderByDescending(p => p.GiaPhongTheoDem).ToList();
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
        }





    }
}
