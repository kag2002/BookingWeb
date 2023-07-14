using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingWeb.Migrations
{
    /// <inheritdoc />
    public partial class up2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BwPhong_BwLoaiPhong_LoaiPhongId",
                table: "BwPhong");

            migrationBuilder.DropIndex(
                name: "IX_BwPhong_LoaiPhongId",
                table: "BwPhong");

            migrationBuilder.DropColumn(
                name: "LoaiPhongId",
                table: "BwPhong");

            migrationBuilder.DropColumn(
                name: "TrangThaiPhong",
                table: "BwPhong");

            migrationBuilder.AddColumn<int>(
                name: "DonViKinhDoanhId",
                table: "BwLoaiPhong",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrangThaiPhong",
                table: "BwLoaiPhong",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BwLoaiPhong_DonViKinhDoanhId",
                table: "BwLoaiPhong",
                column: "DonViKinhDoanhId");

            migrationBuilder.AddForeignKey(
                name: "FK_BwLoaiPhong_BwDonViKinhDoanh_DonViKinhDoanhId",
                table: "BwLoaiPhong",
                column: "DonViKinhDoanhId",
                principalTable: "BwDonViKinhDoanh",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BwLoaiPhong_BwDonViKinhDoanh_DonViKinhDoanhId",
                table: "BwLoaiPhong");

            migrationBuilder.DropIndex(
                name: "IX_BwLoaiPhong_DonViKinhDoanhId",
                table: "BwLoaiPhong");

            migrationBuilder.DropColumn(
                name: "DonViKinhDoanhId",
                table: "BwLoaiPhong");

            migrationBuilder.DropColumn(
                name: "TrangThaiPhong",
                table: "BwLoaiPhong");

            migrationBuilder.AddColumn<int>(
                name: "LoaiPhongId",
                table: "BwPhong",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrangThaiPhong",
                table: "BwPhong",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BwPhong_LoaiPhongId",
                table: "BwPhong",
                column: "LoaiPhongId");

            migrationBuilder.AddForeignKey(
                name: "FK_BwPhong_BwLoaiPhong_LoaiPhongId",
                table: "BwPhong",
                column: "LoaiPhongId",
                principalTable: "BwLoaiPhong",
                principalColumn: "Id");
        }
    }
}
