using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace real_estate_app.Migrations
{
    /// <inheritdoc />
    public partial class initial_create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Saticilar",
                columns: table => new
                {
                    SaticiId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "text", nullable: false),
                    Soyad = table.Column<string>(type: "text", nullable: false),
                    Telefon = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saticilar", x => x.SaticiId);
                });

            migrationBuilder.CreateTable(
                name: "Emlaklar",
                columns: table => new
                {
                    EmlakId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Adres = table.Column<string>(type: "text", nullable: false),
                    Sehir = table.Column<string>(type: "text", nullable: false),
                    Ilce = table.Column<string>(type: "text", nullable: false),
                    OdaSayisi = table.Column<int>(type: "integer", nullable: false),
                    MetreKare = table.Column<int>(type: "integer", nullable: false),
                    Fiyat = table.Column<int>(type: "integer", nullable: false),
                    SatildiMi = table.Column<bool>(type: "boolean", nullable: false),
                    SaticiId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emlaklar", x => x.EmlakId);
                    table.ForeignKey(
                        name: "FK_Emlaklar_Saticilar_SaticiId",
                        column: x => x.SaticiId,
                        principalTable: "Saticilar",
                        principalColumn: "SaticiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emlaklar_SaticiId",
                table: "Emlaklar",
                column: "SaticiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emlaklar");

            migrationBuilder.DropTable(
                name: "Saticilar");
        }
    }
}
