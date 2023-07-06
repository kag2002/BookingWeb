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

        public DbSet<ChinhSachQuyDinh> BwChinhSachQuyDinh { get; set; }

        public DbSet<ChiTietDatPhong> BwChiTietDatPhongs { get; set; }

        public DbSet<DatPhong> BwDatPhong { get; set; }

        public DbSet<DiaDiem> BwDiaDiem { get; set; }

        public DbSet<DichVuTienIch> BwDichVuTienIch { get; set; }

        public DbSet<HinhAnh> BwHinhAnh { get; set; }

        public DbSet<HinhThucKinhDoanh> BwHinhThucKinhDoanh { get; set; }

        public DbSet<KhachHang> BwKhachHang { get; set; }

        public DbSet<LoaiKhachHang> BwLoaiKhachHang { get; set; }

        public DbSet<LoaiPhong> BwLoaiPhong { get; set; }

        public DbSet<NhanVien> BwNhanVien { get; set; }

        public DbSet<NhanXetDanhGia> BwNhanXetDanhGia { get; set; }

        public DbSet<Phong> BwPhong { get; set; }

        public DbSet<TaiKhoan> BwTaiKhoan { get; set; }

        public DbSet<TrangThaiPhong> BwTrangThaiPhong { get; set; }

        public BookingWebDbContext(DbContextOptions<BookingWebDbContext> options)
            : base(options)
        {
        }
    }
}
