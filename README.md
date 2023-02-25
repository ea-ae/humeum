# Humeum

Humeum is a webapp for retirement planning.

* ASP.NET Core backend following Clean Architecture (with a few specks of dirt), (compromised) CQRS, and (somewhat) domain-driven design.
* Backend developed with MediatR, EF Core, PostgreSQL, optional JWT authentication, and Docker Compose.
* Frontend developed with Webpack, React & React Router, ECharts, and TailwindCSS.

## Usage

### Startup

1. Clone repository
2. Open VS (or use the `dotnet` CLI)
3. Run backend tests with `cd server && dotnet test`
4. Run the backend ASP.NET `Web` project with `cd server && dotnet run --project src/Web`
5. Run the frontend React project with `cd client && npm install && npm run server` (a proxy to the backend REST API will be added at `localhost:port/api/`)

### Databases

Create a migration:

    dotnet ef migrations add MigrationName -s Web -p Infrastructure -o Persistence/Migrations

Update database:

    dotnet ef database update -s Web -p Infrastructure
