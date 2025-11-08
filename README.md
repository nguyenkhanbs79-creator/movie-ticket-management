# Movie Ticket Management

## Authentication Hardening

The application now uses PBKDF2 with SHA-256 to store credentials securely.

1. Run the migration script located at `sql/20240421_AddPasswordKdf.sql` to add the new `PasswordKdf` column.
2. Existing users that still rely on the legacy SHA-256 hash will be transparently upgraded to PBKDF2 on their next successful login.
3. The WinForms client sends the plain password over the trusted network channel; hashing and verification happen entirely within the business layer.

No additional configuration changes are required.