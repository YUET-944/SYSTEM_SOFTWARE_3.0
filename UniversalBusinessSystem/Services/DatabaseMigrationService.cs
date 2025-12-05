using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using UniversalBusinessSystem.Data;
using UniversalBusinessSystem.Core.Entities;

namespace UniversalBusinessSystem.Services
{
    public interface IDatabaseMigrationService
    {
        Task<bool> MigrateToPostgreSQLAsync();
        Task<bool> TestPostgreSQLConnectionAsync(string connectionString);
        Task<bool> BackupSQLiteDatabaseAsync();
        Task<bool> ImportDataFromSQLiteAsync(string sqlitePath, string postgresPath);
        Task<MigrationProgress> GetMigrationProgressAsync();
    }

    public class MigrationProgress
    {
        public int CurrentStep { get; set; }
        public int TotalSteps { get; set; }
        public string CurrentOperation { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }

    public class DatabaseMigrationService : IDatabaseMigrationService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DatabaseMigrationService> _logger;
        private readonly UniversalBusinessSystemDbContext _sqliteContext;
        private MigrationProgress _progress = new MigrationProgress();

        public DatabaseMigrationService(
            IConfiguration configuration,
            ILogger<DatabaseMigrationService> logger,
            UniversalBusinessSystemDbContext sqliteContext)
        {
            _configuration = configuration;
            _logger = logger;
            _sqliteContext = sqliteContext;
        }

        public async Task<MigrationProgress> GetMigrationProgressAsync()
        {
            return _progress;
        }

        public async Task<bool> TestPostgreSQLConnectionAsync(string connectionString)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();
                
                using var command = new NpgsqlCommand("SELECT version();", connection);
                var version = await command.ExecuteScalarAsync();
                
