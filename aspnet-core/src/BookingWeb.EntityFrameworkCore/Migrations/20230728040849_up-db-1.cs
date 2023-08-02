using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingWeb.Migrations
{
    /// <inheritdoc />
    public partial class updb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThaiPhong",
                table: "BwLoaiPhong");

            migrationBuilder.AddColumn<int>(
                name: "SLPhongTrong",
                table: "BwLoaiPhong",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SLPhongTrong",
                table: "BwLoaiPhong");

            migrationBuilder.AddColumn<string>(
                name: "TrangThaiPhong",
                table: "BwLoaiPhong",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
