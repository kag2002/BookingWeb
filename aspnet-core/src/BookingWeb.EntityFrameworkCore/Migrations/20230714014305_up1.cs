using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingWeb.Migrations
{
    /// <inheritdoc />
    public partial class up1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BwLoaiPhong_BwPhong_PhongId",
                table: "BwLoaiPhong");

            migrationBuilder.DropIndex(
                name: "IX_BwLoaiPhong_PhongId",
                table: "BwLoaiPhong");

            migrationBuilder.DropColumn(
                name: "PhongId",
                table: "BwLoaiPhong");

            migrationBuilder.AddColumn<int>(
                name: "LoaiPhongId",
                table: "BwPhong",
                type: "int",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "PhongId",
                table: "BwLoaiPhong",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BwLoaiPhong_PhongId",
                table: "BwLoaiPhong",
                column: "PhongId");

            migrationBuilder.AddForeignKey(
                name: "FK_BwLoaiPhong_BwPhong_PhongId",
                table: "BwLoaiPhong",
                column: "PhongId",
                principalTable: "BwPhong",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
