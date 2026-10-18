# API de Jornadas Laborales - Documentación

## 📋 Descripción

Este API REST permite gestionar empleados y sus jornadas laborales con persistencia en SQLite.

## 🗄️ Base de Datos

- **Tipo**: SQLite
- **Ubicación**: `employees.db` (se crea automáticamente en la raíz del proyecto)
- **Características**: 
  - Migrations automáticas al ejecutar la aplicación
  - Seed data con 3 empleados de ejemplo
  - Relaciones Uno-a-Muchos (Employee → Workdays)

## 🔌 Endpoints

### Empleados

#### GET /api/employees
Obtiene todos los empleados.
```bash
curl -X GET https://localhost:7001/api/employees
```

#### GET /api/employees/{id}
Obtiene un empleado específico por ID.
```bash
curl -X GET https://localhost:7001/api/employees/1
```

#### POST /api/employees
Crea un nuevo empleado.
```bash
curl -X POST https://localhost:7001/api/employees \
  -H "Content-Type: application/json" \
  -d '{
	"firstName": "Juan",
	"lastName": "Pérez",
	"email": "juan@example.com",
	"position": "Developer"
  }'
```

#### PUT /api/employees/{id}
Actualiza un empleado existente.
```bash
curl -X PUT https://localhost:7001/api/employees/1 \
  -H "Content-Type: application/json" \
  -d '{
	"firstName": "Juan",
	"lastName": "García",
	"email": "juan.garcia@example.com",
	"position": "Senior Developer"
  }'
```

#### DELETE /api/employees/{id}
Elimina un empleado.
```bash
curl -X DELETE https://localhost:7001/api/employees/1
```

#### GET /api/employees/workdays/{id}
Obtiene todas las jornadas de un empleado.
```bash
curl -X GET https://localhost:7001/api/employees/workdays/1
```

### Jornadas Laborales

#### GET /api/workdays/employee/{employeeId}
Obtiene todas las jornadas de un empleado específico.
```bash
curl -X GET https://localhost:7001/api/workdays/employee/1
```

#### GET /api/workdays/{id}
Obtiene una jornada específica por ID.
```bash
curl -X GET https://localhost:7001/api/workdays/1
```

#### POST /api/workdays
Crea una nueva jornada laboral.
```bash
curl -X POST https://localhost:7001/api/workdays \
  -H "Content-Type: application/json" \
  -d '{
	"employeeId": 1,
	"date": "2024-01-15",
	"hoursWorked": 8.5
  }'
```

#### PUT /api/workdays/{id}
Actualiza una jornada laboral.
```bash
curl -X PUT https://localhost:7001/api/workdays/1 \
  -H "Content-Type: application/json" \
  -d '{
	"date": "2024-01-15",
	"hoursWorked": 9.0
  }'
```

#### DELETE /api/workdays/{id}
Elimina una jornada laboral.
```bash
curl -X DELETE https://localhost:7001/api/workdays/1
```

## 🚀 Ejecución

### Iniciar la aplicación

```bash
cd src/EmployeeAPI.Api
dotnet run
```

La aplicación estará disponible en:
- **HTTP**: https://localhost:7001
- **Swagger UI**: https://localhost:7001/swagger

### Primera vez (Con Seed Data)

Al ejecutar por primera vez:
1. Se crea automáticamente la base de datos `employees.db`
2. Se ejecutan todas las migrations
3. Se insertan 3 empleados de ejemplo con sus jornadas

## 📊 Estructura de Datos

### Employee (Empleado)
```csharp
{
  "id": 1,
  "firstName": "Juan",
  "lastName": "Pérez",
  "email": "juan@example.com",
  "position": "Senior Developer"
}
```

### Workday (Jornada Laboral)
```csharp
{
  "id": 1,
  "employeeId": 1,
  "date": "2024-01-15",
  "hoursWorked": 8.5
}
```

### EmployeeWorkdays (Empleado con sus Jornadas)
```csharp
{
  "employeeId": 1,
  "employeeName": "Juan Pérez",
  "workdays": ["2024-01-15", "2024-01-16", "2024-01-17"]
}
```

## 🛠️ Tecnología

- **.NET 8.0**
- **Entity Framework Core 8.0.0**
- **SQLite**
- **ASP.NET Core 8.0**
- **Swagger/OpenAPI**

## 📁 Arquitectura

```
src/
├── EmployeeAPI.Api/              # Presentación (Controllers, Program.cs)
├── EmployeeAPI.Application/      # Lógica de aplicación (Services, DTOs)
├── EmployeeAPI.Domain/           # Entidades del dominio
└── EmployeeAPI.Infrastructure/   # Acceso a datos (Repositories, DbContext)
```

## ✅ Próximos Pasos

1. Agregar validación con FluentValidation
2. Agregar autenticación y autorización
3. Implementar logging
4. Agregar tests unitarios e integración
5. Dockerizar la aplicación
