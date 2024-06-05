using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.ThongKes.Dto;

namespace BookingWeb.Modules.ThongKes
{
    public class ThongKeAppService : ApplicationService
    {
        private readonly IRepository<ChiTietDatPhong> _chitietdatphong;
        private readonly IRepository<LoaiPhong> _loaiPhongRepository;

        public ThongKeAppService(
            IRepository<ChiTietDatPhong> chitietdatphong,
            IRepository<LoaiPhong> loaiPhongRepository)
        {
            _chitietdatphong = chitietdatphong;
            _loaiPhongRepository = loaiPhongRepository;
        }

        public async Task<List<double>> GetDoanhThu12Thang()
        {
            try
            {
                var chiTietDatPhongs = await _chitietdatphong.GetAllListAsync();
                var doanhThuList = new List<DoanhThuDto>();

                for (int i = 1; i <= 12; i++)
                {
                    doanhThuList.Add(new DoanhThuDto { Month = i });
                }

                foreach (var chiTietDatPhong in chiTietDatPhongs)
                {
                    var checkInMonth = chiTietDatPhong.CheckIn.Month;
                    var doanhThuDto = doanhThuList[checkInMonth];
                    doanhThuDto.Revenue += chiTietDatPhong.TongTien;
                }

                var lst = new List<double>();
                foreach (var item in doanhThuList)
                {
                    lst.Add(item.Revenue);
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<double>> GetTiLeLapDayPhongTheoThang()
        {
            try
            {
                var totalRooms = await TongSoPhong();
                var chiTietDatPhongs = await _chitietdatphong.GetAllListAsync();
                var tongSoNgayTrongThang = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

                var tiLeTheoThang = new double[12];

                foreach (var chiTietDatPhong in chiTietDatPhongs)
                {
                    var checkInMonth = chiTietDatPhong.CheckIn.Month - 1;
                    var stayDays = (chiTietDatPhong.CheckOut - chiTietDatPhong.CheckIn).Days;

                    for (var i = 0; i < stayDays; i++)
                    {
                        var thangHienTai = (chiTietDatPhong.CheckIn.AddDays(i)).Month - 1;
                        if (thangHienTai >= 0 && thangHienTai < 12)
                        {
                            tiLeTheoThang[thangHienTai]++;
                        }
                    }
                }

                var listTiLe = new List<double>();
                for (var i = 0; i < 12; i++)
                {
                    var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, i + 1);
                    var tiLeTheoThangRate = (tiLeTheoThang[i] / (totalRooms * daysInMonth)) * 100;
                    listTiLe.Add(tiLeTheoThangRate);
                }

                return listTiLe;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while calculating the occupancy rates.", ex);
            }
        }

        public async Task<int> TongSoPhong()
        {
            try
            {
                var loaiPhongs = await _loaiPhongRepository.GetAllListAsync();
                var totalRooms = loaiPhongs.Sum(lp => lp.TongSlPhong);
                return totalRooms;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      

   
        public async Task<List<LoaiPhongInforDto>> GetRoomCategoryStatistics()
        {
            try
            {
                var loaiPhongs = await _loaiPhongRepository.GetAllListAsync();
                var chiTietDatPhongs = await _chitietdatphong.GetAllListAsync();

                var roomCategoryStatistics = loaiPhongs.Select(loaiPhong => new LoaiPhongInforDto
                {
                    TenLoaiPhong = loaiPhong.TenLoaiPhong,
                    TongSlPhong = loaiPhong.TongSlPhong,
                    SLPhongTrong = loaiPhong.SLPhongTrong,
                    TongSLDat = chiTietDatPhongs.Count(ctdp => ctdp.LoaiPhongId == loaiPhong.Id)
                }).ToList();

                return roomCategoryStatistics;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving room category statistics.", ex);
            }
        }
    }
}
