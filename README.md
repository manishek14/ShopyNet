# ShopyNet

<p align="center">
  <strong>A modular e-commerce backend built with ASP.NET Core and .NET 10</strong>
  <br />
  A layered C# application for product discovery, customer accounts, shopping orders, and shop administration.
</p>

<p align="center">
  <a href="https://dotnet.microsoft.com/"><img src="https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white" alt=".NET 10" /></a>
  <a href="https://learn.microsoft.com/aspnet/core"><img src="https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4?logo=dotnet&logoColor=white" alt="ASP.NET Core Web API" /></a>
  <a href="https://learn.microsoft.com/ef/core/"><img src="https://img.shields.io/badge/Entity%20Framework%20Core-10-512BD4" alt="Entity Framework Core" /></a>
  <a href="https://www.microsoft.com/sql-server"><img src="https://img.shields.io/badge/Database-SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white" alt="SQL Server" /></a>
  <a href="https://redis.io/"><img src="https://img.shields.io/badge/Cache-Redis-DC382D?logo=redis&logoColor=white" alt="Redis" /></a>
  <a href="https://swagger.io/"><img src="https://img.shields.io/badge/API-Swagger-85EA2D?logo=swagger&logoColor=black" alt="Swagger" /></a>
</p>

## Overview

**ShopyNet** is an e-commerce backend project focused on organizing business logic into clear application layers. It exposes HTTP API endpoints for core shop workflows and combines domain-oriented modeling with infrastructure services such as SQL Server persistence, Redis caching, structured logging, and JWT-based authentication.

The repository is structured as a multi-project .NET solution, separating domain rules, application use cases, data access, query handling, and API presentation.

## Features

- **Authentication and accounts** — registration, login, JWT authentication, refresh-token handling, and email confirmation flows.
- **Product catalog** — product lookup by ID or slug, filtering, product creation and editing, and image management.
- **Categories and comments** — dedicated API controllers for catalog categories and product comments.
- **Shopping orders** — order retrieval, item management, address selection, shipping method selection, and order finalization.
- **User and seller workflows** — separate controllers and query/application components for users, sellers, roles, and related data.
- **Authorization** — role-based access control for protected operations.
- **Rate limiting** — configurable request limits, including stricter policies for sensitive endpoints.
- **Caching** — Redis integration for application caching.
- **Operational visibility** — Serilog logging to console, rolling files, and SQL Server; health-check integration; and Swagger/OpenAPI documentation.
- **Shared infrastructure** — reusable validation, API response, pagination, file handling, and security utilities.

> This README describes functionality represented in the current source tree. It does not imply that the project has been independently load-tested or audited for production readiness.

## Technology Stack

| Area | Technologies |
| --- | --- |
| Runtime and API | C#, .NET 10, ASP.NET Core Web API |
| Persistence | Entity Framework Core 10, SQL Server |
| Caching | Redis, StackExchange.Redis |
| Application dispatch | MediatR |
| Authentication | JWT Bearer, refresh-token flow, role-based authorization |
| Logging | Serilog |
| API documentation | Swagger / Swashbuckle |
| Email | MailKit, MimeKit |
| Health monitoring | ASP.NET Core health checks |

## Architecture

ShopyNet is organized into projects with distinct responsibilities:

```text
ShopyNet
├── Shop/EndPoints/Shop.Api       HTTP controllers, middleware pipeline, API configuration
├── Shop.Presentation.Facade      Facades coordinating application and query operations
├── Shop.Application              Commands and application use cases
├── Shop.Query                    Read operations, query handlers, and DTOs
├── Shop.Domain                   Shop-specific domain model and business rules
├── Shop.Infrastructure           EF Core persistence, repositories, caching, email
├── Shop/Shop.Config              Dependency registration and application configuration
└── Common/
    ├── Common.Domain             Shared domain primitives and exceptions
    ├── Common.Aplication         Shared application contracts and utilities
    ├── Common.Query              Shared query contracts and filtering abstractions
    └── Common.AspNetCore         API helpers and exception-handling middleware
```

### Request flow

The API controllers receive HTTP requests and delegate work through the presentation facade to application commands or query handlers. Domain types hold shop-specific business concepts, while infrastructure components provide persistence and integrations.

