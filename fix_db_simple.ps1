# Simple database fix using dotnet ef
$dbPath = Join-Path $env:APPDATA "UniversalBusinessSystem\database\UniversalBusinessSystem.db"

if (-not (Test-Path $dbPath)) {
    Write-Host "Database not found at: $dbPath"
    exit 1
}

Write-Host "Database found at: $dbPath"

# Use sqlite3 command line tool if available
try {
    # Check if sqlite3 is available
    $sqlite3 = Get-Command sqlite3 -ErrorAction SilentlyContinue
    if ($sqlite3) {
        Write-Host "Using sqlite3 command..."
        
        # Check columns
        $result = & sqlite3 $dbPath "PRAGMA table_info(Users);"
        Write-Host "Current columns:"
        Write-Host $result
        
        # Add missing columns
        if ($result -notmatch "PasswordSalt") {
            & sqlite3 $dbPath "ALTER TABLE Users ADD COLUMN PasswordSalt TEXT NOT NULL DEFAULT '';"
            Write-Host "Added PasswordSalt column"
        }
        
        if ($result -notmatch "PasswordHash") {
            & sqlite3 $dbPath "ALTER TABLE Users ADD COLUMN PasswordHash TEXT NOT NULL DEFAULT '';"
            Write-Host "Added PasswordHash column"
        }
        
        if ($result -notmatch "OrganizationId") {
            & sqlite3 $dbPath "ALTER TABLE Users ADD COLUMN OrganizationId TEXT NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';"
            Write-Host "Added OrganizationId column"
        }
        
        if ($result -notmatch "RoleId") {
            & sqlite3 $dbPath "ALTER TABLE Users ADD COLUMN RoleId TEXT NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';"
            Write-Host "Added RoleId column"
        }
        
        Write-Host "Database updated successfully!"
    } else {
        Write-Host "sqlite3 command not found. Please install SQLite tools or use the application to create the missing columns."
    }
}
catch {
    Write-Host "Error: $($_.Exception.Message)"
}
