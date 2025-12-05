using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemSoftware.WebAPI.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Check if the table exists, if not create it
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
                BEGIN
                    CREATE TABLE [Users] (
                        [Id] int NOT NULL IDENTITY,
                        [Username] nvarchar(100) NOT NULL,
                        [Email] nvarchar(100) NOT NULL,
                        [PasswordHash] nvarchar(255) NOT NULL,
                        [PasswordSalt] nvarchar(255) NOT NULL,
                        [FirstName] nvarchar(100) NULL,
                        [LastName] nvarchar(100) NULL,
                        [IsActive] bit NOT NULL,
                        [IsEmailVerified] bit NOT NULL,
                        [LastLoginAt] datetime2 NULL,
                        [FailedLoginAttempts] int NOT NULL,
                        [LockedUntil] datetime2 NULL,
                        [CreatedAt] datetime2 NOT NULL DEFAULT (GETUTCDATE()),
                        [CreatedBy] nvarchar(max) NULL,
                        [LastModified] datetime2 NULL,
                        [LastModifiedBy] nvarchar(max) NULL,
                        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
                    );
                END
            ");

            // Add indexes if they don't exist
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Users_Email' AND object_id = OBJECT_ID('Users'))
                BEGIN
                    CREATE INDEX [IX_Users_Email] ON [Users]([Email]);
                END
            ");

            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Users_Username' AND object_id = OBJECT_ID('Users'))
                BEGIN
                    CREATE INDEX [IX_Users_Username] ON [Users]([Username]);
                END
            ");

            // Add unique constraints if they don't exist
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.key_constraints WHERE type = 'UQ' AND parent_object_id = OBJECT_ID('Users') AND name = 'UQ_Users_Email')
                BEGIN
                    ALTER TABLE [Users] ADD CONSTRAINT [UQ_Users_Email] UNIQUE ([Email]);
                END
            ");

            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.key_constraints WHERE type = 'UQ' AND parent_object_id = OBJECT_ID('Users') AND name = 'UQ_Users_Username')
                BEGIN
                    ALTER TABLE [Users] ADD CONSTRAINT [UQ_Users_Username] UNIQUE ([Username]);
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
