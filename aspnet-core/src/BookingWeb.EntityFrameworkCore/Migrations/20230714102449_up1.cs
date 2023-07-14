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
            migrationBuilder.AlterColumn<bool>(
                name: "MienPhiHuyPhong",
                table: "BwLoaiPhong",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MienPhiHuyPhong",
                table: "BwLoaiPhong",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
