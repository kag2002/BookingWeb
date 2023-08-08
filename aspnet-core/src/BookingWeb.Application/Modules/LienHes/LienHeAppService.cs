using Abp.Domain.Repositories;
using BookingWeb.DbEntities;
using BookingWeb.Modules.LienHes.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingWeb.Modules.LienHes
{
    public class LienHeAppService : BookingWebAppServiceBase
    {
        private readonly IRepository<LienHe> _lienHe;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public LienHeAppService(IRepository<LienHe> lienHe, IHttpContextAccessor httpContextAccessor)
        {
            _lienHe = lienHe;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<DanhSachOutputDto>> GetDanhSachLienHe()
        {
            try
            {
                var lst = await _lienHe.GetAllListAsync();

                var dto = lst.Select(e => new DanhSachOutputDto
                {
                    Id = e.Id,
                    HoTen=e.HoTen,
                    Email=e.Email,
                    PhoneNumber=e.PhoneNumber,
                    NoiDung=e.NoiDung,
                    userId=e.CreatorUserId
                }).ToList();
                return dto;
            }
            catch(Exception ex)
            {
                throw ex;
            }
    
        }

        public async Task<List<DanhSachOutputDto>> GetDanhSachLienHeByUserId(int userId)
        {
            try
            {
                if (userId == 0)
                {
                    throw new Exception($"{userId} is invalid !");
                }

                var lst = await _lienHe.GetAllListAsync();

                var dto = lst.Where(p=>p.CreatorUserId == userId).Select(e => new DanhSachOutputDto
                {
                    Id = e.Id,
                    HoTen = e.HoTen,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    NoiDung = e.NoiDung,
                    userId = e.CreatorUserId
                }).ToList();
                return dto;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> ClientSendToMessage(MessageDto input)
        {
            try
            {
                var message = new LienHe
                {
                    HoTen = input.Hoten,
                    Email = input.Email,
                    PhoneNumber = input.PhoneNumber,
                    NoiDung = input.NoiDung
                };

                await _lienHe.InsertAsync(message);

                return true;

            }catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
