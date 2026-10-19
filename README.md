# Employee API

API REST para gestión de empleados y jornadas laborales con arquitectura limpia en capas.

## 🎯 Características

- ✅ CRUD completo de empleados y jornadas
- ✅ Base de datos SQLite con migraciones automáticas
- ✅ Entity Framework Core 10
- ✅ Arquitectura limpia en capas (Domain, Application, Infrastructure, Api)
- ✅ Documentación con Swagger/OpenAPI
- ✅ .NET 10 con ASP.NET Core
- ✅ Tests unitarios e integración
- ✅ Docker y Docker Compose listos
- ✅ Health checks integrados

## 🚀 Inicio Rápido

### Opción 1: Ejecutar con Docker Compose (Recomendado)

```bash
# Clonar el repositorio
git clone https://github.com/jc-dazam1/test-JornadasLaborales-backend.git
cd test-JornadasLaborales-backend

# Ejecutar con Docker Compose
docker-compose up -d

# Acceder a la API
# Swagger UI: http://localhost:5000/swagger/index.html
# API: http://localhost:5000
```

### Opción 2: Ejecutar localmente sin Docker

```bash
# Restaurar dependencias
dotnet restore

# Ejecutar migraciones
dotnet ef database update -p src/EmployeeAPI.Infrastructure -s src/EmployeeAPI.Api

# Ejecutar la aplicación
dotnet run --project src/EmployeeAPI.Api
```

### Opción 3: Usar Make (si tienes make instalado)

```bash
# Ver todos los comandos disponibles
make help

# Construir e iniciar
make up

# Ver logs
make logs

# Detener
make down
```

## 📦 Docker

### Comandos principales de Docker

```bash
# Construir imagen
docker build -t employeeapi:latest .

# Ejecutar contenedor
docker run -p 5000:5000 employeeapi:latest

# Con docker-compose
docker-compose up -d      # Iniciar
docker-compose down        # Detener
docker-compose logs -f     # Ver logs
```

### Configuración con Variables de Entorno

```bash
# En docker-compose.yml o mediante -e
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://+:5000
ConnectionStrings__DefaultConnection=Data Source=/app/data/employees.db
```

### Volúmenes

Los datos persisten en el volumen `employeeapi_data`. Para acceder a la base de datos:

```bash
docker cp employeeapi:/app/data/employees.db ./employees.db
```

Para más información de Docker, ver [DOCKER_GUIDE.md](./DOCKER_GUIDE.md)

## 🔌 API

### URL Base
- **Desarrollo**: `http://localhost:5000`
- **Producción**: `/api`

### Documentación Interactiva
- Swagger UI: `http://localhost:5000/swagger/index.html`

### Endpoints principales

#### Empleados
- `GET /api/employees` - Obtener todos los empleados
- `GET /api/employees/{id}` - Obtener un empleado por ID
- `POST /api/employees` - Crear un nuevo empleado
- `PUT /api/employees/{id}` - Actualizar un empleado
- `DELETE /api/employees/{id}` - Eliminar un empleado
- `GET /api/employees/{id}/workdays` - Obtener jornadas del empleado

#### Jornadas (Workdays)
- `GET /api/workdays` - Obtener todas las jornadas
- `GET /api/workdays/{id}` - Obtener una jornada por ID
- `GET /api/workdays/employee/{employeeId}` - Obtener jornadas de un empleado
- `POST /api/workdays` - Crear una nueva jornada
- `PUT /api/workdays/{id}` - Actualizar una jornada
- `DELETE /api/workdays/{id}` - Eliminar una jornada

## 📋 Estructura del Proyecto

```
.
├── src/
│   ├── EmployeeAPI.Domain/           # Entidades del negocio
│   ├── EmployeeAPI.Application/      # Lógica de aplicación, DTOs, servicios
│   ├── EmployeeAPI.Infrastructure/   # Acceso a datos, DbContext, migraciones
│   ├── EmployeeAPI.Api/              # Controllers, Program.cs
│   ├── EmployeeAPI.Tests.Unit/       # Tests unitarios
│   └── EmployeeAPI.Tests.Integration/# Tests de integración
├── Dockerfile                         # Configuración Docker multi-stage
├── docker-compose.yml                 # Orquestación de contenedores
├── Makefile                           # Comandos de utilidad
├── docker-script.sh                   # Script de gestión (Linux/Mac)
└── .dockerignore                      # Archivos a ignorar en Docker
```

