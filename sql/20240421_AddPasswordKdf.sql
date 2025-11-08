ALTER TABLE dbo.Users
ADD PasswordKdf NVARCHAR(256) NULL;

GO

-- Optional: backfill PBKDF2 credentials as users log in successfully.
