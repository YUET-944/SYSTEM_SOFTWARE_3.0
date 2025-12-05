-- Add PasswordSalt column to Users table if it doesn't exist
ALTER TABLE Users ADD COLUMN PasswordSalt TEXT NOT NULL DEFAULT '';
