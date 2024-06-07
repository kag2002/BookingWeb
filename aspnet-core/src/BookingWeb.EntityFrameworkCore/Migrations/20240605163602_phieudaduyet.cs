using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingWeb.Migrations
{
    /// <inheritdoc />
    public partial class phieudaduyet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BwPhieuDaDuyet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhieuDatPhongId = table.Column<int>(type: "int", nullable: false),
                    PhongId = table.Column<int>(type: "int", nullable: false),
                    LoaiPhongId = table.Column<int>(type: "int", nullable: false),
                    TrangThaiPhongId = table.Column<int>(type: "int", nullable: false),
                    TenTrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenPhong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SLNguoiLon = table.Column<int>(type: "int", nullable: false),
                    SLTreEm = table.Column<int>(type: "int", nullable: false),
                    SLPhong = table.Column<int>(type: "int", nullable: false),
                    TienPhongQuaHan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NgayHuy = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChiPhiHuyPhong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    table.PrimaryKey("PK_BwPhieuDaDuyet", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BwPhieuDaDuyet");
        }
    }
}
