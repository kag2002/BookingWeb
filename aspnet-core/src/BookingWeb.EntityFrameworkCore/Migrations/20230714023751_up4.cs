using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingWeb.Migrations
{
    /// <inheritdoc />
    public partial class up4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MienPhiHuyPhong",
                table: "BwPhong");

            migrationBuilder.AddColumn<float>(
                name: "ChiPhiHuyPhong",
                table: "BwLoaiPhong",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "MienPhiHuyPhong",
                table: "BwLoaiPhong",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "TienPhong",
                table: "BwChiTietDatPhong",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChiPhiHuyPhong",
                table: "BwLoaiPhong");

            migrationBuilder.DropColumn(
                name: "MienPhiHuyPhong",
                table: "BwLoaiPhong");

            migrationBuilder.DropColumn(
                name: "TienPhong",
                table: "BwChiTietDatPhong");

            migrationBuilder.AddColumn<int>(
                name: "MienPhiHuyPhong",
                table: "BwPhong",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
