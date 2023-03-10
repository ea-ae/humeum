# Humeum

Humeum is a webapp for retirement planning. Still in progress, not all features outlined are complete.

## Features

* Financial simulations (simple + Monte-Carlo). Income and expense tracking throughout time. Calculations accounting for income taxes and tax discounts.
* Simple premade security options (index funds, III pillar funds, bonds) with configurable asset allocation ratios, as well as custom asset type creation.

## Tech

* ASP.NET backend following Clean Architecture (with a few specks of dirt), (compromising) CQS, OpenAPI REST, and (mostly) domain-driven design.
* Backend: EF Core, ASP.NET Identity with JWT, MediatR, AutoMapper, xUnit, Moq, and PostgreSQL.
* Frontend: Webpack, React and React Router. Apache ECharts for data visualization. TailwindCSS and MUI for styling.
* Deployment: CI/CD with Docker Swarms & nginx.
* Future plans may include trying out tech like RabbitMQ, GraphQL, load balancing, SSR, event sourcing, etc.

## Testing

* Unit tests for the domain model.
* Integration tests for the application services (queries/commands) with mocked infrastructure services (e.g. authentication) and an in-memory database.
* Integration tests for the web controllers through a test server & HTTP client with an in-memory database.

## Design

The project is composed of four layers: domain, application, infrastructure, and presentation. The domain layer consists of rich domain objects that double up as EF entity models, but are themselves ignorant of EF, thanks to Fluent API configuration in a higher persistence-aware layer. Domain objects are organized into aggregates. Enumeration classes and ValueObjects are used extensively to store enumeration data in reference tables and encapsulate domain validation, respectively.

The application layer describes the use cases for our domain. It is composed primarily of queries and commands that perform application-specific validation and work with domain objects and their methods. This layer also contains interfaces for classes that are implemented and provided through dependency injection in the infrastructure layer.

The infrastructure layer contains all the implementations that the application layer needs, such as persistence & DbContext, authentication, and other external services. 

Finally, the presentation layer, which is where ASP.NET is configured, depends on the application layer but not the infrastructure layer (although it does reference it for DI configuration). The presentation layer contains all the controllers & filters, as well as the application entry point.

When a request is received, it first runs through authentication/authorization middleware and our controller filters. After model binding is successfully performed, the controller sends a request through MediatR that arrives at a command/query handler, where further validation and business logic is performed (through infrastructure services & domain methods). Finally, a result is returned (e.g. through an automapped DTO) that is passed back to the controller and finally the client.

## Setup

### Dev setup

1. Fully configure `appsettings.json`
1. Apply migrations with `cd server/src && dotnet ef database update -s Web -p Infrastructure`
1. Run backend tests with `cd server && dotnet test`
1. Run the backend ASP.NET `Web` project with `cd server/src && dotnet run --project Web`
1. Run the frontend React project with `cd client && npm install && npm run server` (a proxy to the backend REST API will be added at `localhost:port/api/`)

### Prod setup

1. Create a valid `.env` file based off the `.env.example` template
1. Configure `appsettings.json` non-sensitive data (the rest is in `.env`)
1. Run the `docker-compose up` command

### Databases

Create a migration:

    dotnet ef migrations add MigrationName -s Web -p Infrastructure -o Persistence/Migrations

Update database:

    dotnet ef database update -s Web -p Infrastructure
