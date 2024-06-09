using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingWeb.Migrations
{
    /// <inheritdoc />
    public partial class _9_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TongTien",
                table: "BwPhieuDaDuyet",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "TienPhongQuaHan",
                table: "BwPhieuDaDuyet",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "ChiPhiHuyPhong",
                table: "BwPhieuDaDuyet",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_BwPhieuDaDuyet_PhieuDatPhongId",
                table: "BwPhieuDaDuyet",
                column: "PhieuDatPhongId");

            migrationBuilder.AddForeignKey(
                name: "FK_BwPhieuDaDuyet_BwPhieuDatPhong_PhieuDatPhongId",
                table: "BwPhieuDaDuyet",
                column: "PhieuDatPhongId",
                principalTable: "BwPhieuDatPhong",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BwPhieuDaDuyet_BwPhieuDatPhong_PhieuDatPhongId",
                table: "BwPhieuDaDuyet");

            migrationBuilder.DropIndex(
                name: "IX_BwPhieuDaDuyet_PhieuDatPhongId",
                table: "BwPhieuDaDuyet");

            migrationBuilder.AlterColumn<decimal>(
                name: "TongTien",
                table: "BwPhieuDaDuyet",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "TienPhongQuaHan",
                table: "BwPhieuDaDuyet",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "ChiPhiHuyPhong",
                table: "BwPhieuDaDuyet",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
