﻿using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.Phongs.Dto;
using BookingWeb.Modules.SearchingFilter.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BookingWeb.SessionsDefine;
using BookingWeb.Authorization.Users;
using BookingWeb.Modules.ChinhSachChungs.Dto;
using BookingWeb.Modules.DichVuTienIchChungs.Dto;

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
        private readonly IRepository<PhieuDatPhong> _phieuDatPhong;
        private readonly IReadOnlyList<LstTrangThaiPhong> _trangThaiPhong;
        private readonly IRepository<ChinhSachChung> _chinhSachChung;
        private readonly IRepository<DichVuTienIchChung> _dichVuChung;


        private readonly UserManager _userManager;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public PhongAppService(IRepository<Phong> phong, IRepository<HinhThucPhong> hinhThuc,
            IRepository<HinhAnh> hinhAnh, IRepository<DiaDiem> diaDiem,
            IRepository<LoaiPhong> loaiPhong, IRepository<DichVuTienIch> dichvu,
            IRepository<NhanXetDanhGia> nhanXet, IRepository<ChiTietDatPhong> chiTietDatPhong,
            IRepository<KhachHang> khachHang, IRepository<PhieuDatPhong> phieuDatPhong,
            IReadOnlyList<LstTrangThaiPhong> trangThaiPhong, UserManager userManager,
            IRepository<ChinhSachChung> chinhSachChung, IRepository<DichVuTienIchChung> dichVuChung,
            IHttpContextAccessor httpContextAccessor, IRepository<DonViKinhDoanh> donViKinhDoanh)
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
            _trangThaiPhong = trangThaiPhong;
            _phieuDatPhong = phieuDatPhong;
            _userManager = userManager;
            _chinhSachChung = chinhSachChung;
            _dichVuChung = dichVuChung;
        }

        public async Task<GetInfoPhongDto> GetRoomById(int Id)
        {
            try
            {
                var phong = await _phong.FirstOrDefaultAsync(p=>p.Id == Id);
                if(phong == null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong ton tai phong voi id = {Id}");
                    return null;
                }
                else
                {
                    var lstHt = await _hinhThuc.FirstOrDefaultAsync(p => p.Id == phong.HinhThucPhongId);

                    var lstDvkd = await _donViKinhDoanh.FirstOrDefaultAsync(p => p.Id == phong.DonViKinhDoanhId);

                    var lstDd = await _diaDiem.FirstOrDefaultAsync(p => p.Id == lstDvkd.DiaDiemId);

                    var lp = await _loaiPhong.GetAllListAsync();
                    var lstLp = lp.Where(p => p.DonViKinhDoanhId == lstDvkd.Id).ToList();
                    var lstLpIds = lp.Select(p => p.Id).ToList();

                    var dv = await _dichvu.GetAllListAsync();

                    var anh = await _hinhAnh.GetAllListAsync();
                    var lstA = anh.Where(p => p.PhongId == phong.Id).ToList();

                    var ct = await _chiTietDatPhong.GetAllListAsync();
                    var lstCt = ct.Where(p => p.PhongId == phong.Id).ToList();
                    var lstCtIds = lstCt.Select(p => p.Id).ToList();

                    var lstNx = await _nhanXet.GetAllListAsync();

                    var lstDvc = await _dichVuChung.GetAllListAsync();
                    var Dvc = lstDvc.Where(p=>p.DonViKinhDoanhId == phong.DonViKinhDoanhId).ToList();

                    var lstCsc = await _chinhSachChung.GetAllListAsync();
                    var Csc = lstCsc.Where(p => p.DonViKinhDoanhId == phong.DonViKinhDoanhId).ToList();

                    var dtoLst = new GetInfoPhongDto
                    {
                        DiaDiemId = lstDd.Id,
                        TenDiaDiem = lstDd.TenDiaDiem,
                        ThongTinViTri = lstDd.ThongTinViTri,

                        DonViKinhDoanhId = lstDvkd.Id,
                        TenDonVi = lstDvkd.TenDonVi,
                        DiaChiChiTiet = lstDvkd.DiaChiChiTiet,

                        HinhThucPhongId = lstHt.Id,
                        HinhThucPhong = lstHt.TenHinhThuc,

                        PhongId = phong.Id,
                        Mota = phong.Mota,
                        TenFileAnhDaiDien = phong.TenFileAnhDaiDien,

                        LuoDatPhong = phong.LuotDatPhong,
                        DiemDanhGiaTB = phong.DiemDanhGiaTB,
                        DanhGiaSaoTb =phong.DanhGiaSaoTb,
                        /*lstNx.Where(p => lstCtIds.Contains(p.ChiTietDatPhongId)).Select(p => p.DanhGiaSao).ToList().Sum() / lstNx.Where(p => lstCtIds.Contains(p.ChiTietDatPhongId)).Select(p => p.DanhGiaSao).ToList().Count(),*/
                        ListLoaiPhong = lstLp.Select(e => new LoaiPhongSearchingDto
                        {
                            LoaiPhongId = e.Id,
                            LoaiPhong = e.TenLoaiPhong,
                            SucChua = e.SucChua,
                            TongSLPhong = e.TongSlPhong,
                            TrangThaiPhong = e.TrangThaiPhong,
                            MienPhiHuyPhong = e.MienPhiHuyPhong,
                            GiaPhongTheoDem = e.GiaPhongTheoDem,
                            GiaGoiDVThem = e.GiaGoiDichVuThem,
                            DichVu = dv.Where(p => p.LoaiPhongId == e.Id).Select(q => new DichVuSearchingDto
                            {
                                DichVuId = q.Id,
                                TenDichVu = q.TenDichVu,
                                MoTa = q.MoTa
                            }).ToList(),
                        }).ToList(),

                        ChinhSachChung = Csc.Select(e=> new ChinhSachChungDto
                        {
                            Id = e.Id,
                            KiemTraThongTin = e.KiemTraThongTin,
                            BuaSang = e.BuaSang,
                            NhanPhong = e.NhanPhong,
                            TraPhong = e.TraPhong,
                            ChinhSachVePhong=e.ChinhSachVePhong,
                            ChinhSachTreEm = e.ChinhSachTreEm,
                            ChinhSachVeGiuongPhu = e.ChinhSachVeGiuongPhu,
                            ChinhSachVeThuCung = e.ChinhSachVeThuCung,
                            PhuongThucThanhToan = e.PhuongThucThanhToan
                        }).ToList(),

                        DichVuChung = Dvc.Select(e => new DichVuChungDto{
                            Id = e.Id,
                            TenDichVu=e.TenDichVu,
                            ChiTiet = e.ChiTiet
                        }).ToList(),

                        HinhAnh = lstA.Select(p => p.TenFileAnh).ToList()
                    };
                    return dtoLst;
                }
                
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<List<GetInfoPhongDto>> GetRoomsByDiaDiemId(int diaDiemId)
        {
            try
            {
                var lstP = await _phong.GetAllListAsync();
                var lstDVKD = await _donViKinhDoanh.GetAllListAsync();

                var dvkds = lstDVKD.Where(p => p.DiaDiemId == diaDiemId).ToList();

                var dtoList = new List<GetInfoPhongDto>();

                var hinhAnh = await _hinhAnh.GetAllListAsync();
                var dichVu = await _dichvu.GetAllListAsync();
                var loaiPhong = await _loaiPhong.GetAllListAsync();
                var chiTiet = await _chiTietDatPhong.GetAllListAsync();
                var nhanXet = await _nhanXet.GetAllListAsync();
                var dichVuChung = await _dichVuChung.GetAllListAsync();
                var chinhSachChung = await _chinhSachChung.GetAllListAsync();

                foreach (var item in dvkds)
                {
                    var phongs = lstP.Where(p => p.DonViKinhDoanhId == item.Id).ToList();

                    if (phongs == null || !phongs.Any())
                    {
                        await _httpContextAccessor.HttpContext.Response.WriteAsync($"Không tìm thấy phòng thuộc địa điểm có id = {diaDiemId}");
                        return null;
                    }
                    else
                    {
                        foreach (var i in phongs)
                        {
                            var diaDiem = await _diaDiem.FirstOrDefaultAsync(p => p.Id == diaDiemId);
                            var hinhThucPhong = await _hinhThuc.FirstOrDefaultAsync(p => p.Id == i.HinhThucPhongId);

                            var dtoP = new GetInfoPhongDto
                            {
                                DiaDiemId = diaDiem.Id,
                                TenDiaDiem = diaDiem.TenDiaDiem,
                                ThongTinViTri = diaDiem.ThongTinViTri,

                                DonViKinhDoanhId = item.Id,
                                TenDonVi = item.TenDonVi,
                                DiaChiChiTiet = item.DiaChiChiTiet,

                                HinhThucPhong = hinhThucPhong?.TenHinhThuc,
                                HinhThucPhongId = hinhThucPhong.Id,

                                PhongId = i.Id,
                                Mota = i.Mota,
                                TenFileAnhDaiDien = i.TenFileAnhDaiDien,

                                LuoDatPhong = i.LuotDatPhong,
                                DanhGiaSaoTb = i.DanhGiaSaoTb,
                                DiemDanhGiaTB = i.DiemDanhGiaTB,

                                ListLoaiPhong = loaiPhong.Where(p => p.DonViKinhDoanhId == i.DonViKinhDoanhId).Select(e => new LoaiPhongSearchingDto
                                {
                                    LoaiPhongId = e.Id,
                                    LoaiPhong = e.TenLoaiPhong,
                                    SucChua = e.SucChua,
                                    TongSLPhong = e.TongSlPhong,
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

                                ChinhSachChung = chinhSachChung.Where(p=>p.DonViKinhDoanhId == i.DonViKinhDoanhId).Select(e=> new ChinhSachChungDto
                                {
                                    Id = e.Id,
                                    KiemTraThongTin = e.KiemTraThongTin,
                                    BuaSang = e.BuaSang,
                                    NhanPhong = e.NhanPhong,
                                    TraPhong = e.TraPhong,
                                    ChinhSachVePhong = e.ChinhSachVePhong,
                                    ChinhSachTreEm = e.ChinhSachTreEm,
                                    ChinhSachVeGiuongPhu = e.ChinhSachVeGiuongPhu,
                                    ChinhSachVeThuCung = e.ChinhSachVeThuCung,
                                    PhuongThucThanhToan = e.PhuongThucThanhToan
                                }).ToList(),

                                DichVuChung = dichVuChung.Where(p=>p.DonViKinhDoanhId == i.DonViKinhDoanhId).Select(e => new DichVuChungDto
                                {
                                    Id = e.Id,
                                    TenDichVu = e.TenDichVu,
                                    ChiTiet = e.ChiTiet
                                }).ToList(),

                                HinhAnh = hinhAnh.Where(p => p.PhongId == i.Id).Select(p => p.TenFileAnh).ToList(),
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


        public async Task<List<GetInfoPhongDto>> GetAllRoom()
        {
            try
            {
                var lstP = await _phong.GetAllListAsync();
                var dv = await _dichvu.GetAllListAsync();
                var lp = await _loaiPhong.GetAllListAsync();
                var anh = await _hinhAnh.GetAllListAsync();
                var lstNx = await _nhanXet.GetAllListAsync();
                var dvc = await _dichVuChung.GetAllListAsync();
                var csc = await _chinhSachChung.GetAllListAsync();

                var dtoLst = new List<GetInfoPhongDto>();

                foreach (var i in lstP)
                {
                    var lstHt = await _hinhThuc.FirstOrDefaultAsync(p => p.Id == i.HinhThucPhongId);

                    var lstDvkd = await _donViKinhDoanh.FirstOrDefaultAsync(p => p.Id == i.DonViKinhDoanhId);

                    var lstDd = await _diaDiem.FirstOrDefaultAsync(p => p.Id == lstDvkd.DiaDiemId);

                    var dto = new GetInfoPhongDto
                    {
                        DiaDiemId = lstDd.Id,
                        TenDiaDiem = lstDd.TenDiaDiem,
                        ThongTinViTri = lstDd.ThongTinViTri,

                        DonViKinhDoanhId = lstDvkd.Id,
                        TenDonVi = lstDvkd.TenDonVi,
                        DiaChiChiTiet = lstDvkd.DiaChiChiTiet,

                        HinhThucPhongId = lstHt.Id,
                        HinhThucPhong = lstHt.TenHinhThuc,

                        PhongId = i.Id,
                        Mota = i.Mota,
                        TenFileAnhDaiDien = i.TenFileAnhDaiDien,

                        LuoDatPhong = i.LuotDatPhong,
                        DiemDanhGiaTB = i.DiemDanhGiaTB,
                        DanhGiaSaoTb = i.DanhGiaSaoTb,

                        ListLoaiPhong = lp.Where(p => p.DonViKinhDoanhId == lstDvkd.Id).Select(e => new LoaiPhongSearchingDto
                        {
                            LoaiPhongId = e.Id,
                            LoaiPhong = e.TenLoaiPhong,
                            SucChua =e.SucChua,
                            TongSLPhong = e.TongSlPhong,
                            TrangThaiPhong = e.TrangThaiPhong,
                            MienPhiHuyPhong = e.MienPhiHuyPhong,
                            GiaPhongTheoDem = e.GiaPhongTheoDem,
                            GiaGoiDVThem = e.GiaGoiDichVuThem,

                            DichVu = dv.Where(p => p.LoaiPhongId == e.Id).Select(q => new DichVuSearchingDto
                            {
                                DichVuId = q.Id,
                                TenDichVu = q.TenDichVu,
                                MoTa = q.MoTa

                            }).ToList(),

                        }).ToList(),

                        ChinhSachChung = csc.Where(p => p.DonViKinhDoanhId == i.DonViKinhDoanhId).Select(e => new ChinhSachChungDto
                        {
                            Id = e.Id,
                            KiemTraThongTin = e.KiemTraThongTin,
                            BuaSang = e.BuaSang,
                            NhanPhong = e.NhanPhong,
                            TraPhong = e.TraPhong,
                            ChinhSachVePhong = e.ChinhSachVePhong,
                            ChinhSachTreEm = e.ChinhSachTreEm,
                            ChinhSachVeGiuongPhu = e.ChinhSachVeGiuongPhu,
                            ChinhSachVeThuCung = e.ChinhSachVeThuCung,
                            PhuongThucThanhToan = e.PhuongThucThanhToan
                        }).ToList(),

                        DichVuChung = dvc.Where(p => p.DonViKinhDoanhId == i.DonViKinhDoanhId).Select(e => new DichVuChungDto
                        {
                            Id = e.Id,
                            TenDichVu = e.TenDichVu,
                            ChiTiet = e.ChiTiet
                        }).ToList(),

                        HinhAnh = anh.Where(p => p.PhongId == i.Id).Select(p => p.TenFileAnh).ToList()
                    };

                    dtoLst.Add(dto);
                }

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


        public async Task<GetInfoRoomToBookOutputDto> GetInfoRoomToBook(int loaiPhongId)
        {
            try
            {
                /*var infoRoom = await _httpContextAccessor.HttpContext.Session.GetObjectAsync<InfoBookingDto>("InfoBooking");*/

                var info = await _loaiPhong.FirstOrDefaultAsync(p=>p.Id ==  loaiPhongId);

                var dvkd = await _donViKinhDoanh.FirstOrDefaultAsync(p => p.Id == info.DonViKinhDoanhId);

                var phong = await _phong.FirstOrDefaultAsync(p => p.DonViKinhDoanhId == dvkd.Id);

                var dto = new GetInfoRoomToBookOutputDto
                {
                    donViKinhDoanhId = dvkd.Id,
                    tenDonVi = dvkd.TenDonVi,
                    phongId = phong.Id,
                    loaiPhongId = info.Id,
                    tenLoaiPhong = info.TenLoaiPhong,
                    sucChuaPhong =info.SucChua,
                    moTaPhong = info.MoTa,
                    tienNghi = info.TienNghiTrongPhong,
                    giaPhongTheoDem = info.GiaPhongTheoDem,
                    mienPhiHuyPhong = info.MienPhiHuyPhong.ToString(),
                    /*infoBookingDto = infoRoom*/
                };
                return dto;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return null;
            }
        }


        public async Task<ClientBookRoomOutputDto> ClientBookRoom(ClientBookRoomInputDto input)
        {
            try
            {
                var infoRoom = await GetInfoRoomToBook(input.loaiPhongId);

                 

/*                if(input.DatHo != 1)
                {
                    input.CCCD = khachHang.CCCD;
                    input.HoTen = khachHang.HoTen;
                    input.SDT = khachHang.SoDienThoai;
                    input.Email = khachHang.Email;
                }
*/
                var infoBooking = new ClientBookRoomOutputDto
                {
                    donViKinhDoanhId = infoRoom.donViKinhDoanhId,
                    tenDonVi = infoRoom.tenDonVi,
                    phongId = infoRoom.phongId,
                    tenLoaiPhong =infoRoom.tenLoaiPhong,
                    infoBookingDto = infoRoom.infoBookingDto,
                    sucChuaPhong = infoRoom.sucChuaPhong,
                    moTaPhong = infoRoom.moTaPhong,
                    tienNghi = infoRoom.tienNghi,
                    giaPhongTheoDem = infoRoom.giaPhongTheoDem,
                    mienPhiHuyPhong = infoRoom.mienPhiHuyPhong,
                    TongTien = input.TongTien,
                    DatHo = input.DatHo,
                    HoTen = input.HoTen,
                    SDT = input.SDT,
                    Email = input.Email,
                    YeuCauDacBiet = input.YeuCauDacBiet
                };

                await _httpContextAccessor.HttpContext.Session.SetObjectAsync("infoBooking", infoBooking);
                return infoBooking;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return null;
            }
        }

        public async Task<bool> ConfirmBookRoom()
        {
            try
            {
                var infoBooking = await _httpContextAccessor.HttpContext.Session.GetObjectAsync<ClientBookRoomOutputDto>("infoBooking");

                var newPhieuDat = new PhieuDatPhong
                {

                    HoTen = infoBooking.HoTen,
                    SDT = infoBooking.SDT,
                    Email = infoBooking.Email,
                    NgayBatDau = infoBooking.infoBookingDto.NgayDat,
                    NgayHenTra = infoBooking.infoBookingDto.NgayTra,
                    DatHo = infoBooking.DatHo,
                };

                var idPhieuDat = await _phieuDatPhong.InsertAndGetIdAsync(newPhieuDat);

                var chiTietPhieuDat = new ChiTietDatPhong
                {
                    TrangThaiPhong = _trangThaiPhong.Select(p=>p.TrangThai1).ToString(),
                    CheckIn = "Từ 14h" + infoBooking.infoBookingDto.NgayDat.ToString(),
                    CheckOut = "Trước 12h" + infoBooking.infoBookingDto.NgayTra.ToString(),
                    SLNguoiLon = infoBooking.infoBookingDto.SlNguoiLon,
                    SLTreEm = infoBooking.infoBookingDto.SlTreEm,
                    SLPhong = infoBooking.infoBookingDto.SlPhong,
                    TienPhong = infoBooking.TongTien,
                    TienPhongQuaHan = 0,
                    ChiPhiHuyPhong = 0,
                    TongTien = 0,
                    PhieuDatPhongId = idPhieuDat,
                    PhongId = infoBooking.phongId,
                };

                await _chiTietDatPhong.InsertAsync(chiTietPhieuDat);

                await CurrentUnitOfWork.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }

    }
}