The solution uses separate command and query components to keep write use cases and read operations organized. This is a code-organization approach; it should not be interpreted as a claim that the application uses a fully independent CQRS infrastructure or separate read/write databases.

## Getting Started

### Prerequisites

Install the following before running the API:

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- SQL Server
- Redis
- An editor or IDE such as Visual Studio or Visual Studio Code

### 1. Clone the repository

```bash
git clone https://github.com/manishek14/ShopyNet.git
cd ShopyNet
```

### 2. Configure application settings

The API expects a `ConnectionStrings:DefaultConnection` value and JWT configuration. Redis settings are defined in `Shop/EndPoints/Shop.Api/appsettings.json`.

Configure environment-specific values or .NET User Secrets for local development. For example, from the API project directory:

```bash
cd Shop/EndPoints/Shop.Api
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<YOUR_SQL_SERVER_CONNECTION_STRING>"
dotnet user-secrets set "JwtConfig:SecretKey" "<YOUR_LONG_RANDOM_SECRET>"
```

Make sure the JWT issuer/audience and other `JwtConfig` values match the configuration expected by the application. Set the Redis connection string to your Redis instance if it is not running at the configured local address.

**Do not commit real passwords, signing keys, or production connection strings.** Review `appsettings.json` and replace machine-specific local configuration before deploying the application.

### 3. Restore dependencies

From the repository root:

```bash
dotnet restore ShopyNet.slnx
```

### 4. Apply database migrations

The repository includes Entity Framework Core migrations. After configuring a valid SQL Server connection, use the EF Core tools to apply them. If the EF tools are not installed globally, you can use the local tool manifest or install the EF Core command-line tool.

From the repository root, a typical command is:

```bash
dotnet ef database update \
  --project Shop.Infrastructure/Shop.Infrastructure.csproj \
  --startup-project Shop/EndPoints/Shop.Api/Shop.Api.csproj
```

If the tooling reports that it cannot resolve the migrations assembly or startup configuration, check the project's EF Core design-time setup and run the command with the appropriate project paths.

### 5. Run the API

From the repository root:

```bash
dotnet run --project Shop/EndPoints/Shop.Api/Shop.Api.csproj
```

When the application is running, open the Swagger endpoint shown in the startup output or configured launch profile to explore the available API operations.

## API Areas

The API currently includes controllers for the following areas:

| Controller | Responsibility |
| --- | --- |
| `AuthController` | Registration, login, and token-related authentication flows |
| `ProductController` | Product queries, filtering, create/update operations, and images |
| `CategoryController` | Product category endpoints |
| `CommentController` | Comment-related endpoints |
| `OrderController` | Order queries and shopping-order operations |
| `UserController` | User-related operations |
| `SellerController` | Seller-related operations |
| `RoleController` | Role management endpoints |
| `AuditController` | Audit-related endpoints |

For exact routes, request models, authorization requirements, and response formats, refer to the controller source and Swagger documentation.

## Configuration Notes

- **SQL Server:** provide a valid `ConnectionStrings:DefaultConnection` value.
- **Redis:** configure the `Redis:ConnectionString` and related cache settings.
- **JWT:** set a strong signing secret and the issuer/audience values required by the token validation configuration.
- **CORS:** update the configured origins for your frontend and deployment environment.
- **Logging:** review Serilog sinks and file retention settings for your environment.
- **Uploads:** verify image storage paths and permissions before deployment.

## Development

Contributions and improvements are welcome. Before opening a pull request:

1. Keep responsibilities within the appropriate solution layer.
2. Avoid committing credentials or machine-specific configuration.
3. Restore dependencies and build the solution.
4. Test changes to authentication, authorization, order handling, and persistence paths.

Build the solution from the repository root:

```bash
dotnet build ShopyNet.slnx
```

## Roadmap

Potential next steps for the project include expanding automated test coverage, documenting endpoint contracts and example requests, and providing a reproducible local environment for SQL Server and Redis.

## Author

**Mani Shekofteh**

- GitHub: [@manishek14](https://github.com/manishek14)
- LinkedIn: [mani-shekofteh](https://linkedin.com/in/mani-shekofteh)

---

<p align="center">
  Built with C# and ASP.NET Core.
</p>
