# Guía de Inicio Rápido - EmployeeAPI

## Requisitos Previos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) o superior
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) o [Visual Studio Code](https://code.visualstudio.com/)
- Git (opcional)

## Instalación y Ejecución

### 1. Restaurar Dependencias

```bash
dotnet restore
```

### 2. Ejecutar la Aplicación

```bash
dotnet run --project src/EmployeeAPI.Api
```

O desde Visual Studio:
- Abre la solución `EmployeeAPI.sln`
- Establece `EmployeeAPI.Api` como proyecto de inicio
- Presiona `F5` para ejecutar

### 3. Acceder a la API

La API estará disponible en:
- **URL Base**: `https://localhost:5001`
- **Swagger UI**: `https://localhost:5001/swagger`

## Estructura de Carpetas

```
EmployeeAPI/
├── src/
│   ├── EmployeeAPI.Api/
│   │   ├── Controllers/
│   │   │   └── EmployeesController.cs
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   └── EmployeeAPI.Api.csproj
│   │
│   ├── EmployeeAPI.Application/
│   │   ├── DTOs/
│   │   │   └── EmployeeDto.cs
│   │   ├── Services/
│   │   │   ├── IEmployeeService.cs
│   │   │   └── EmployeeService.cs
│   │   ├── Repositories/
│   │   │   └── IEmployeeRepository.cs
│   │   └── EmployeeAPI.Application.csproj
│   │
│   ├── EmployeeAPI.Domain/
│   │   ├── Entities/
│   │   │   ├── Employee.cs
│   │   │   └── Workday.cs
│   │   └── EmployeeAPI.Domain.csproj
│   │
│   └── EmployeeAPI.Infrastructure/
│       ├── Persistence/
│       │   └── EmployeeDbContext.cs
│       ├── Repositories/
│       │   └── EmployeeRepository.cs
│       └── EmployeeAPI.Infrastructure.csproj
│
├── EmployeeAPI.sln
├── README.md
├── ARCHITECTURE.md
└── GETTING_STARTED.md (este archivo)
```

## Endpoints de la API

### Empleados

#### Obtener todos los empleados
```http
GET /api/employees
```

**Respuesta exitosa (200)**:
```json
[
  {
	"id": 1,
	"firstName": "Juan",
	"lastName": "Pérez",
	"email": "juan.perez@example.com",
	"position": "Ingeniero"
  }
]
```

#### Obtener empleado por ID
```http
GET /api/employees/{id}
```

**Parámetros**:
- `id` (int): ID del empleado

**Respuesta exitosa (200)**:
```json
{
  "id": 1,
  "firstName": "Juan",
  "lastName": "Pérez",
  "email": "juan.perez@example.com",
  "position": "Ingeniero"
}
```

#### Crear empleado
```http
POST /api/employees
Content-Type: application/json

{
  "firstName": "María",
  "lastName": "García",
  "email": "maria.garcia@example.com",
  "position": "Analista"
}
```

**Respuesta exitosa (201)**:
```json
{
  "id": 2,
  "firstName": "María",
  "lastName": "García",
  "email": "maria.garcia@example.com",
  "position": "Analista"
}
```

#### Actualizar empleado
```http
PUT /api/employees/{id}
Content-Type: application/json

{
  "firstName": "María",
  "lastName": "García",
  "email": "maria.garcia.nueva@example.com",
  "position": "Gerente"
}
```

**Respuesta exitosa (204)**: Sin contenido

#### Eliminar empleado
```http
DELETE /api/employees/{id}
```

**Respuesta exitosa (204)**: Sin contenido

#### Obtener jornadas laborales de un empleado
```http
GET /api/employees/workdays/{id}
```

**Respuesta exitosa (200)**:
```json
{
  "employeeId": 1,
  "employeeName": "Juan Pérez",
  "workdays": [
	"2024-01-15T00:00:00",
	"2024-01-16T00:00:00",
	"2024-01-17T00:00:00"
  ]
}
```

## Compilación

```bash
# Compilar toda la solución
dotnet build EmployeeAPI.sln

# Compilar un proyecto específico
dotnet build src/EmployeeAPI.Api/EmployeeAPI.Api.csproj
```

## Base de Datos

La aplicación utiliza **SQLite** con una base de datos local:
- **Archivo**: `employees.db` (se crea automáticamente en el directorio de ejecución)
- **Configuración**: Ver `appsettings.json`

Para resetear la base de datos:
```bash
# Eliminar el archivo de base de datos
rm employees.db

# La siguiente ejecución creará una nueva base de datos vacía
```

## Configuración

### appsettings.json

```json
{
  "ConnectionStrings": {
	"DefaultConnection": "Data Source=employees.db"
  },
  "Logging": {
	"LogLevel": {
	  "Default": "Information"
	}
  }
}
```

## Desarrollo

### Agregar Migrations (si se modifica el modelo)

```bash
# Instalar EF Tools
dotnet tool install --global dotnet-ef

# Crear una nueva migration
dotnet ef migrations add NombreMigration -p src/EmployeeAPI.Infrastructure -s src/EmployeeAPI.Api

# Aplicar migrations
dotnet ef database update -p src/EmployeeAPI.Infrastructure -s src/EmployeeAPI.Api
```

### Estructura de Código

- Cada proyecto en una carpeta separada dentro de `src/`
- Las interfaces se definen en `Application` (contrato)
- Las implementaciones en `Infrastructure` (detalles)
- Los DTOs en `Application/DTOs`
- Los Entities en `Domain/Entities`

## Próximas Pasos

1. ✅ Completar operaciones CRUD de empleados
2. ⏳ Implementar operaciones CRUD de jornadas laborales
3. ⏳ Agregar validaciones
4. ⏳ Implementar autenticación
5. ⏳ Agregar pruebas unitarias
6. ⏳ Agregar pruebas de integración
7. ⏳ Dockerizar la aplicación

## Troubleshooting

### "El comando dotnet no se encuentra"
- Asegúrate de tener .NET 8.0 SDK instalado
- Añade .NET al PATH de tu sistema

### "Port 5001 already in use"
- Cambia el puerto en `launchSettings.json`
- O termina el proceso que está usando el puerto

### "Database locked"
- Asegúrate de no tener otra instancia de la aplicación ejecutándose
- Elimina `employees.db-shm` y `employees.db-wal` si existen

## Recursos

- [Documentación de ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Repository Pattern](https://martinfowler.com/eaaCatalog/repository.html)
