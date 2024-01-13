using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Spotify.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Musics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    Length = table.Column<int>(type: "INTEGER", nullable: true),
                    ArtistId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Musics_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMusics",
                columns: table => new
                {
                    MusicId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMusics", x => new { x.UserId, x.MusicId });
                    table.ForeignKey(
                        name: "FK_UserMusics_Musics_MusicId",
                        column: x => x.MusicId,
                        principalTable: "Musics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMusics_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Homayoun Shajarian" },
                    { 2, "Mohammadreza Shajarian" },
                    { 3, "Mohammad Motamedi" },
                    { 4, "Keyvan Kalhor" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsAdmin", "Password", "Username" },
                values: new object[,]
                {
                    { 1, "hoseinshaemi@gmail.com", true, "test123", "hshaemi" },
                    { 2, "amirhosseinfathi@gmail.com", false, "123test", "afathi" },
                    { 3, "alinikaein@gmail.com", false, "pass123", "anikaein" },
                    { 4, "mammadmmp@gmail.com", false, "123pass", "mamadmmp" }
                });

            migrationBuilder.InsertData(
                table: "Musics",
                columns: new[] { "Id", "ArtistId", "FileName", "Length", "Title" },
                values: new object[,]
                {
                    { 1, 1, "abr-biseda-mibarad.mp4", null, "Abr Biseda Mibarad" },
                    { 2, 1, "tasnif-ghollab.mp4", null, "Tasnif Ghollab" },
                    { 3, 2, "rahe-meykhane.mp4", null, "Rahe Meykhane" },
                    { 4, 2, "saghi.mp4", null, "Saghi" },
                    { 5, 3, "hala-ke-miravi.mp4", null, "Hala Ke Miravi" },
                    { 6, 3, "dastam-ra-begir.mp4", null, "Dastam Ra Begir" },
                    { 7, 4, "khatoon.mp4", null, "Khatoon" },
                    { 8, 4, "gol-va-khak.mp4", null, "Gol Va Khak" }
                });

            migrationBuilder.InsertData(
                table: "UserMusics",
                columns: new[] { "MusicId", "UserId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 3, 1 },
                    { 3, 2 },
                    { 4, 3 },
                    { 5, 3 },
                    { 1, 4 },
                    { 6, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Musics_ArtistId",
                table: "Musics",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMusics_MusicId",
                table: "UserMusics",
                column: "MusicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMusics");

            migrationBuilder.DropTable(
                name: "Musics");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Artists");
        }
    }
}
