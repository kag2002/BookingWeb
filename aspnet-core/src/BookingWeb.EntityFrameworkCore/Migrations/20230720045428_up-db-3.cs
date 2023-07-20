using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingWeb.Migrations
{
    /// <inheritdoc />
    public partial class updb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BwPhieuDatPhong_BwKhachHang_KhachHangId",
                table: "BwPhieuDatPhong");

            migrationBuilder.AlterColumn<int>(
                name: "KhachHangId",
                table: "BwPhieuDatPhong",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AnhDaiDien",
                table: "BwNhanVien",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnhDaiDien",
                table: "BwKhachHang",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BwPhieuDatPhong_BwKhachHang_KhachHangId",
                table: "BwPhieuDatPhong",
                column: "KhachHangId",
                principalTable: "BwKhachHang",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BwPhieuDatPhong_BwKhachHang_KhachHangId",
                table: "BwPhieuDatPhong");

            migrationBuilder.DropColumn(
                name: "AnhDaiDien",
                table: "BwNhanVien");

            migrationBuilder.DropColumn(
                name: "AnhDaiDien",
                table: "BwKhachHang");

            migrationBuilder.AlterColumn<int>(
                name: "KhachHangId",
                table: "BwPhieuDatPhong",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BwPhieuDatPhong_BwKhachHang_KhachHangId",
                table: "BwPhieuDatPhong",
                column: "KhachHangId",
                principalTable: "BwKhachHang",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
