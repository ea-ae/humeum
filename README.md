# Humeum

Humeus is an ASP.NET & React webapp for retirement planning. 

## Usage

### Startup

1. Clone repository
2. Open VS (or use the `dotnet` CLI)
3. Run tests
4. Run the `Web` project

### Databases

Create a migration:

    dotnet ef migrations add MigrationName -s Web -p Infrastructure -o Persistence/Migrations

Update database:

    dotnet ef database update -s Web -p Infrastructure

