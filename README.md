# Employee API

API REST para gestión de empleados y jornadas laborales.

## Características

- CRUD completo de empleados y jornadas
- Base de datos SQLite
- Entity Framework Core 10
- Arquitectura limpia en capas
- Swagger/OpenAPI
- .NET 10

## Instalación

## API

- URL: http://localhost:5000
- Swagger: http://localhost:5000/swagger/index.html

## Endpoints

### Empleados
- GET `/api/employees` - Obtener todos
- GET `/api/employees/{id}` - Por ID
- POST `/api/employees` - Crear
- PUT `/api/employees/{id}` - Actualizar
- DELETE `/api/employees/{id}` - Eliminar

### Jornadas
- GET `/api/workdays/employee/{employeeId}` - Por empleado
- POST `/api/workdays` - Crear
- PUT `/api/workdays/{id}` - Actualizar
- DELETE `/api/workdays/{id}` - Eliminar

## Stack

- .NET 10
- ASP.NET Core 10
- EF Core 10.0.13
- SQLite
- Swashbuckle 10.3.0

## Licencia

MIT

Enero 2025
