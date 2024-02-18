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

        public ThongKeAppService(IRepository<ChiTietDatPhong> chitietdatphong)
        {
            _chitietdatphong = chitietdatphong;
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
                    var checkInMonth =chiTietDatPhong.CheckIn.Month;
                    var doanhThuDto = doanhThuList[checkInMonth];

                   
                    doanhThuDto.Revenue += chiTietDatPhong.TongTien;
                }


                var lst = new List<double>();
                foreach(var item in doanhThuList)
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
        public async Task<List<double>> GetDoanhThuDiaDiem()
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

                    // Accumulate the revenue for the month
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
                // Handle exceptions appropriately
                throw ex;
            }
        }
    }
}
