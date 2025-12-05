using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversalBusinessSystem.Data.Migrations
{
    public partial class AddPasswordSalt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add PasswordSalt column if it doesn't exist
            migrationBuilder.Sql(
                @"CREATE TABLE IF NOT EXISTS __EFMigrationsHistory (
                    MigrationId TEXT NOT NULL CONSTRAINT PK___EFMigrationsHistory PRIMARY KEY,
                    ProductVersion TEXT NOT NULL
                );");

            migrationBuilder.Sql(
                @"INSERT OR IGNORE INTO __EFMigrationsHistory (MigrationId, ProductVersion) 
                VALUES ('20251205101200_AddPasswordSalt', '8.0.8');");

            // Add PasswordSalt column to Users table
            migrationBuilder.Sql(
                @"ALTER TABLE Users ADD COLUMN PasswordSalt TEXT NOT NULL DEFAULT '';");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"ALTER TABLE Users DROP COLUMN PasswordSalt;");
        }
    }
}
