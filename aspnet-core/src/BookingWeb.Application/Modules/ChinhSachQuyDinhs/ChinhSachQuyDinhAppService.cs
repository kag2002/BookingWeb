using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.ChinhSachQuyDinhs.Dto;
using BookingWeb.Modules.HinhThucKinhDoanhs.Dto;
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

        public async Task<List<ChinhSachQuyDinhFullDto>> GetAllPolicies()
        {
            try
            {
                var lst = await _repositoryCs.GetAllListAsync();

                var dtoList = lst.Select(entity => new ChinhSachQuyDinhFullDto
                {
                    Id = entity.Id,
                    QuyDinhVeDatPhong = entity.QuyDinhVeDatPhong,
                    QuyDinhVeTreEm = entity.QuyDinhVeTreEm,
                    QuyDinhVeThuCung = entity.QuyDinhVeThuCung,
                    HinhThucKinhDoanhId = entity.HinhThucKinhDoanhId

                }).ToList();

                return dtoList;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"erorr : {ex.Message}");
                return null;
            }

        }


        public async Task<bool> AddNewPolicy(ChinhSachQuyDinhDto input)
        {
            try
            {
                var check = await _repositoryHt.FirstOrDefaultAsync(p=>p.Id == input.HinhThucKinhDoanhId);
                if(check == null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong ton tai hinh thuc kinh doanh voi id = {input.HinhThucKinhDoanhId}");
                    return false;
                }
                else
                {
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
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdatePolicy(ChinhSachQuyDinhFullDto input)
        {
            try
            {
                var item = await _repositoryCs.FirstOrDefaultAsync(p => p.Id == input.Id);

                if(item == null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong ton tai Chinh sach quy dinh voi id = {input.Id}");
                    return false;
                }
                else
                {
                    item.QuyDinhVeDatPhong = input.QuyDinhVeDatPhong;
                    item.QuyDinhVeThuCung = input.QuyDinhVeThuCung;
                    item.QuyDinhVeTreEm = input.QuyDinhVeTreEm;
                    item.HinhThucKinhDoanhId = input.HinhThucKinhDoanhId;

                    await _repositoryCs.UpdateAsync(item);
                    return true;

                }

            }
            catch(Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeletePolicy(int id)
        {
            try
            {
                var item = await _repositoryCs.FirstOrDefaultAsync(p => p.Id == id);
                if (item == null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync("Product not found");
                    return false;
                }

                await _repositoryCs.DeleteAsync(item);
                await _httpContextAccessor.HttpContext.Response.WriteAsync("Successfully Deleted !");
                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"Internal server error: {ex.Message}");
                return false;
            }
        }


    }
}
