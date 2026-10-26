## Configuration

Set the following User Secrets:

dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=books;Username=...;Password=..."

dotnet user-secrets set "Jwt:Key" "$(openssl rand -base64 48)"