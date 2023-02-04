# Humeum

Humeus is a webapp for retirement planning.

* ASP.NET Core backend following Clean Architecture, CQRS, and (partially) domain-driven design.
* Backend developed with MediatR, EF Core, PostgreSQL, and Docker Compose.
* Frontend developed with Webpack, React & React Router, ECharts, and TailwindCSS.

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

### REST API

Create a basic sample transaction:

    {{url}}/api/v1/users/3/transactions?amount=150&type=INCOME&paymentStart=2023-02-04T18:33:56

