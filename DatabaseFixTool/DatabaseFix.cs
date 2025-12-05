using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

class Program
{
    static void Main()
    {
        var dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "UniversalBusinessSystem",
            "database",
            "UniversalBusinessSystem.db");
        
        if (!File.Exists(dbPath))
        {
            Console.WriteLine("Database not found at: " + dbPath);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            return;
        }
        
        Console.WriteLine("Database found at: " + dbPath);
        
        try
        {
            using var connection = new SQLiteConnection($"Data Source={dbPath}");
            connection.Open();
            
            // Check existing columns
            using var checkCmd = new SQLiteCommand("PRAGMA table_info(Users);", connection);
            using var reader = checkCmd.ExecuteReader();
            
            var columns = new System.Collections.Generic.List<string>();
            while (reader.Read())
            {
                var columnName = reader.GetString(1);
                columns.Add(columnName);
                Console.WriteLine($"Found column: {columnName}");
            }
            
            // Add missing columns
            if (!columns.Contains("PasswordSalt"))
            {
                using var cmd = new SQLiteCommand("ALTER TABLE Users ADD COLUMN PasswordSalt TEXT NOT NULL DEFAULT '';", connection);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Added PasswordSalt column");
            }
            
            if (!columns.Contains("PasswordHash"))
            {
                using var cmd = new SQLiteCommand("ALTER TABLE Users ADD COLUMN PasswordHash TEXT NOT NULL DEFAULT '';", connection);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Added PasswordHash column");
            }
            
            if (!columns.Contains("OrganizationId"))
            {
                using var cmd = new SQLiteCommand("ALTER TABLE Users ADD COLUMN OrganizationId TEXT NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';", connection);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Added OrganizationId column");
            }
            
            if (!columns.Contains("RoleId"))
            {
                using var cmd = new SQLiteCommand("ALTER TABLE Users ADD COLUMN RoleId TEXT NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';", connection);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Added RoleId column");
            }
            
            Console.WriteLine("Database schema updated successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
