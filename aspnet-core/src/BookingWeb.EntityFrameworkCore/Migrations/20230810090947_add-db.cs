using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingWeb.Migrations
{
    /// <inheritdoc />
    public partial class adddb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BwDiaDiem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDiaDiem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThongTinViTri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenFileAnhDD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BwDiaDiem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BwHinhThucPhong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    TenHinhThuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BwHinhThucPhong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BwLienHe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BwLienHe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BwLoaiKhachHang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    PhanLoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MucGiamGia = table.Column<double>(type: "float", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BwLoaiKhachHang", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BwNhanVien",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<long>(type: "bigint", nullable: false),
                    QueQuan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GioiTinh = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BwNhanVien", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BwPhieuDatPhong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CCCD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayHenTra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatHo = table.Column<int>(type: "int", nullable: false),
                    YeuCauDacBiet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BwPhieuDatPhong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BwTrangThaiPhong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BwTrangThaiPhong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BwDonViKinhDoanh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDonVi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChiChiTiet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GioiThieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaDiemId = table.Column<int>(type: "int", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BwDonViKinhDoanh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BwDonViKinhDoanh_BwDiaDiem_DiaDiemId",
                        column: x => x.DiaDiemId,
                        principalTable: "BwDiaDiem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BwKhachHang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    CCCD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<long>(type: "bigint", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GioiTinh = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiKhachHangId = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BwKhachHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BwKhachHang_BwLoaiKhachHang_LoaiKhachHangId",
                        column: x => x.LoaiKhachHangId,
                        principalTable: "BwLoaiKhachHang",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BwChinhSachChung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    KiemTraThongTin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuaSang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NhanPhong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TraPhong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChinhSachVePhong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChinhSachTreEm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChinhSachVeGiuongPhu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChinhSachVeThuCung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhuongThucThanhToan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonViKinhDoanhId = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BwChinhSachChung", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BwChinhSachChung_BwDonViKinhDoanh_DonViKinhDoanhId",
                        column: x => x.DonViKinhDoanhId,
                        principalTable: "BwDonViKinhDoanh",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BwDichVuTienIchChung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    TenDichVu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChiTiet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DonViKinhDoanhId = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BwDichVuTienIchChung", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BwDichVuTienIchChung_BwDonViKinhDoanh_DonViKinhDoanhId",
                        column: x => x.DonViKinhDoanhId,
                        principalTable: "BwDonViKinhDoanh",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BwLoaiPhong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    TenLoaiPhong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TongSlPhong = table.Column<int>(type: "int", nullable: false),
                    SucChua = table.Column<int>(type: "int", nullable: false),
                    SLPhongTrong = table.Column<int>(type: "int", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TienNghiTrongPhong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiaPhongTheoDem = table.Column<double>(type: "float", nullable: false),
                    GiaGoiDichVuThem = table.Column<double>(type: "float", nullable: false),
                    MienPhiHuyPhong = table.Column<bool>(type: "bit", nullable: false),
                    ChiPhiHuyPhong = table.Column<double>(type: "float", nullable: false),
                    UuDai = table.Column<double>(type: "float", nullable: false),
                    UuDaiDacBiet = table.Column<double>(type: "float", nullable: false),
                    DonViKinhDoanhId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BwLoaiPhong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BwLoaiPhong_BwDonViKinhDoanh_DonViKinhDoanhId",
                        column: x => x.DonViKinhDoanhId,
                        principalTable: "BwDonViKinhDoanh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BwPhong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenFileAnhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LuotDatPhong = table.Column<int>(type: "int", nullable: false),
                    DiemDanhGiaTB = table.Column<double>(type: "float", nullable: false),
                    DanhGiaSaoTb = table.Column<double>(type: "float", nullable: false),
                    DonViKinhDoanhId = table.Column<int>(type: "int", nullable: true),
                    HinhThucPhongId = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BwPhong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BwPhong_BwDonViKinhDoanh_DonViKinhDoanhId",
                        column: x => x.DonViKinhDoanhId,
                        principalTable: "BwDonViKinhDoanh",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BwPhong_BwHinhThucPhong_HinhThucPhongId",
                        column: x => x.HinhThucPhongId,
                        principalTable: "BwHinhThucPhong",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BwDichVuTienIch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    TenDichVu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoaiPhongId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BwDichVuTienIch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BwDichVuTienIch_BwLoaiPhong_LoaiPhongId",
                        column: x => x.LoaiPhongId,
                        principalTable: "BwLoaiPhong",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BwChiTietDatPhong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrangThaiPhongId = table.Column<int>(type: "int", nullable: false),
                    CheckIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckOut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SLNguoiLon = table.Column<int>(type: "int", nullable: false),
                    SLTreEm = table.Column<int>(type: "int", nullable: false),
                    SLPhong = table.Column<int>(type: "int", nullable: false),
                    TienPhongQuaHan = table.Column<double>(type: "float", nullable: false),
                    NgayHuy = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChiPhiHuyPhong = table.Column<double>(type: "float", nullable: false),
                    TongTien = table.Column<double>(type: "float", nullable: false),
                    PhongId = table.Column<int>(type: "int", nullable: false),
                    LoaiPhongId = table.Column<int>(type: "int", nullable: false),
                    PhieuDatPhongId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BwChiTietDatPhong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BwChiTietDatPhong_BwPhieuDatPhong_PhieuDatPhongId",
                        column: x => x.PhieuDatPhongId,
                        principalTable: "BwPhieuDatPhong",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BwChiTietDatPhong_BwPhong_PhongId",
                        column: x => x.PhongId,
                        principalTable: "BwPhong",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BwHinhAnh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenFileAnh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhongId = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BwHinhAnh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BwHinhAnh_BwPhong_PhongId",
                        column: x => x.PhongId,
                        principalTable: "BwPhong",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BwNhanXetDanhGia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NhanXet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiemDanhGia = table.Column<float>(type: "real", nullable: false),
                    DanhGiaSao = table.Column<float>(type: "real", nullable: false),
                    ChiTietDatPhongId = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BwNhanXetDanhGia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BwNhanXetDanhGia_BwChiTietDatPhong_ChiTietDatPhongId",
                        column: x => x.ChiTietDatPhongId,
                        principalTable: "BwChiTietDatPhong",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BwChinhSachChung_DonViKinhDoanhId",
                table: "BwChinhSachChung",
                column: "DonViKinhDoanhId");

            migrationBuilder.CreateIndex(
                name: "IX_BwChiTietDatPhong_PhieuDatPhongId",
                table: "BwChiTietDatPhong",
                column: "PhieuDatPhongId");

            migrationBuilder.CreateIndex(
                name: "IX_BwChiTietDatPhong_PhongId",
                table: "BwChiTietDatPhong",
                column: "PhongId");

            migrationBuilder.CreateIndex(
                name: "IX_BwDichVuTienIch_LoaiPhongId",
                table: "BwDichVuTienIch",
                column: "LoaiPhongId");

            migrationBuilder.CreateIndex(
                name: "IX_BwDichVuTienIchChung_DonViKinhDoanhId",
                table: "BwDichVuTienIchChung",
                column: "DonViKinhDoanhId");

            migrationBuilder.CreateIndex(
                name: "IX_BwDonViKinhDoanh_DiaDiemId",
                table: "BwDonViKinhDoanh",
                column: "DiaDiemId");

            migrationBuilder.CreateIndex(
                name: "IX_BwHinhAnh_PhongId",
                table: "BwHinhAnh",
                column: "PhongId");

            migrationBuilder.CreateIndex(
                name: "IX_BwKhachHang_LoaiKhachHangId",
                table: "BwKhachHang",
                column: "LoaiKhachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_BwLoaiPhong_DonViKinhDoanhId",
                table: "BwLoaiPhong",
                column: "DonViKinhDoanhId");

            migrationBuilder.CreateIndex(
                name: "IX_BwNhanXetDanhGia_ChiTietDatPhongId",
                table: "BwNhanXetDanhGia",
                column: "ChiTietDatPhongId");

            migrationBuilder.CreateIndex(
                name: "IX_BwPhong_DonViKinhDoanhId",
                table: "BwPhong",
                column: "DonViKinhDoanhId");

            migrationBuilder.CreateIndex(
                name: "IX_BwPhong_HinhThucPhongId",
                table: "BwPhong",
                column: "HinhThucPhongId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BwChinhSachChung");

            migrationBuilder.DropTable(
                name: "BwDichVuTienIch");

            migrationBuilder.DropTable(
                name: "BwDichVuTienIchChung");

            migrationBuilder.DropTable(
                name: "BwHinhAnh");

            migrationBuilder.DropTable(
                name: "BwKhachHang");

            migrationBuilder.DropTable(
                name: "BwLienHe");

            migrationBuilder.DropTable(
                name: "BwNhanVien");

            migrationBuilder.DropTable(
                name: "BwNhanXetDanhGia");

            migrationBuilder.DropTable(
                name: "BwTrangThaiPhong");

            migrationBuilder.DropTable(
                name: "BwLoaiPhong");

            migrationBuilder.DropTable(
                name: "BwLoaiKhachHang");

            migrationBuilder.DropTable(
                name: "BwChiTietDatPhong");

            migrationBuilder.DropTable(
                name: "BwPhieuDatPhong");

            migrationBuilder.DropTable(
                name: "BwPhong");

            migrationBuilder.DropTable(
                name: "BwDonViKinhDoanh");

            migrationBuilder.DropTable(
                name: "BwHinhThucPhong");

            migrationBuilder.DropTable(
                name: "BwDiaDiem");
        }
    }
}
