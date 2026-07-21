# Seguridad

## Reportar una vulnerabilidad

1. **No** abras un issue público.
2. Escribe a `jeffryzavala@hotmail.com` con asunto `[SECURITY] tpl-api-dotnet`.
3. Incluye: descripción, pasos para reproducir, impacto.

Respuesta esperada en 48 horas.

## Prácticas del template

- Secrets vía variables de entorno / `appsettings.*.json` fuera de git (ver `.gitignore`); nunca hardcodeados.
- Validación de entrada con FluentValidation en cada request de la capa Application.
- Manejo de errores con ErrorOr, sin exponer stack traces al cliente.
- Rate limiting vía AspNetCoreRateLimit.
- Dependencias auditadas por Dependabot (semanal, `nuget` + `github-actions`).
- CI corre `dotnet build` en cada push/PR a `main`/`master`/`develop`.
