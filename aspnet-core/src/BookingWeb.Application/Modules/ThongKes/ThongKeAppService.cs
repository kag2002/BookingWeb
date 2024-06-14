using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using BookingWeb.Authorization.Users;
using BookingWeb.DbEntities;
using BookingWeb.Modules.ThongKes.Dto;
using Microsoft.EntityFrameworkCore;

namespace BookingWeb.Modules.ThongKes
{
    public class ThongKeAppService : ApplicationService
    {
        private readonly IRepository<PhieuDaDuyet> _phieuDaDuyetRepository;
        private readonly IRepository<LoaiPhong> _loaiPhongRepository;
        private readonly IRepository<User, long> _userRepository;

        public ThongKeAppService(
            IRepository<PhieuDaDuyet> phieuDaDuyetRepository,
            IRepository<LoaiPhong> loaiPhongRepository,
            IRepository<User, long> userRepository)
        {
            _phieuDaDuyetRepository = phieuDaDuyetRepository;
            _loaiPhongRepository = loaiPhongRepository;
            _userRepository = userRepository;
        }

        public async Task<List<double>> GetDoanhThu12Thang()
        {
            try
            {
                var phieuDaDuyets = await _phieuDaDuyetRepository.GetAllListAsync();
                var doanhThuList = new List<DoanhThuDto>();

                for (int i = 1; i <= 12; i++)
                {
                    doanhThuList.Add(new DoanhThuDto { Month = i });
                }

                foreach (var phieuDaDuyet in phieuDaDuyets)
                {
                    var checkInMonth = phieuDaDuyet.NgayBatDau.Month - 1; // Adjusting month to 0-based index
                    doanhThuList[checkInMonth].Revenue += phieuDaDuyet.TongTien;
                }

                return doanhThuList.Select(dto => dto.Revenue).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while calculating the monthly revenue.", ex);
            }
        }

        public async Task<List<double>> GetTiLeLapDayPhongTheoThang()
        {
            try
            {
                var totalRooms = await TongSoPhong();
                var phieuDaDuyets = await _phieuDaDuyetRepository.GetAllListAsync();
                var tiLeTheoThang = new double[12];

                foreach (var phieuDaDuyet in phieuDaDuyets)
                {
                    // Ensure NgayBatDau (NgayHenTra) are within the same year
                    var ngayBatDauYear = phieuDaDuyet.NgayBatDau.Year;
                    var ngayHenTraYear = phieuDaDuyet.NgayHenTra.Year;

                    if (ngayBatDauYear != ngayHenTraYear)
                    {
                        // Handle bookings that span across years if necessary
                        // This is a simplified approach assuming no bookings span more than 1 year
                        continue;
                    }

                    // Calculate occupancy rate per day
                    var stayDays = (phieuDaDuyet.NgayHenTra - phieuDaDuyet.NgayBatDau).Days;

                    for (var i = 0; i < stayDays; i++)
                    {
                        var ngayBatDauDay = phieuDaDuyet.NgayBatDau.AddDays(i);
                        var thangHienTai = ngayBatDauDay.Month - 1;

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
                throw new Exception("An error occurred while calculating the total number of rooms.", ex);
            }
        }

        public async Task<List<LoaiPhongInforDto>> GetRoomCategoryStatistics()
        {
            try
            {
                var loaiPhongs = await _loaiPhongRepository.GetAllListAsync();
                var phieuDaDuyets = await _phieuDaDuyetRepository.GetAllListAsync();

                var roomCategoryStatistics = loaiPhongs.Select(loaiPhong => new LoaiPhongInforDto
                {
                    TenLoaiPhong = loaiPhong.TenLoaiPhong,
                    TongSlPhong = loaiPhong.TongSlPhong,
                    SLPhongTrong = loaiPhong.SLPhongTrong,
                    TongSLDat = phieuDaDuyets.Count(pdd => pdd.LoaiPhongId == loaiPhong.Id)
                }).ToList();

                return roomCategoryStatistics;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving room category statistics.", ex);
            }
        }

        public async Task<double> GetTotalRevenue()
        {
            try
            {
                var phieuDaDuyets = await _phieuDaDuyetRepository.GetAllListAsync();
                var totalRevenue = phieuDaDuyets.Sum(pdd => pdd.TongTien);
                return totalRevenue;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while calculating the total revenue.", ex);
            }
        }

        public async Task<int> CountTotalAccounts()
        {
            try
            {
                var totalAccounts = await _userRepository.CountAsync();
                return totalAccounts;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while counting the total accounts.", ex);
            }
        }

        public async Task<int> CountUniqueCustomersByCCCD()
        {
            try
            {
                var uniqueCustomers = await _phieuDaDuyetRepository.GetAll()
                    .Select(p => p.CCCD)
                    .Distinct()
                    .CountAsync();
                return uniqueCustomers;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while counting unique customers by CCCD.", ex);
            }
        }
    }
}
