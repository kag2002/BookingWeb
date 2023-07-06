using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingWeb.Migrations
{
    /// <inheritdoc />
    public partial class addtbtrangthaiphong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_BwKhachHang_BwLoaiKhachHang_LoaiKhachHangId",
                table: "BwKhachHang");

            migrationBuilder.DropForeignKey(
                name: "FK_BwKhachHang_BwTaiKhoan_TaiKhoanId",
                table: "BwKhachHang");

            migrationBuilder.DropForeignKey(
                name: "FK_BwPhong_BwDiaDiem_DiaDiemId",
                table: "BwPhong");

            migrationBuilder.DropForeignKey(
                name: "FK_BwPhong_BwHinhThucKinhDoanh_HinhThucKinhDoanhId",
                table: "BwPhong");

            migrationBuilder.DropForeignKey(
                name: "FK_BwPhong_BwLoaiPhong_LoaiPhongId",
                table: "BwPhong");*/

/*            migrationBuilder.DropColumn(
                name: "TrangThaiPhong",
                table: "BwChiTietDatPhongs");

            migrationBuilder.RenameColumn(
                name: "TrangThaiPhong",
                table: "BwPhong",
                newName: "DiaChiChiTiet");*/

            /*migrationBuilder.AlterColumn<int>(
                name: "LoaiPhongId",
                table: "BwPhong",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HinhThucKinhDoanhId",
                table: "BwPhong",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DiaDiemId",
                table: "BwPhong",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");*/

            migrationBuilder.AddColumn<int>(
                name: "TrangThaiPhongId",
                table: "BwPhong",
                type: "int",
                nullable: false,
                defaultValue: 0);

            /*migrationBuilder.AlterColumn<int>(
                name: "TaiKhoanId",
                table: "BwKhachHang",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LoaiKhachHangId",
                table: "BwKhachHang",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");*/

            migrationBuilder.AddColumn<int>(
                name: "TrangThaiPhongId",
                table: "BwChiTietDatPhongs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BwTrangThaiPhong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),

                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true),

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

            migrationBuilder.CreateIndex(
                name: "IX_BwPhong_TrangThaiPhongId",
                table: "BwPhong",
                column: "TrangThaiPhongId");

            migrationBuilder.CreateIndex(
                name: "IX_BwChiTietDatPhongs_TrangThaiPhongId",
                table: "BwChiTietDatPhongs",
                column: "TrangThaiPhongId");

            /*migrationBuilder.AddForeignKey(
                name: "FK_BwChiTietDatPhongs_BwTrangThaiPhong_TrangThaiPhongId",
                table: "BwChiTietDatPhongs",
                column: "TrangThaiPhongId",
                principalTable: "BwTrangThaiPhong",
                principalColumn: "Id"
               *//* onDelete: ReferentialAction.Cascade*//*);*/

            /*migrationBuilder.AddForeignKey(
                name: "FK_BwKhachHang_BwLoaiKhachHang_LoaiKhachHangId",
                table: "BwKhachHang",
                column: "LoaiKhachHangId",
                principalTable: "BwLoaiKhachHang",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BwKhachHang_BwTaiKhoan_TaiKhoanId",
                table: "BwKhachHang",
                column: "TaiKhoanId",
                principalTable: "BwTaiKhoan",
                principalColumn: "Id");*/

            /*migrationBuilder.AddForeignKey(
                name: "FK_BwPhong_BwDiaDiem_DiaDiemId",
                table: "BwPhong",
                column: "DiaDiemId",
                principalTable: "BwDiaDiem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BwPhong_BwHinhThucKinhDoanh_HinhThucKinhDoanhId",
                table: "BwPhong",
                column: "HinhThucKinhDoanhId",
                principalTable: "BwHinhThucKinhDoanh",
                principalColumn: "Id");*/

          /*  migrationBuilder.AddForeignKey(
                name: "FK_BwPhong_BwLoaiPhong_LoaiPhongId",
                table: "BwPhong",
                column: "LoaiPhongId",
                principalTable: "BwLoaiPhong",
                principalColumn: "Id");*/

            /*migrationBuilder.AddForeignKey(
                name: "FK_BwPhong_BwTrangThaiPhong_TrangThaiPhongId",
                table: "BwPhong",
                column: "TrangThaiPhongId",
                principalTable: "BwTrangThaiPhong",
                principalColumn: "Id"*//*,
                onDelete: ReferentialAction.Cascade*//*);*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_BwChiTietDatPhongs_BwTrangThaiPhong_TrangThaiPhongId",
                table: "BwChiTietDatPhongs");*/

            /*migrationBuilder.DropForeignKey(
                name: "FK_BwKhachHang_BwLoaiKhachHang_LoaiKhachHangId",
                table: "BwKhachHang");*/

            /*migrationBuilder.DropForeignKey(
                name: "FK_BwKhachHang_BwTaiKhoan_TaiKhoanId",
                table: "BwKhachHang");

            migrationBuilder.DropForeignKey(
                name: "FK_BwPhong_BwDiaDiem_DiaDiemId",
                table: "BwPhong");*/

            /*migrationBuilder.DropForeignKey(
                name: "FK_BwPhong_BwHinhThucKinhDoanh_HinhThucKinhDoanhId",
                table: "BwPhong");

            migrationBuilder.DropForeignKey(
                name: "FK_BwPhong_BwLoaiPhong_LoaiPhongId",
                table: "BwPhong");*/

            /*migrationBuilder.DropForeignKey(
                name: "FK_BwPhong_BwTrangThaiPhong_TrangThaiPhongId",
                table: "BwPhong");*/

            migrationBuilder.DropTable(
                name: "BwTrangThaiPhong");

            migrationBuilder.DropIndex(
                name: "IX_BwPhong_TrangThaiPhongId",
                table: "BwPhong");

            migrationBuilder.DropIndex(
                name: "IX_BwChiTietDatPhongs_TrangThaiPhongId",
                table: "BwChiTietDatPhongs");

            migrationBuilder.DropColumn(
                name: "TrangThaiPhongId",
                table: "BwPhong");

            migrationBuilder.DropColumn(
                name: "TrangThaiPhongId",
                table: "BwChiTietDatPhongs");

            /*migrationBuilder.RenameColumn(
                name: "DiaChiChiTiet",
                table: "BwPhong",
                newName: "TrangThaiPhong");*/

            /*migrationBuilder.AlterColumn<int>(
                name: "LoaiPhongId",
                table: "BwPhong",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HinhThucKinhDoanhId",
                table: "BwPhong",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DiaDiemId",
                table: "BwPhong",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TaiKhoanId",
                table: "BwKhachHang",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LoaiKhachHangId",
                table: "BwKhachHang",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);*/

            /*migrationBuilder.AddColumn<string>(
                name: "TrangThaiPhong",
                table: "BwChiTietDatPhongs",
                type: "nvarchar(max)",
                nullable: true);*/

            /*migrationBuilder.AddForeignKey(
                name: "FK_BwKhachHang_BwLoaiKhachHang_LoaiKhachHangId",
                table: "BwKhachHang",
                column: "LoaiKhachHangId",
                principalTable: "BwLoaiKhachHang",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BwKhachHang_BwTaiKhoan_TaiKhoanId",
                table: "BwKhachHang",
                column: "TaiKhoanId",
                principalTable: "BwTaiKhoan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BwPhong_BwDiaDiem_DiaDiemId",
                table: "BwPhong",
                column: "DiaDiemId",
                principalTable: "BwDiaDiem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BwPhong_BwHinhThucKinhDoanh_HinhThucKinhDoanhId",
                table: "BwPhong",
                column: "HinhThucKinhDoanhId",
                principalTable: "BwHinhThucKinhDoanh",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BwPhong_BwLoaiPhong_LoaiPhongId",
                table: "BwPhong",
                column: "LoaiPhongId",
                principalTable: "BwLoaiPhong",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);*/
        }
    }
}
