using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.Phongs.Dto;
using BookingWeb.Modules.SearchingFilter.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingWeb.Modules.Phongs
{
    public class PhongAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<Phong> _phong;
        private readonly IRepository<HinhThucPhong> _hinhThuc;
        private readonly IRepository<DonViKinhDoanh> _donViKinhDoanh;
        private readonly IRepository<HinhAnh> _hinhAnh;
        private readonly IRepository<DiaDiem> _diaDiem;
        private readonly IRepository<LoaiPhong> _loaiPhong;
        private readonly IRepository<DichVuTienIch> _dichvu;
        private readonly IRepository<NhanXetDanhGia> _nhanXet;
        private readonly IRepository<ChiTietDatPhong>  _chiTietDatPhong;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public PhongAppService(IRepository<Phong> phong, IRepository<HinhThucPhong> hinhThuc,
            IRepository<HinhAnh> hinhAnh, IRepository<DiaDiem> diaDiem,
            IRepository<LoaiPhong> loaiPhong, IRepository<DichVuTienIch> dichvu,
            IRepository<NhanXetDanhGia> nhanXet, IRepository<ChiTietDatPhong> chiTietDatPhong
            , IHttpContextAccessor httpContextAccessor, IRepository<DonViKinhDoanh> donViKinhDoanh)
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
            _donViKinhDoanh = donViKinhDoanh;
        }

        public async Task<GetPhongByLocationDto> GetRoomById(int Id)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
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
                                DanhGiaSaoTb = /*i.DanhGiaSaoTb,*/ nhanXet.Where(p=>p.ChiTietDatPhongId == chiTiet.FirstOrDefault(q=>q.PhongId == i.Id).Id).Select(p=>p.DanhGiaSao).Sum() / nhanXet.Where(p => p.ChiTietDatPhongId == chiTiet.FirstOrDefault(q => q.PhongId == i.Id).Id).Select(p => p.DanhGiaSao).Count(),
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
                return dtoList;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"Error: {ex.Message}");
                return null;
            }
        }


        


        public async Task<List<PhongOutputDto>> GetAllRoom()
        {
            try
            {
                var lstP = await _phong.GetAllListAsync();
                var lstHt = await _hinhThuc.GetAllListAsync();
                var lstDd = await _diaDiem.GetAllListAsync();
                var lstDvkd = await _donViKinhDoanh.GetAllListAsync();
                var lstLp = await _loaiPhong.GetAllListAsync();
                var lstDv = await _dichvu.GetAllListAsync();
                var lstA = await _hinhAnh.GetAllListAsync();
                var lstCt = await _chiTietDatPhong.GetAllListAsync();
                var lstNx = await _nhanXet.GetAllListAsync();

                var dtoLst = lstP.Select(entity => new PhongOutputDto
                {
                    DiaDiemId = lstDd.FirstOrDefault(p=>p.Id == (lstDvkd.FirstOrDefault(q=>q.Id == entity.DonViKinhDoanhId)).Id).Id,
                    DiaDiem = lstDd.FirstOrDefault(p => p.Id == (lstDvkd.FirstOrDefault(q => q.Id == entity.DonViKinhDoanhId)).Id).TenDiaDiem,
                    ThongTinViTri = lstDd.FirstOrDefault(p => p.Id == (lstDvkd.FirstOrDefault(q => q.Id == entity.DonViKinhDoanhId)).Id).ThongTinViTri,
                    DonViKinhDoanhId = lstDvkd.FirstOrDefault(q => q.Id == entity.DonViKinhDoanhId).Id,
                    TenDonVi = lstDvkd.FirstOrDefault(q => q.Id == entity.DonViKinhDoanhId).TenDonVi,
                    PhongId = entity.Id,
                    TenFileAnhDaiDien = entity.TenFileAnhDaiDien,
                    DiaChiChiTiet = lstDvkd.FirstOrDefault(q => q.Id == entity.DonViKinhDoanhId).DiaChiChiTiet,
                    Mota = entity.Mota,
                    DoPhoBien = lstCt.Where(p => p.PhongId == entity.Id).Count(),
                    DanhGiaSaoTb = /*entity.DanhGiaSaoTb,*/lstNx.Where(p => p.ChiTietDatPhongId == lstCt.FirstOrDefault(q => q.PhongId == entity.Id).Id).Select(p => p.DanhGiaSao).Sum() / lstNx.Where(p => p.ChiTietDatPhongId == lstCt.FirstOrDefault(q => q.PhongId == entity.Id).Id).Select(p => p.DanhGiaSao).Count(),
                    DiemDanhGiaTB = /*entity.DiemDanhGiaTB,*/lstNx.Where(p => p.ChiTietDatPhongId == lstCt.FirstOrDefault(q => q.PhongId == entity.Id).Id).Select(p => p.DiemDanhGia).Sum() / lstNx.Where(p => p.ChiTietDatPhongId == lstCt.FirstOrDefault(q => q.PhongId == entity.Id).Id).Select(p => p.DiemDanhGia).Count(),
                    HinhThucPhongId = lstHt.FirstOrDefault(p=>p.Id == entity.HinhThucPhongId).Id,
                    HinhThucPhong = lstHt.FirstOrDefault(p => p.Id == entity.HinhThucPhongId).TenHinhThuc,

                    ListLoaiPhong = lstLp.Where(p => p.DonViKinhDoanhId == entity.DonViKinhDoanhId).Select(e => new LoaiPhongSearchingDto
                    {
                        LoaiPhongId = e.Id,
                        LoaiPhong = e.TenLoaiPhong,
                        TongSLPhong = 100,/*e.TongSlPhong,*/
                        TrangThaiPhong = e.TrangThaiPhong,
                        MienPhiHuyPhong = e.MienPhiHuyPhong,
                        GiaPhongTheoDem = e.GiaPhongTheoDem,
                        GiaGoiDVThem = e.GiaGoiDichVuThem,
                        DichVu = lstDv.Where(p => p.LoaiPhongId == e.Id).Select(q => new DichVuSearchingDto
                        {
                            DichVuId = q.Id,
                            TenDichVu = q.TenDichVu,
                            MoTa = q.MoTa

                        }).ToList()

                    }).ToList(),

                    HinhAnh = lstA.Where(p => p.PhongId == entity.Id).Select(p => p.TenFileAnh).ToList(),
                    ChinhSachVePhong = lstDvkd.FirstOrDefault(q => q.Id == entity.DonViKinhDoanhId).ChinhSachVePhong,
                    ChinhSachVeTreEm = lstDvkd.FirstOrDefault(q => q.Id == entity.DonViKinhDoanhId).ChinhSachVeTreEm,
                    ChinhSachVeThuCung = lstDvkd.FirstOrDefault(q => q.Id == entity.DonViKinhDoanhId).ChinhSachVeThuCung

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
                    TenFileAnhDaiDien = input.TenFileAnhDaiDien,
                    DonViKinhDoanhId = input.DonViKinhDoanhId,
                    HinhThucPhongId = input.HinhThucPhongId,

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


        public async Task<bool> UpdateRoom(PhongInputDto input)
        {
            try
            {
                var checkP = await _phong.FirstOrDefaultAsync(p => p.Id == input.Id);

                if (checkP != null)
                {
                    if (input.Mota != null)
                    {
                        checkP.Mota = input.Mota;
                    }

                    if (input.TenFileAnhDaiDien != null)
                    {
                        checkP.TenFileAnhDaiDien = input.TenFileAnhDaiDien;
                    }
                    if (input.HinhThucPhongId != null)
                    {
                        checkP.HinhThucPhongId = input.HinhThucPhongId;
                    }

                    await _phong.UpdateAsync(checkP);
                    return true;
                }
                else
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong ton tai loai phong voi id = {input.Id}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }



        public async Task<bool> DeleteRoom(int id)
        {
            try
            {
                var checkP = await _phong.FirstOrDefaultAsync(p=>p.Id == id);

                if (checkP != null)
                {
                    var hinhAnh = await _hinhAnh.GetAllListAsync();
                    var checkHa = hinhAnh.Where(p => p.PhongId == checkP.Id).ToList();
                    if (checkHa.Any())
                    {
                        foreach (var i in checkHa)
                        {
                            await _hinhAnh.DeleteAsync(i);
                            await _httpContextAccessor.HttpContext.Response.WriteAsync($"da xoa hinh anh {i}");
                        }
                    }

                    var chiTietDatPhong = await _chiTietDatPhong.GetAllListAsync();
                    var checkCtdp = chiTietDatPhong.Where(p=>p.PhongId == checkP.Id).ToList();
                    if(checkCtdp.Any())
                    {
                        var nhanXet = await _nhanXet.GetAllListAsync();
                        foreach (var i in checkCtdp)
                        {
                            var checkNx = nhanXet.Where(p=>p.ChiTietDatPhongId == i.Id).ToList();
                            if (checkNx.Any())
                            {
                                foreach(var j in checkNx)
                                {
                                    await _nhanXet.DeleteAsync(j);
                                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"da xoa nhan xet {j}");

                                }
                            }
                            await _chiTietDatPhong.DeleteAsync(i);
                            await _httpContextAccessor.HttpContext.Response.WriteAsync($"da xoa chi tiet dat phong {i}");
                        }
                    }

                    await _phong.DeleteAsync(checkP);
                    return true;
                }
                else
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong ton tai phong voi id = {id}");
                    return false;
                }


            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }

        }
    }
}