## 🛠️ Stack Tecnológico

| Componente | Versión |
|-----------|---------|
| .NET | 10.0 |
| ASP.NET Core | 10.0 |
| Entity Framework Core | 10.0.13 |
| SQLite | Incluido en EF Core |
| Swashbuckle (Swagger) | 6.4.0 |
| xUnit | 2.9.3 |
| Moq | Última versión |
| FluentAssertions | Última versión |
| Docker | Multi-stage build |

## 📊 Base de Datos

### Migraciones
Las migraciones se aplican automáticamente al iniciar la aplicación:

```bash
# Ver migraciones existentes
dotnet ef migrations list -p src/EmployeeAPI.Infrastructure -s src/EmployeeAPI.Api

# Agregar una nueva migración
dotnet ef migrations add "MigrationName" -p src/EmployeeAPI.Infrastructure -s src/EmployeeAPI.Api

# Revertir última migración
dotnet ef database update "PreviousMigration" -p src/EmployeeAPI.Infrastructure -s src/EmployeeAPI.Api
```

### Seed Data
Al iniciar, la aplicación carga datos de prueba automáticamente (DbContextSeed.cs)

## ✅ Testing

### Ejecutar tests
```bash
# Todos los tests
dotnet test

# Solo tests unitarios
dotnet test src/EmployeeAPI.Tests.Unit/EmployeeAPI.Tests.Unit.csproj

# Solo tests de integración
dotnet test src/EmployeeAPI.Tests.Integration/EmployeeAPI.Tests.Integration.csproj

# Con resultados detallados
dotnet test --verbosity detailed
```

## 🔧 Desarrollo Local

### Requisitos
- .NET SDK 10.0
- Visual Studio 2026 o Visual Studio Code
- Docker Desktop (opcional, para Docker)

### Setup inicial
```bash
# Clonar repositorio
git clone https://github.com/jc-dazam1/test-JornadasLaborales-backend.git
cd test-JornadasLaborales-backend

# Restaurar paquetes
dotnet restore

# Compilar
dotnet build

# Ejecutar migraciones
dotnet ef database update -p src/EmployeeAPI.Infrastructure -s src/EmployeeAPI.Api

# Ejecutar
dotnet run --project src/EmployeeAPI.Api
```

Acceder a: `http://localhost:5000/swagger/index.html`

## 📝 Variables de Entorno

Ver archivo [.env.example](./.env.example) para template de configuración.

```bash
# Copiar y personalizar
cp .env.example .env
```

## 🐛 Troubleshooting

### Puerto 5000 en uso
```bash
# Cambiar puerto en docker-compose.yml o
docker run -p 5001:5000 employeeapi:latest
```

### Errores de base de datos
```bash
# Limpiar y recrear
docker-compose down -v
docker-compose up -d
```

### Ver logs
```bash
# Local
dotnet run

# Docker
docker-compose logs -f employeeapi
docker logs employeeapi
```

## 📚 Documentación Adicional

- [DOCKER_GUIDE.md](./DOCKER_GUIDE.md) - Guía completa de Docker
- [Swagger OpenAPI](http://localhost:5000/swagger/index.html) - Documentación interactiva

## 👨‍💻 Desarrollo

### Rama actual
```bash
git branch -a
# feature-CREATEDOCKERFILE
```

### Commit
```bash
git add .
git commit -m "feat: Agregar Docker y documentación"
git push origin feature-CREATEDOCKERFILE
```

## 📄 Licencia

MIT License (c) 2025

## 👤 Autor

Juan Camilo Daza Martínez - [GitHub](https://github.com/jc-dazam1/test-JornadasLaborales-backend)

---

**Última actualización**: Enero 2025
