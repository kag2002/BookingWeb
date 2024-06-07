using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using BookingWeb.Authorization.Roles;
using BookingWeb.Authorization.Users;
using BookingWeb.MultiTenancy;
using BookingWeb.DbEntities;

namespace BookingWeb.EntityFrameworkCore
{
    public class BookingWebDbContext : AbpZeroDbContext<Tenant, Role, User, BookingWebDbContext>
    {
        public DbSet<ChiTietDatPhong> BwChiTietDatPhong { get; set; }
        public DbSet<PhieuDaDuyet> BwPhieuDaDuyet{ get; set; }
        public DbSet<PhieuDatPhong> BwPhieuDatPhong { get; set; }

        public DbSet<DiaDiem> BwDiaDiem { get; set; }

        public DbSet<DichVuTienIch> BwDichVuTienIch { get; set; }

        public DbSet<HinhAnh> BwHinhAnh { get; set; }

        public DbSet<HinhThucPhong> BwHinhThucPhong { get; set; }

        public DbSet<KhachHang> BwKhachHang { get; set; }

        public DbSet<LoaiKhachHang> BwLoaiKhachHang { get; set; }

        public DbSet<LoaiPhong> BwLoaiPhong { get; set; }

        public DbSet<NhanVien> BwNhanVien { get; set; }

        public DbSet<NhanXetDanhGia> BwNhanXetDanhGia { get; set; }

        public DbSet<Phong> BwPhong { get; set; }
        
        public DbSet<DonViKinhDoanh> BwDonViKinhDoanh { get; set; }

        public DbSet<ChinhSachChung> BwChinhSachChung { get; set; }

        public DbSet<DichVuTienIchChung> BwDichVuTienIchChung { get; set; }

        public DbSet<TrangThaiPhong> BwTrangThaiPhong { get; set; }

        public DbSet<LienHe> BwLienHe { get; set; }

        public BookingWebDbContext(DbContextOptions<BookingWebDbContext> options)
            : base(options)
        {
        }
    }
}
