using Microsoft.Data.Sqlite;
using System;
using System.IO;

public class DatabaseFixer
{
    public static void AddMissingColumns()
    {
        var dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "UniversalBusinessSystem",
            "database",
            "UniversalBusinessSystem.db");
        
        var connectionString = $"Data Source={dbPath}";
        
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        
        Console.WriteLine($"Database path: {dbPath}");
        
        // Check if PasswordSalt column exists
        var checkQuery = "PRAGMA table_info(Users);";
        using var checkCommand = new SqliteCommand(checkQuery, connection);
        using var reader = checkCommand.ExecuteReader();
        
        bool hasPasswordSalt = false;
        bool hasPasswordHash = false;
        bool hasOrganizationId = false;
        bool hasRoleId = false;
        
        while (reader.Read())
        {
            var columnName = reader.GetString(1);
            Console.WriteLine($"Found column: {columnName}");
            
            if (columnName == "PasswordSalt")
                hasPasswordSalt = true;
            if (columnName == "PasswordHash")
                hasPasswordHash = true;
            if (columnName == "OrganizationId")
                hasOrganizationId = true;
            if (columnName == "RoleId")
                hasRoleId = true;
        }
        
        // Add missing columns
        if (!hasPasswordSalt)
        {
            var addSaltQuery = "ALTER TABLE Users ADD COLUMN PasswordSalt TEXT NOT NULL DEFAULT '';";
            using var saltCommand = new SqliteCommand(addSaltQuery, connection);
            saltCommand.ExecuteNonQuery();
            Console.WriteLine("Added PasswordSalt column");
        }
        
        if (!hasPasswordHash)
        {
            var addHashQuery = "ALTER TABLE Users ADD COLUMN PasswordHash TEXT NOT NULL DEFAULT '';";
            using var hashCommand = new SqliteCommand(addHashQuery, connection);
            hashCommand.ExecuteNonQuery();
            Console.WriteLine("Added PasswordHash column");
        }
        
        if (!hasOrganizationId)
        {
            var addOrgQuery = "ALTER TABLE Users ADD COLUMN OrganizationId TEXT NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';";
            using var orgCommand = new SqliteCommand(addOrgQuery, connection);
            orgCommand.ExecuteNonQuery();
            Console.WriteLine("Added OrganizationId column");
        }
        
        if (!hasRoleId)
        {
            var addRoleQuery = "ALTER TABLE Users ADD COLUMN RoleId TEXT NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';";
            using var roleCommand = new SqliteCommand(addRoleQuery, connection);
            roleCommand.ExecuteNonQuery();
            Console.WriteLine("Added RoleId column");
        }
        
        Console.WriteLine("Database schema update complete!");
    }
}

class Program
{
    static void Main()
    {
        try
        {
            DatabaseFixer.AddMissingColumns();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
