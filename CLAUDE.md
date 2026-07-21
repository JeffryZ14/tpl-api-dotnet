# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Build
dotnet build CleanTemplate.sln

# Run API locally (needs Postgres reachable per src/API/appsettings.json DatabaseSettings)
dotnet run --project src/API/API.csproj

# Restore only
dotnet restore CleanTemplate.sln

# Full stack (API + Postgres) via Docker
docker-compose up --build
```

There are no test projects in the solution (`**/*test*` is empty) and no CI config — do not assume a test command exists unless one is added.

## Architecture

Clean Architecture / CQRS template, 4 projects under `src/`, referencing inward only:

```
API  -->  Application  -->  Domain
API  -->  Infrastructure  -->  Domain
```

- **Domain** — no project references. Aggregates/entities (`Domain/Entities`), value objects (`Domain/ValueObjects`: `ProductId`, `ProductName`, `Money`, `Discount`), base types (`Domain/Common/Entity.cs`, `Domain/Primitives/AggregateRoot.cs`, `Domain/Primitives/DomainEvent.cs`). `Domain/Entities/Audit` has `ActivatableAggregateRoot`/`AuditableAggregateRoot` mixins for soft-activate and created/modified tracking that aggregates can inherit.
- **Application** — CQRS via MediatR. One folder per use case under `Application/Products/{Create,Desactive,UpdatePrice,Queries}`, each with a `record ... : IRequest<ErrorOr<T>>` command/query plus its handler. Folder names and C# namespaces diverge (e.g. folder `Products/Create`, namespace `Application.Products.Commands`; handlers live in `Application.Products.Handlers`) — search by namespace, not by folder path, when looking for a request/handler pair. FluentValidation validators live in `Application/Products/Validators`. `Application/Common/Errors.cs` centralizes `ErrorOr` error factories, nested per aggregate (`Errors.Product.NotFound(id)`).
- **Infrastructure** — EF Core (Npgsql) persistence. `Persistence/AppDbContext.cs`, `Persistence/Configurations` (`IEntityTypeConfiguration<T>` per entity), `Persistence/Repository` (repository per aggregate, e.g. `EfProductRepository`), `Persistence/UnitOfWork.cs` (wraps `SaveChangesAsync` and dispatches queued domain events via `IMediator.Publish` — see behavior note below).
- **API** — controllers only call `ISender`/MediatR, never repositories or the DbContext directly. `Controllers/ApiController.cs` is the shared base that maps `ErrorOr` failures to `ProblemDetails` (`Problem(errors)`); derive new controllers from it rather than `ControllerBase` to keep error-mapping consistent.

### MediatR pipeline behaviors

Registered in `src/API/Extensions/ServiceCollectionExtensions.cs` (`AddApplication`), in this exact order, which is also execution order (outermost first):

`ValidationBehavior` -> `LoggingBehavior` -> `UnitOfWorkBehavior` -> `CachingBehavior` -> `RetryBehavior` -> `AuditLoggingBehavior`

Notably `UnitOfWorkBehavior` runs for every request (queries included) and calls `CommitAsync` unconditionally after `next()` — there's no read/write split, so a query handler that mutates tracked entities would get silently persisted. `CachingBehavior` only applies to requests implementing `ICacheableQuery<TResponse>`; nothing currently implements it. Registration order matters when adding a new behavior — insert it where it needs to sit in the pipeline, not at the end by default.

MediatR is registered twice — once in `Infrastructure` (`AddInfrastructure`, scanning the assembly containing `AppDbContext`) and once in `Application` (`AddApplication`, scanning the assembly containing `CreateProductHandler`) — because handlers live in `Application` but the pipeline/DbContext wiring lives in `Infrastructure`. Add new handlers to `Application`; they're picked up automatically.

### Known inconsistencies (don't "fix" without confirming intent)

- `docker-compose.yml` sets `ConnectionStrings__DefaultConnection`, and `Program.cs` logs `builder.Configuration.GetConnectionString("DefaultConnection")`, but `AddInfrastructure` actually builds the Npgsql connection string from the separate `DatabaseSettings` config section (`Infrastructure/Common/DatabaseSettings.cs`) via `Host`/`Port`/`Database`/`UserId`/`Password`. The logged connection string and the one actually used are different config values.
- `API/Extensions/ApplicationBuilderExtensions.cs` defines a `UseApi()` extension (routing, auth, `/health` mapping, Swagger) that is never called — `Program.cs` inlines an equivalent but slightly different pipeline instead, and does **not** map `/health` or call `UseAuthorization()`. `ApiKeyMiddleware` and `app.UseAuthorization()` are both commented out in `Program.cs`.
- `Product.cs`'s constructor and `UpdatePrice` have `SetCreated`/`SetModified`/`AddDomainEvent` calls commented out, so audit fields and `ProductCreatedEvent`/`ProductPriceChangedEvent` are not currently raised despite the event classes and `ActivatableAggregateRoot` base existing.

## Configuration

`src/API/appsettings.json`: `DatabaseSettings` (Postgres connection parts), `IpRateLimitOptions` (AspNetCoreRateLimit, applied globally via `app.UseIpRateLimiting()`), `ApiSettings:ApiKey` (used by the currently-disabled `ApiKeyMiddleware`). CORS policy `AllowApiTemplate` allows any origin/header/method — defined in `API/DependencyInjection.cs`.
