# PowerShell script to fix database schema
Add-Type -AssemblyName System.Data.SQLite

# Get database path
$dbPath = Join-Path $env:APPDATA "UniversalBusinessSystem\database\UniversalBusinessSystem.db"

if (-not (Test-Path $dbPath)) {
    Write-Host "Database not found at: $dbPath"
    Write-Host "Please run the application first to create the database."
    exit 1
}

Write-Host "Database found at: $dbPath"

try {
    # Open connection
    $connection = New-Object System.Data.SQLite.SQLiteConnection("Data Source=$dbPath")
    $connection.Open()
    
    # Check existing columns
    $checkCmd = $connection.CreateCommand()
    $checkCmd.CommandText = "PRAGMA table_info(Users);"
    $reader = $checkCmd.ExecuteReader()
    
    $columns = @()
    while ($reader.Read()) {
        $columns += $reader["name"]
        Write-Host "Found column: $($reader['name'])"
    }
    $reader.Close()
    
    # Add missing columns
    $commands = @()
    
    if ("PasswordSalt" -notin $columns) {
        $commands += "ALTER TABLE Users ADD COLUMN PasswordSalt TEXT NOT NULL DEFAULT '';"
        Write-Host "Will add PasswordSalt column"
    }
    
    if ("PasswordHash" -notin $columns) {
        $commands += "ALTER TABLE Users ADD COLUMN PasswordHash TEXT NOT NULL DEFAULT '';"
        Write-Host "Will add PasswordHash column"
    }
    
    if ("OrganizationId" -notin $columns) {
        $commands += "ALTER TABLE Users ADD COLUMN OrganizationId TEXT NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';"
        Write-Host "Will add OrganizationId column"
    }
    
    if ("RoleId" -notin $columns) {
        $commands += "ALTER TABLE Users ADD COLUMN RoleId TEXT NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';"
        Write-Host "Will add RoleId column"
    }
    
    # Execute commands
    foreach ($cmdText in $commands) {
        $cmd = $connection.CreateCommand()
        $cmd.CommandText = $cmdText
        $cmd.ExecuteNonQuery()
        Write-Host "Executed: $cmdText"
    }
    
    $connection.Close()
    Write-Host "Database schema updated successfully!"
}
catch {
    Write-Host "Error: $($_.Exception.Message)"
    exit 1
}
