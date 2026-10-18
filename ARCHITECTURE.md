# Arquitectura - EmployeeAPI

## Estructura de Capas (Clean Architecture)

La solución está organizada en **4 capas principales**, siguiendo los principios de Clean Architecture:

```
┌─────────────────────────────────────────────────────────┐
│                    API Layer (Presentación)             │
│              EmployeeAPI.Api                            │
│    - Controllers (EmployeesController)                  │
│    - Startup (Program.cs)                               │
│    - Configuration                                      │
└──────────────┬──────────────────────────────────────────┘
			   │ (referencias)
┌──────────────▼──────────────────────────────────────────┐
│              Application Layer (Lógica)                 │
│            EmployeeAPI.Application                      │
│    - Services (EmployeeService)                         │
│    - DTOs (Data Transfer Objects)                       │
│    - Repositories (Interfaces)                          │
└──────────────┬──────────────────────────────────────────┘
			   │ (referencias)
┌──────────────┴──────────────────────────────────────────┐
│                Domain Layer (Dominio)                   │
│              EmployeeAPI.Domain                         │
│    - Entities (Employee, Workday)                       │
│    - Business Logic                                     │
└─────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────┐
│            Infrastructure Layer (Persistencia)          │
│            EmployeeAPI.Infrastructure                   │
│    - DbContext (Entity Framework)                       │
│    - Repository Implementations                         │
│    - Database Configuration                             │
└─────────────────────────────────────────────────────────┘
```

## Capas Detalladas

### 1. **Domain Layer** (EmployeeAPI.Domain)
- **Responsabilidad**: Contiene la lógica de negocio pura
- **Independencia**: No depende de ninguna otra capa
- **Contenido**:
  - `Employee`: Entidad que representa un empleado
  - `Workday`: Entidad que representa los días laborales
  - Reglas y lógica de negocio

### 2. **Application Layer** (EmployeeAPI.Application)
- **Responsabilidad**: Implementa los casos de uso de la aplicación
- **Contenido**:
  - `IEmployeeService`: Interfaz de servicios de empleados
  - `EmployeeService`: Implementación de lógica de aplicación
  - DTOs: Para transferencia de datos entre capas
  - `IEmployeeRepository` e `IWorkdayRepository`: Interfaces para acceso a datos
- **Patrón**: Abstrae el acceso a datos mediante interfaces

### 3. **Infrastructure Layer** (EmployeeAPI.Infrastructure)
- **Responsabilidad**: Implementa la persistencia y acceso a datos
- **Contenido**:
  - `EmployeeDbContext`: DbContext de Entity Framework
  - `EmployeeRepository`, `WorkdayRepository`: Implementaciones de repositorios
  - Configuración de base de datos
- **Tecnología**: Entity Framework Core + SQLite

### 4. **API Layer / Presentación** (EmployeeAPI.Api)
- **Responsabilidad**: Expone los endpoints HTTP
- **Contenido**:
  - `EmployeesController`: Controlador REST
  - `Program.cs`: Configuración de servicios e inyección de dependencias
  - `appsettings.json`: Configuración de la aplicación
- **Framework**: ASP.NET Core

## Patrones Implementados

### 1. **Dependency Injection (DI)**
```csharp
// En Program.cs
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
```
- Facilita testabilidad
- Desacoplamiento entre capas

### 2. **Repository Pattern**
- Abstrae el acceso a datos
- Permite cambiar de tecnología de persistencia sin afectar la lógica

### 3. **DTO Pattern**
- Separación entre modelos de dominio y datos transferidos
- Previene exposición innecesaria de entidades

### 4. **Separation of Concerns**
- Cada capa tiene responsabilidades claramente definidas
- Facilita mantenimiento y escalabilidad

## Flujo de Solicitud

```
1. Cliente HTTP
   ↓
2. EmployeesController (API Layer)
   ├─ Recibe la solicitud
   ├─ Valida entrada
   ↓
3. EmployeeService (Application Layer)
   ├─ Implementa lógica de negocio
   ├─ Mapea DTOs a Entities
   ↓
4. EmployeeRepository (Infrastructure Layer)
   ├─ Accede a la base de datos
   ├─ Retorna datos
   ↓
5. Entity (Domain Layer)
   └─ Lógica de negocio pura
   ↓
6. Respuesta HTTP al Cliente
```

## Tecnologías

- **.NET 8.0**: Framework
- **ASP.NET Core**: Web API
- **Entity Framework Core 8.0**: ORM
- **SQLite**: Base de datos
- **Swagger**: Documentación de API

## Beneficios de esta Arquitectura

✅ **Testeable**: Cada capa puede ser testeada independientemente  
✅ **Mantenible**: Cambios en una capa no afectan otras  
✅ **Escalable**: Fácil agregar nuevas funcionalidades  
✅ **Flexible**: Cambiar tecnologías sin afectar lógica  
✅ **Clean Code**: Código limpio y bien organizado  