                _logger.LogInformation($"PostgreSQL Connection Successful. Version: {version}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to connect to PostgreSQL");
                return false;
            }
        }

        public async Task<bool> BackupSQLiteDatabaseAsync()
        {
            try
            {
                var sqlitePath = GetSqlitePath();
                var backupPath = $"UniversalBusiness_backup_{DateTime.Now:yyyyMMdd_HHmmss}.db";
                
                if (File.Exists(sqlitePath))
                {
                    File.Copy(sqlitePath, backupPath, true);
                    _logger.LogInformation($"SQLite database backed up to: {backupPath}");
                    return true;
                }
                
                _logger.LogWarning("SQLite database file not found");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to backup SQLite database");
                return false;
            }
        }

        public async Task<bool> MigrateToPostgreSQLAsync()
        {
            try
            {
                _logger.LogInformation("Starting SQLite to PostgreSQL migration...");
                _progress = new MigrationProgress { TotalSteps = 5 };

                // Step 1: Backup SQLite database
                _progress.CurrentStep = 1;
                _progress.CurrentOperation = "Creating SQLite backup...";
                if (!await BackupSQLiteDatabaseAsync())
                {
                    _progress.ErrorMessage = "Failed to backup SQLite database";
                    return false;
                }

                // Step 2: Test PostgreSQL connection
                _progress.CurrentStep = 2;
                _progress.CurrentOperation = "Testing PostgreSQL connection...";
                var postgresConnectionString = _configuration["Database:ConnectionStrings:PostgreSQL"];
                if (!await TestPostgreSQLConnectionAsync(postgresConnectionString))
                {
                    _progress.ErrorMessage = "Cannot connect to PostgreSQL. Please check connection settings.";
                    return false;
                }

                // Step 3: Create PostgreSQL schema
                _progress.CurrentStep = 3;
                _progress.CurrentOperation = "Creating PostgreSQL schema...";
                await CreatePostgreSQLSchemaAsync(postgresConnectionString);

                // Step 4: Import data from SQLite
                _progress.CurrentStep = 4;
                _progress.CurrentOperation = "Importing data from SQLite...";
                var sqlitePath = GetSqlitePath();
                if (!await ImportDataFromSQLiteAsync(sqlitePath, postgresConnectionString))
                {
                    _progress.ErrorMessage = "Failed to import data from SQLite";
                    return false;
                }

                // Step 5: Update configuration
                _progress.CurrentStep = 5;
                _progress.CurrentOperation = "Updating configuration...";
                UpdateConfigurationToPostgreSQL();

                _progress.CurrentStep = 5;
                _progress.CurrentOperation = "Migration completed";
                _progress.IsCompleted = true;
                _logger.LogInformation("Migration completed successfully!");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Migration failed");
                _progress.ErrorMessage = ex.Message;
                return false;
            }
        }

        private async Task CreatePostgreSQLSchemaAsync(string connectionString)
        {
            try
            {
                // Read SQL script
                var sqlScript = File.ReadAllText("PostgreSQL_Migration.sql");
                
                using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();
                
                // Split script by semicolons and execute
                var commands = sqlScript.Split(';', StringSplitOptions.RemoveEmptyEntries);
                
                foreach (var cmd in commands)
                {
                    if (!string.IsNullOrWhiteSpace(cmd.Trim()))
                    {
                        using var command = new NpgsqlCommand(cmd.Trim() + ";", connection);
                        try
                        {
                            await command.ExecuteNonQueryAsync();
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning($"Command execution warning: {ex.Message}");
                        }
                    }
                }
                
                _logger.LogInformation("PostgreSQL schema created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create PostgreSQL schema");
                throw;
            }
        }

        public async Task<bool> ImportDataFromSQLiteAsync(string sqlitePath, string postgresPath)
        {
            try
            {
                _logger.LogInformation("Starting data import from SQLite to PostgreSQL...");
                
                using var sqliteConn = new SqliteConnection($"Data Source={sqlitePath}");
                using var postgresConn = new NpgsqlConnection(postgresPath);
                
                await sqliteConn.OpenAsync();
                await postgresConn.OpenAsync();
                
                // Import data in order of dependencies
                await ImportTableAsync(sqliteConn, postgresConn, "organizations");
                await ImportTableAsync(sqliteConn, postgresConn, "shop_types");
                await ImportTableAsync(sqliteConn, postgresConn, "units");
                await ImportTableAsync(sqliteConn, postgresConn, "modules");
                await ImportTableAsync(sqliteConn, postgresConn, "permissions");
                await ImportTableAsync(sqliteConn, postgresConn, "categories");
                await ImportTableAsync(sqliteConn, postgresConn, "suppliers");
                await ImportTableAsync(sqliteConn, postgresConn, "products");
                await ImportTableAsync(sqliteConn, postgresConn, "users");
                
                _logger.LogInformation("Data import completed successfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to import data from SQLite");
                return false;
            }
        }

        private async Task ImportTableAsync(SqliteConnection source, NpgsqlConnection destination, string tableName)
        {
            try
            {
                // Read from SQLite
                var selectQuery = $"SELECT * FROM {tableName}";
                using var selectCommand = new SqliteCommand(selectQuery, source);
                using var reader = await selectCommand.ExecuteReaderAsync();
                
                if (reader.HasRows)
                {
                    // Get column names
                    var columnNames = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        columnNames.Add(reader.GetName(i));
                    }
                    
                    // Prepare insert query for PostgreSQL
                    var columnList = string.Join(", ", columnNames);
                    var valueParams = string.Join(", ", columnNames.Select(c => $"@{c}"));
                    var insertQuery = $"INSERT INTO {tableName} ({columnList}) VALUES ({valueParams}) ON CONFLICT DO NOTHING";
                    
                    int rowCount = 0;
                    
                    while (await reader.ReadAsync())
                    {
                        using var insertCommand = new NpgsqlCommand(insertQuery, destination);
                        
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var value = reader.IsDBNull(i) ? DBNull.Value : reader.GetValue(i);
                            insertCommand.Parameters.AddWithValue($"@{columnNames[i]}", value ?? DBNull.Value);
                        }
                        
                        await insertCommand.ExecuteNonQueryAsync();
                        rowCount++;
                    }
                    
                    _logger.LogInformation($"Imported {rowCount} rows into {tableName}");
                }
                else
                {
                    _logger.LogInformation($"No data found in {tableName}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error importing {tableName}");
                throw;
            }
        }

        private string GetSqlitePath()
        {
            var sqlitePath = _configuration["Database:ConnectionStrings:SQLite"];
            if (string.IsNullOrEmpty(sqlitePath))
            {
                sqlitePath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "UniversalBusinessSystem",
                    "database",
                    "UniversalBusinessSystem.db");
            }
            return sqlitePath.Replace("Data Source=", "");
        }

        private void UpdateConfigurationToPostgreSQL()
        {
            try
            {
                var configPath = "appsettings.json";
                var configJson = File.ReadAllText(configPath);
                
                // Update Provider to PostgreSQL
                configJson = configJson.Replace(
                    "\"Provider\": \"SQLite\"", 
                    "\"Provider\": \"PostgreSQL\"");
                
                File.WriteAllText(configPath, configJson);
                
                _logger.LogInformation("Configuration updated to use PostgreSQL");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update configuration");
                throw;
            }
        }
    }
}
