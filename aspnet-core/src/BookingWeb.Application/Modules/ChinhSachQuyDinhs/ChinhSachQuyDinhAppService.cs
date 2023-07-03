using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.ChinhSachQuyDinhs.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.ChinhSachQuyDinhs
{
    public class ChinhSachQuyDinhAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<ChinhSachQuyDinh> _repositoryCs;

        private readonly IRepository<HinhThucKinhDoanh> _repositoryHt;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChinhSachQuyDinhAppService(IRepository<ChinhSachQuyDinh> repositoryCs, IRepository<HinhThucKinhDoanh> repositoryHt, IHttpContextAccessor httpContextAccessor)
        {
            _repositoryCs = repositoryCs;
            _repositoryHt = repositoryHt;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> AddItem(ChinhSachQuyDinhAddDto input)
        {
            try
            {
                var check = _repositoryHt.FirstOrDefaultAsync(p=>p.Equals(input.HinhThucKinhDoanhId));
                if(check == null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong ton tai hinh thuc kinh doanh voi id = {input.HinhThucKinhDoanhId}");
                    return false;
                }

                var csqd = new ChinhSachQuyDinh
                {
                    QuyDinhVeDatPhong = input.QuyDinhVeDatPhong,
                    QuyDinhVeThuCung = input.QuyDinhVeThuCung,
                    QuyDinhVeTreEm = input.QuyDinhVeTreEm,
                    HinhThucKinhDoanhId = input.HinhThucKinhDoanhId
                };

                await _repositoryCs.InsertAsync(csqd);
                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }


    }
}
