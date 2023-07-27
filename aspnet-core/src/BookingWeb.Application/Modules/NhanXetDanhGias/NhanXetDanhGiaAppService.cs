using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using BookingWeb.Authorization.Users;
using BookingWeb.DbEntities;
using BookingWeb.Modules.NhanXetDanhGias.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.NhanXetDanhGias
{
    public class NhanXetDanhGiaAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<NhanXetDanhGia> _nhanXetGiaDanh;

        private readonly IRepository<KhachHang> _khachHang;

        private readonly IRepository<ChiTietDatPhong> _chiTietDatPhong;

        private readonly IRepository<PhieuDatPhong> _phieuDatPhong;

        private readonly IHttpContextAccessor _httpContextAccessor;
/*
        private readonly UserManager _abpUser;
*/
        public NhanXetDanhGiaAppService(IRepository<NhanXetDanhGia> nhanXetGiaDanh, IRepository<KhachHang> khachHang, IHttpContextAccessor httpContextAccessor)
        {
            _nhanXetGiaDanh = nhanXetGiaDanh;
            _khachHang = khachHang;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<NhanXetDanhGiaDto>> GetAllList()
        {
            try
            {
                var lstNx = await _nhanXetGiaDanh.GetAllListAsync();
                var lstPd = await _phieuDatPhong.GetAllListAsync();
                var lstKh = await _khachHang.GetAllListAsync();
                var lstCt = await _chiTietDatPhong.GetAllListAsync();

                var result = (from nx in lstNx
                             join ct in lstCt on nx.ChiTietDatPhongId equals ct.Id
                             join pd in lstPd on ct.PhieuDatPhongId equals pd.Id
                             select new NhanXetDanhGiaDto
                             {
                                 NhanXet = nx.NhanXet,
                                 DanhGiaSao = nx.DanhGiaSao,
                                 DiemDanhGia = nx.DiemDanhGia,
                                 ChiTietDatPhongId = ct.Id,
                                 PhongId = ct.PhongId
                             }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return null;
            }
        }





    }
}
