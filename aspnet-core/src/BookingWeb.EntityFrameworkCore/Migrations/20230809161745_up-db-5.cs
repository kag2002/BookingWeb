using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingWeb.Migrations
{
    /// <inheritdoc />
    public partial class updb5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TienPhong",
                table: "BwChiTietDatPhong");

            migrationBuilder.AlterColumn<string>(
                name: "SDT",
                table: "BwPhieuDatPhong",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "YeuCauDacBiet",
                table: "BwPhieuDatPhong",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YeuCauDacBiet",
                table: "BwPhieuDatPhong");

            migrationBuilder.AlterColumn<long>(
                name: "SDT",
                table: "BwPhieuDatPhong",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TienPhong",
                table: "BwChiTietDatPhong",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
