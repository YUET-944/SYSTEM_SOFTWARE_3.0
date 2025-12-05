using Microsoft.Data.Sqlite;
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
        
        using var connection = new SqliteConnection($"Data Source={dbPath}");
        connection.Open();
        
        // Check if column exists
        var checkCmd = connection.CreateCommand();
        checkCmd.CommandText = "PRAGMA table_info(Users);";
        
        bool columnExists = false;
        using var reader = checkCmd.ExecuteReader();
        while (reader.Read())
        {
            if (reader["name"].ToString() == "PasswordSalt")
            {
                columnExists = true;
                break;
            }
        }
        
        if (!columnExists)
        {
            var addCmd = connection.CreateCommand();
            addCmd.CommandText = "ALTER TABLE Users ADD COLUMN PasswordSalt TEXT NOT NULL DEFAULT '';";
            addCmd.ExecuteNonQuery();
            Console.WriteLine("PasswordSalt column added successfully.");
        }
        else
        {
            Console.WriteLine("PasswordSalt column already exists.");
        }
    }
}
