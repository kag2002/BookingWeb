using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingWeb.Migrations
{
    /// <inheritdoc />
    public partial class addDbBookingWeb : Migration
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
                    ThongTinViTriDiaLy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaDangXungQuanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "BwHinhThucKinhDoanh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHinhThuc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDonViKinhDoanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChiChiTiet = table.Column<string>(type: "nvarchar(max)", nullable: true),

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
                    table.PrimaryKey("PK_BwHinhThucKinhDoanh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BwLoaiKhachHang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MucGiamGia = table.Column<float>(type: "real", nullable: false),

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
                name: "BwLoaiPhong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoaiPhong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SucChua = table.Column<int>(type: "int", nullable: false),
                    TienNghiPhong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiaPhongTheoDem = table.Column<float>(type: "real", nullable: false),
                    GiaGoiDichVuThem = table.Column<float>(type: "real", nullable: false),
                    UuDai = table.Column<float>(type: "real", nullable: false),

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
                    table.PrimaryKey("PK_BwLoaiPhong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BwTaiKhoan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhanLoai = table.Column<int>(type: "int", nullable: false),

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
                    table.PrimaryKey("PK_BwTaiKhoan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BwChinhSachQuyDinh",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuyDinhVeThuCung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuyDinhVeTreEm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuyDinhVeDatPhong = table.Column<string>(type: "nvarchar(max)", nullable: true),

                    HinhThucKinhDoanhId = table.Column<int>(type: "int", nullable: false),

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
                    table.PrimaryKey("PK_BwChinhSachQuyDinh", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BwChinhSachQuyDinh_BwHinhThucKinhDoanh_HinhThucKinhDoanhId",
                        column: x => x.HinhThucKinhDoanhId,
                        principalTable: "BwHinhThucKinhDoanh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BwDichVuTienIch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDichVuTienIch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),

                    LoaiPhongId = table.Column<int>(type: "int", nullable: false),

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
                    table.PrimaryKey("PK_BwDichVuTienIch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BwDichVuTienIch_BwLoaiPhong_LoaiPhongId",
                        column: x => x.LoaiPhongId,
                        principalTable: "BwLoaiPhong",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BwPhong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThaiPhong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenFileAnhDaiDien = table.Column<string>(type: "nvarchar(max)", nullable: true),

                    DiaDiemId = table.Column<int>(type: "int", nullable: false),
                    LoaiPhongId = table.Column<int>(type: "int", nullable: false),
                    HinhThucKinhDoanhId = table.Column<int>(type: "int", nullable: false),

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
                    table.PrimaryKey("PK_BwPhong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BwPhong_BwDiaDiem_DiaDiemId",
                        column: x => x.DiaDiemId,
                        principalTable: "BwDiaDiem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BwPhong_BwHinhThucKinhDoanh_HinhThucKinhDoanhId",
                        column: x => x.HinhThucKinhDoanhId,
                        principalTable: "BwHinhThucKinhDoanh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BwPhong_BwLoaiPhong_LoaiPhongId",
                        column: x => x.LoaiPhongId,
                        principalTable: "BwLoaiPhong",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BwKhachHang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CCCD = table.Column<long>(type: "bigint", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),

                    LoaiKhachHangId = table.Column<int>(type: "int", nullable: false),
                    TaiKhoanId = table.Column<int>(type: "int", nullable: false),

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
                    table.PrimaryKey("PK_BwKhachHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BwKhachHang_BwLoaiKhachHang_LoaiKhachHangId",
                        column: x => x.LoaiKhachHangId,
                        principalTable: "BwLoaiKhachHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BwKhachHang_BwTaiKhoan_TaiKhoanId",
                        column: x => x.TaiKhoanId,
                        principalTable: "BwTaiKhoan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BwNhanVien",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<long>(type: "bigint", nullable: false),
                    Que = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),

                    TaiKhoanId = table.Column<int>(type: "int", nullable: false),

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
                    table.PrimaryKey("PK_BwNhanVien", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BwNhanVien_BwTaiKhoan_TaiKhoanId",
                        column: x => x.TaiKhoanId,
                        principalTable: "BwTaiKhoan",
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
                    ViTri = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "BwDatPhong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayDatDuKien = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTraDuKien = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KhachHangId = table.Column<int>(type: "int", nullable: false),
                    NhanVienId = table.Column<int>(type: "int", nullable: false),

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
                    table.PrimaryKey("PK_BwDatPhong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BwDatPhong_BwKhachHang_KhachHangId",
                        column: x => x.KhachHangId,
                        principalTable: "BwKhachHang",
                        principalColumn: "Id");
                        //onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BwDatPhong_BwNhanVien_NhanVienId",
                        column: x => x.NhanVienId,
                        principalTable: "BwNhanVien",
                        principalColumn: "Id");
                        //onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BwChiTietDatPhongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrangThaiPhong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SLNguoiLon = table.Column<int>(type: "int", nullable: false),
                    SLTreEm = table.Column<int>(type: "int", nullable: false),
                    TienPhongQuaHan = table.Column<float>(type: "real", nullable: false),
                    ChiPhiHuyPhong = table.Column<float>(type: "real", nullable: false),
                    NgayHuy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TongTien = table.Column<float>(type: "real", nullable: false),

                    PhongId = table.Column<int>(type: "int", nullable: false),
                    DatPhongId = table.Column<int>(type: "int", nullable: false),

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
                    table.PrimaryKey("PK_BwChiTietDatPhongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BwChiTietDatPhongs_BwDatPhong_DatPhongId",
                        column: x => x.DatPhongId,
                        principalTable: "BwDatPhong",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BwChiTietDatPhongs_BwPhong_PhongId",
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
                    DiemDanhGia = table.Column<float>(type: "real", nullable: false),
                    DanhGiaSao = table.Column<float>(type: "real", nullable: false),
                    NhanXet = table.Column<string>(type: "nvarchar(max)", nullable: true),

                    DichVuTienIchId = table.Column<int>(type: "int", nullable: false),
                    KhachHangId = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_BwNhanXetDanhGia_BwChiTietDatPhongs_ChiTietDatPhongId",
                        column: x => x.ChiTietDatPhongId,
                        principalTable: "BwChiTietDatPhongs",
                        principalColumn: "Id");
                    //onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BwNhanXetDanhGia_BwDichVuTienIch_DichVuTienIchId",
                        column: x => x.DichVuTienIchId,
                        principalTable: "BwDichVuTienIch",
                        principalColumn: "Id");
                    //onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BwNhanXetDanhGia_BwKhachHang_KhachHangId",
                        column: x => x.KhachHangId,
                        principalTable: "BwKhachHang",
                        principalColumn: "Id");
                        //onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BwChinhSachQuyDinh_HinhThucKinhDoanhId",
                table: "BwChinhSachQuyDinh",
                column: "HinhThucKinhDoanhId");

            migrationBuilder.CreateIndex(
                name: "IX_BwChiTietDatPhongs_DatPhongId",
                table: "BwChiTietDatPhongs",
                column: "DatPhongId");

            migrationBuilder.CreateIndex(
                name: "IX_BwChiTietDatPhongs_PhongId",
                table: "BwChiTietDatPhongs",
                column: "PhongId");

            migrationBuilder.CreateIndex(
                name: "IX_BwDatPhong_KhachHangId",
                table: "BwDatPhong",
                column: "KhachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_BwDatPhong_NhanVienId",
                table: "BwDatPhong",
                column: "NhanVienId");

            migrationBuilder.CreateIndex(
                name: "IX_BwDichVuTienIch_LoaiPhongId",
                table: "BwDichVuTienIch",
                column: "LoaiPhongId");

            migrationBuilder.CreateIndex(
                name: "IX_BwHinhAnh_PhongId",
                table: "BwHinhAnh",
                column: "PhongId");

            migrationBuilder.CreateIndex(
                name: "IX_BwKhachHang_LoaiKhachHangId",
                table: "BwKhachHang",
                column: "LoaiKhachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_BwKhachHang_TaiKhoanId",
                table: "BwKhachHang",
                column: "TaiKhoanId");

            migrationBuilder.CreateIndex(
                name: "IX_BwNhanVien_TaiKhoanId",
                table: "BwNhanVien",
                column: "TaiKhoanId");

            migrationBuilder.CreateIndex(
                name: "IX_BwNhanXetDanhGia_ChiTietDatPhongId",
                table: "BwNhanXetDanhGia",
                column: "ChiTietDatPhongId");

            migrationBuilder.CreateIndex(
                name: "IX_BwNhanXetDanhGia_DichVuTienIchId",
                table: "BwNhanXetDanhGia",
                column: "DichVuTienIchId");

            migrationBuilder.CreateIndex(
                name: "IX_BwNhanXetDanhGia_KhachHangId",
                table: "BwNhanXetDanhGia",
                column: "KhachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_BwPhong_DiaDiemId",
                table: "BwPhong",
                column: "DiaDiemId");

            migrationBuilder.CreateIndex(
                name: "IX_BwPhong_HinhThucKinhDoanhId",
                table: "BwPhong",
                column: "HinhThucKinhDoanhId");

            migrationBuilder.CreateIndex(
                name: "IX_BwPhong_LoaiPhongId",
                table: "BwPhong",
                column: "LoaiPhongId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BwChinhSachQuyDinh");

            migrationBuilder.DropTable(
                name: "BwHinhAnh");

            migrationBuilder.DropTable(
                name: "BwNhanXetDanhGia");

            migrationBuilder.DropTable(
                name: "BwChiTietDatPhongs");

            migrationBuilder.DropTable(
                name: "BwDichVuTienIch");

            migrationBuilder.DropTable(
                name: "BwDatPhong");

            migrationBuilder.DropTable(
                name: "BwPhong");

            migrationBuilder.DropTable(
                name: "BwKhachHang");

            migrationBuilder.DropTable(
                name: "BwNhanVien");

            migrationBuilder.DropTable(
                name: "BwDiaDiem");

            migrationBuilder.DropTable(
                name: "BwHinhThucKinhDoanh");

            migrationBuilder.DropTable(
                name: "BwLoaiPhong");

            migrationBuilder.DropTable(
                name: "BwLoaiKhachHang");

            migrationBuilder.DropTable(
                name: "BwTaiKhoan");
        }
    }
}
