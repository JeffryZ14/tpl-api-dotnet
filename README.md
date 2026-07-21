# CleanTemplate API

Template de API en .NET 8 con Clean Architecture y CQRS (MediatR), pensado como base para nuevos servicios. Incluye un caso de ejemplo (`Product`) que cubre creación, consulta, actualización de precio y desactivación.

## Stack

- .NET 8 / ASP.NET Core
- PostgreSQL + Entity Framework Core (Npgsql)
- MediatR (CQRS + pipeline behaviors)
- FluentValidation
- AutoMapper
- ErrorOr (manejo de resultados/errores sin excepciones de control de flujo)
- Polly (retries)
- AspNetCoreRateLimit
- Swagger / Swashbuckle
- Docker / docker-compose

## Arquitectura

Clean Architecture con 4 proyectos en `src/`, dependiendo siempre hacia adentro:

```
API  -->  Application  -->  Domain
API  -->  Infrastructure  -->  Domain
```

- **Domain**: entidades, agregados, value objects (`ProductId`, `ProductName`, `Money`, `Discount`) y eventos de dominio. Sin dependencias externas.
- **Application**: casos de uso como comandos/queries de MediatR (`Application/Products/{Create,Desactive,UpdatePrice,Queries}`), validadores de FluentValidation, mapeos de AutoMapper y los pipeline behaviors.
- **Infrastructure**: `AppDbContext` (EF Core + Npgsql), configuraciones de entidades, repositorios y `UnitOfWork`.
- **API**: controllers que solo hablan con MediatR (`ISender`), middlewares y configuración de arranque (`Program.cs`).

Detalle completo de la arquitectura, orden de los pipeline behaviors e inconsistencias conocidas del template en [CLAUDE.md](./CLAUDE.md).

## Requisitos

- .NET 8 SDK
- Docker y Docker Compose (para levantar Postgres o el stack completo)

## Cómo correr el proyecto

### Opción 1: Docker Compose (API + Postgres)

```bash
docker-compose up --build
```

Levanta Postgres (`localhost:5432`) y la API (`localhost:8081`). El script `scripts/init.sql` crea la tabla `products` al inicializar el contenedor de base de datos.

### Opción 2: API local + Postgres en Docker

```bash
docker-compose up db
dotnet run --project src/API/API.csproj
```

La cadena de conexión se arma a partir de la sección `DatabaseSettings` en `src/API/appsettings.json` (`Host`, `Port`, `Database`, `UserId`, `Password`), no de `ConnectionStrings`.

En ambiente `Development`/`Staging` la API expone Swagger en `/swagger`.

## Build

```bash
dotnet build CleanTemplate.sln
```

No hay proyectos de test en la solución actualmente.

## Endpoints (`ProductsController`)

| Método | Ruta                          | Descripción                     |
|--------|-------------------------------|----------------------------------|
| POST   | `/api/products`               | Crea un producto                |
| GET    | `/api/products/{id}`          | Obtiene un producto por Id       |
| PUT    | `/api/products/{id}/price`    | Actualiza el precio del producto |
| POST   | `/api/products/{id}/deactivate` | Desactiva el producto          |

Ejemplo de creación:

```json
POST /api/products
{
  "name": "Teclado mecánico",
  "price": 49.99,
  "currency": "USD"
}
```

Los errores de negocio/validación se devuelven como `ProblemDetails` (`ErrorOr` mapeado en `ApiController`), no como excepciones sin manejar.

## Configuración relevante (`src/API/appsettings.json`)

- `DatabaseSettings`: datos de conexión a Postgres.
- `IpRateLimitOptions`: límite global de requests (AspNetCoreRateLimit).
- `ApiSettings.ApiKey`: usada por `ApiKeyMiddleware` (actualmente deshabilitado en `Program.cs`).

## Notas

Este proyecto es un template/base, no un servicio de producción. Hay piezas deshabilitadas intencionalmente o pendientes de decisión (auth, eventos de dominio, `ApiKeyMiddleware`) — ver la sección "Known inconsistencies" en [CLAUDE.md](./CLAUDE.md) antes de asumir su comportamiento.
