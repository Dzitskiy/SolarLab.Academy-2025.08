using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Articles.Hosts.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class Fix_DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Articles_CreatedAt_Id",
                table: "Articles",
                columns: new[] { "CreatedAt", "Id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Articles_CreatedAt_Id",
                table: "Articles");
        }
    }
}
