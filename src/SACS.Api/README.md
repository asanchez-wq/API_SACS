# SACS.Api — README de pruebas

Breve guía para ejecutar la API localmente, abrir Swagger, importar la colección de Postman y usar el manual de pruebas.

Rutas de documentación (relativas al repo):
- src/SACS.Api/docs/SACS_API_with_tests.postman_collection.json
- src/SACS.Api/docs/SACS_API_environment.postman_environment.json
- src/SACS.Api/docs/SACS_API.postman_collection.json (opcional)
- src/SACS.Api/docs/newman/package.json
- src/SACS.Api/docs/INSTRUCCIONES_POSTMAN_NEWMAN.md
- src/SACS.Api/docs/MANUAL_PRUEBAS.md
- src/SACS.Api/docs/MANUAL_PRUEBAS_CON_IMAGENES.md
- src/SACS.Api/docs/examples/*.json

Requisitos
- Visual Studio 2022 o posterior
- .NET 8 SDK
- Postman (opcional) o curl
- Node.js + npm (para Newman, si usa CI)

Ejecutar la API localmente
1. Abrir la solución en Visual Studio.
2. Iniciar el proyecto SACS.Api (F5/Ctrl+F5).
3. En Output buscar: `Now listening on: https://localhost:PORT` — usar esa URL como `{BASE_URL}`.

Swagger
- URL: `{BASE_URL}/swagger`
- Si no ve Swagger, confirme `ASPNETCORE_ENVIRONMENT=Development` en `Properties/launchSettings.json`.

Colección Postman
- Importar `src/SACS.Api/docs/SACS_API_with_tests.postman_collection.json`.
- Importar environment `src/SACS.Api/docs/SACS_API_environment.postman_environment.json`.
- Ajustar `baseUrl` en el environment con la URL mostrada en Output.
- Ejecutar `Auth - Obtener token (dev)` para que el token se guarde en la variable `token`.

Newman (CI / local)
1. cd src/SACS.Api/docs/newman
2. npm ci
3. npm run test:api

Soporte y contribuciones
- Añada capturas en `src/SACS.Api/docs/images/` si usa la documentación con imágenes.
- Para cambios en documentación: git add/commit/push y abrir PR con pasos reproducibles.
