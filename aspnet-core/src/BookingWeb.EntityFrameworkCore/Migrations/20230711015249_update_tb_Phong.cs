using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingWeb.Migrations
{
    /// <inheritdoc />
    public partial class update_tb_Phong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BwPhong_BwHinhThucKinhDoanh_HinhThucPhongId",
                table: "BwPhong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BwHinhThucKinhDoanh",
                table: "BwHinhThucKinhDoanh");

            migrationBuilder.RenameTable(
                name: "BwHinhThucKinhDoanh",
                newName: "BwHinhThucPhong");

            migrationBuilder.RenameColumn(
                name: "NgayDat",
                table: "BwPhieuDatPhong",
                newName: "NgayBatDau");

            migrationBuilder.AddColumn<float>(
                name: "DanhGiaSaoTb",
                table: "BwPhong",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "DiemDanhGiaTB",
                table: "BwPhong",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "GioiTinh",
                table: "BwNhanVien",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BwHinhThucPhong",
                table: "BwHinhThucPhong",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BwPhong_BwHinhThucPhong_HinhThucPhongId",
                table: "BwPhong",
                column: "HinhThucPhongId",
                principalTable: "BwHinhThucPhong",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BwPhong_BwHinhThucPhong_HinhThucPhongId",
                table: "BwPhong");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BwHinhThucPhong",
                table: "BwHinhThucPhong");

            migrationBuilder.DropColumn(
                name: "DanhGiaSaoTb",
                table: "BwPhong");

            migrationBuilder.DropColumn(
                name: "DiemDanhGiaTB",
                table: "BwPhong");

            migrationBuilder.DropColumn(
                name: "GioiTinh",
                table: "BwNhanVien");

            migrationBuilder.RenameTable(
                name: "BwHinhThucPhong",
                newName: "BwHinhThucKinhDoanh");

            migrationBuilder.RenameColumn(
                name: "NgayBatDau",
                table: "BwPhieuDatPhong",
                newName: "NgayDat");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BwHinhThucKinhDoanh",
                table: "BwHinhThucKinhDoanh",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BwPhong_BwHinhThucKinhDoanh_HinhThucPhongId",
                table: "BwPhong",
                column: "HinhThucPhongId",
                principalTable: "BwHinhThucKinhDoanh",
                principalColumn: "Id");
        }
    }
}
