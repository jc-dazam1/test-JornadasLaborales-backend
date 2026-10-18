# Desafío 1: API REST de Empleados y Jornadas Laborales

## Descripción

Desarrollar una **API REST** que implemente operaciones CRUD completas para:
1. **Empleados** (Create, Read, Update, Delete)
2. **Jornadas Laborales** (endpoint para consultar jornadas de un empleado)

## Estado Actual

✅ **Completado**: Estructura base de Clean Architecture
✅ **Completado**: Capa de Presentación (API)
✅ **Completado**: Capa de Aplicación (Servicios)
✅ **Completado**: Capa de Dominio (Entidades)
✅ **Completado**: Capa de Infraestructura (Persistencia)
✅ **Completado**: Configuración de Dependencias
✅ **Completado**: Base de Datos SQLite

## Endpoints Implementados

### Empleados ✅

| Método | Endpoint | Descripción | Estado |
|--------|----------|-------------|--------|
| `GET` | `/api/employees` | Obtener todos los empleados | ✅ |
| `GET` | `/api/employees/{id}` | Obtener empleado por ID | ✅ |
| `POST` | `/api/employees` | Crear nuevo empleado | ✅ |
| `PUT` | `/api/employees/{id}` | Actualizar empleado | ✅ |
| `DELETE` | `/api/employees/{id}` | Eliminar empleado | ✅ |
| `GET` | `/api/employees/workdays/{id}` | Obtener jornadas laborales | ✅ |

## Próximos Pasos (Desafío 2)

### Funcionalidades a Implementar

1. **Validaciones**
   - Validar campos requeridos
   - Validar formato de email
   - Validar que el empleado exista antes de actualizar/eliminar

2. **Manejo de Errores**
   - Responses de error consistentes
   - Códigos HTTP apropiados
   - Mensajes de error descriptivos

3. **Operaciones CRUD de Jornadas Laborales**
   - Crear jornada laboral
   - Leer jornadas laborales
   - Actualizar jornada laboral
   - Eliminar jornada laboral

4. **Mejoras de Seguridad**
   - Autenticación (JWT, OAuth, etc.)
   - Autorización (roles y permisos)
   - CORS

5. **Testing**
   - Pruebas unitarias
   - Pruebas de integración
   - Pruebas de endpoints

6. **Documentación**
   - Swagger/OpenAPI mejorado
   - Ejemplos de solicitudes
   - Descripción de errores

7. **Docker**
   - Dockerfile
   - Docker Compose
   - Instrucciones de deployment

## Requisitos de Calidad

- ✅ Clean Code
- ✅ Clean Architecture
- ✅ SOLID Principles
- ✅ Separation of Concerns
- ⏳ Comprehensive Testing
- ⏳ Complete Documentation

## Tecnologías Utilizadas

| Tecnología | Versión | Propósito |
|------------|---------|----------|
| .NET | 8.0 | Framework |
| ASP.NET Core | 8.0 | Web API |
| Entity Framework Core | 8.0 | ORM |
| SQLite | - | Base de Datos |
| Swagger | - | Documentación |

## Estructura de Datos

### Employee (Empleado)
```csharp
public class Employee
{
	public int Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string Position { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public ICollection<Workday> Workdays { get; set; }
}
```

### Workday (Jornada Laboral)
```csharp
public class Workday
{
	public int Id { get; set; }
	public int EmployeeId { get; set; }
	public DateTime Date { get; set; }
	public int HoursWorked { get; set; }
	public Employee Employee { get; set; }
}
```

## Reglas de Negocio

1. Un empleado debe tener un email único
2. Las jornadas laborales no pueden tener fechas duplicadas para el mismo empleado
3. Las horas trabajadas deben ser un número positivo (0-24)
4. No se puede eliminar un empleado que tenga jornadas laborales registradas (debe decidirse el comportamiento)

## Criterios de Aceptación

- [x] API expone correctamente endpoints CRUD
- [x] Base de datos persiste correctamente los datos
- [x] Código sigue Clean Architecture
- [x] Código es fácil de mantener y entender
- [ ] Código tiene pruebas completas
- [ ] API está completamente documentada
- [ ] Solución incluye Docker

## Referencias

- [Clean Code by Robert C. Martin](https://www.amazon.com/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350882)
- [Clean Architecture by Robert C. Martin](https://www.amazon.com/Clean-Architecture-Craftsmans-Software-Structure/dp/0134494164)
- [SOLID Principles](https://en.wikipedia.org/wiki/SOLID)
- [Repository Pattern](https://martinfowler.com/eaaCatalog/repository.html)
- [Dependency Injection](https://en.wikipedia.org/wiki/Dependency_injection)
