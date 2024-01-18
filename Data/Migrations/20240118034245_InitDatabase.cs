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
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsVerified = table.Column<bool>(type: "INTEGER", nullable: false),
                    VerificationToken = table.Column<string>(type: "TEXT", nullable: true)
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
                    Link = table.Column<string>(type: "TEXT", nullable: true),
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
                columns: new[] { "Id", "Email", "IsAdmin", "IsVerified", "Password", "Username", "VerificationToken" },
                values: new object[,]
                {
                    { 1, "hoseinshaemi@gmail.com", true, true, "$2b$10$RP0U1QHbMHM5MuQwMWIlcukscw3hI7uW9n2WsPSIDl6R6n.VcdEdC", "hshaemi", "b5c47c6fd898421da866084b06877b67" },
                    { 2, "amirhosseinfathi@gmail.com", false, true, "$2b$10$aizHepe82n7C2nn844.pjO366zgBrpHMB.FejTfAwB6wXz/K/k/LO", "afathi", "cc9245b3860c4337b2ca38469ce98d79" },
                    { 3, "alinikaein@gmail.com", false, true, "$2b$10$dnlioaOYLoh9r5Z58Qm9NefC0IMru/HDpO6r8OFw.RKxuqdSIgM1K", "anikaein", "5de09702354844de9fd2fe22423ddcb9" },
                    { 4, "mammadmmp@gmail.com", false, true, "$2b$10$qa.z5C92ABVmepPszzatiOzQ2cRrwPzzRU9fg20k5Q59u/uAV8.NS", "mamadmmp", "e9c0b0d1a3f7441f9d075378f4b3fad3" }
                });

            migrationBuilder.InsertData(
                table: "Musics",
                columns: new[] { "Id", "ArtistId", "Link", "Title" },
                values: new object[,]
                {
                    { 1, 1, "abr-biseda-mibarad.mp4", "Abr Biseda Mibarad" },
                    { 2, 1, "tasnif-ghollab.mp4", "Tasnif Ghollab" },
                    { 3, 2, "rahe-meykhane.mp4", "Rahe Meykhane" },
                    { 4, 2, "saghi.mp4", "Saghi" },
                    { 5, 3, "hala-ke-miravi.mp4", "Hala Ke Miravi" },
                    { 6, 3, "dastam-ra-begir.mp4", "Dastam Ra Begir" },
                    { 7, 4, "khatoon.mp4", "Khatoon" },
                    { 8, 4, "gol-va-khak.mp4", "Gol Va Khak" }
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

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
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
