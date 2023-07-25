using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.DiaDiems.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingWeb.Modules.DiaDiems
{
    public class DiaDiemAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<DiaDiem> _diaDiem;

        private readonly IRepository<DonViKinhDoanh> _donViKinhDoanh;

        private readonly IRepository<Phong> _phong;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public DiaDiemAppService(IRepository<DiaDiem> diaDiem, IRepository<DonViKinhDoanh> donViKinhDoanh, IRepository<Phong> phong, IHttpContextAccessor httpContextAccessor)
        {
            _diaDiem = diaDiem;
            _donViKinhDoanh = donViKinhDoanh;
            _phong = phong;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<DiaDiemFullDto>> GetAllLocations()
        {
            try
            {
                var lst = await _diaDiem.GetAllListAsync();

                var dtoLst = lst.Select(entity => new DiaDiemFullDto
                {
                    Id = entity.Id,
                    TenDiaDiem = entity.TenDiaDiem,
                    ThongTinViTri = entity.ThongTinViTri,
                    TenFileAnhDD = entity.TenFileAnhDD
                }).ToList();

                return dtoLst;

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AddNewLocation(DiaDiemDto input)
        {
            try
            {
                var item = new DiaDiem
                {
                    TenDiaDiem = input.TenDiaDiem,
                    ThongTinViTri = input.ThongTinViTri,
                };

                await _diaDiem.InsertAsync(item);
                return true;
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateLocation(DiaDiemFullDto input)
        {
            try
            {
                var item = await _diaDiem.FirstOrDefaultAsync(p => p.Id == input.Id);

                if (item == null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong ton tai dia danh voi id = {input.Id}");
                    return false;
                }
                else
                {
                    item.TenDiaDiem = input.TenDiaDiem;
                    item.ThongTinViTri = input.ThongTinViTri;
                    item.TenFileAnhDD = input.TenFileAnhDD;

                    await _diaDiem.UpdateAsync(item);
                    return true;
                }
            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error : {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteLocation(int id)
        {
            try
            {
                var checkDD = await _diaDiem.FirstOrDefaultAsync(p=>p.Id==id);
                if (checkDD == null)
                {
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"khong co dia diem voi id = {id}");
                    return false;
                }
                else
                {
                    var DVKD = await _donViKinhDoanh.GetAllListAsync();

                    var checkDvkd = DVKD.Where(p=>p.DiaDiemId == checkDD.Id).ToList();

                    if (checkDvkd.Any())
                    {
                        foreach(var i in checkDvkd)
                        {
                            i.DiaDiemId = null;
                        }
                    }

                    await _diaDiem.DeleteAsync(checkDD);
                    await _httpContextAccessor.HttpContext.Response.WriteAsync($"da xoa dia diem {checkDD}");
                    return true;
                }

            }
            catch (Exception ex)
            {
                await _httpContextAccessor.HttpContext.Response.WriteAsync($"error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UploadImage(int id,IFormFile imageFile, string path)
        {
            try
            {
                if(imageFile != null & imageFile.Length > 0)
                {
                    var fileName = Path.GetFileName(imageFile.Name);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), path, fileName);


                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    var diaDiem = await _diaDiem.FirstOrDefaultAsync(p=>p.Id == id);

                    if (diaDiem != null)
                    {
                        diaDiem.TenFileAnhDD = fileName.ToString();
                        await _diaDiem.UpdateAsync(diaDiem);
                        return true;
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


    }
}
