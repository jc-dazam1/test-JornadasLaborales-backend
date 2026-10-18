# 🐳 Guía Docker - EmployeeAPI

## Requisitos Previos

- [Docker](https://www.docker.com/products/docker-desktop) instalado
- [Docker Compose](https://docs.docker.com/compose/install/) instalado (incluido en Docker Desktop)

## Opción 1: Docker Compose (Recomendado)

### Paso 1: Construir la imagen

```bash
docker-compose build
```

### Paso 2: Ejecutar los servicios

```bash
docker-compose up
```

o en segundo plano:

```bash
docker-compose up -d
```

### Paso 3: Acceder a la API

- **Swagger UI**: http://localhost:5001/swagger
- **API Base URL**: http://localhost:5001

### Detener los servicios

```bash
docker-compose down
```

Ver logs en vivo:

```bash
docker-compose logs -f employeeapi
```

## Opción 2: Docker Manual

### Paso 1: Construir la imagen

```bash
docker build -t employeeapi:latest .
```

### Paso 2: Ejecutar el contenedor

```bash
docker run -d \
  --name employeeapi \
  -p 5001:80 \
  -e ASPNETCORE_ENVIRONMENT=Development \
  -v $(pwd)/data:/app/data \
  employeeapi:latest
```

**En Windows (PowerShell)**:
```powershell
docker run -d `
  --name employeeapi `
  -p 5001:80 `
  -e ASPNETCORE_ENVIRONMENT=Development `
  -v ${PWD}/data:/app/data `
  employeeapi:latest
```

### Paso 3: Ver logs

```bash
docker logs employeeapi
docker logs -f employeeapi  # En vivo
```

### Paso 4: Detener el contenedor

```bash
docker stop employeeapi
docker rm employeeapi
```

## Estructura de archivos en Docker

```
Container: employeeapi
├── /app
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── EmployeeAPI.Api.dll
│   ├── EmployeeAPI.Application.dll
│   ├── EmployeeAPI.Domain.dll
│   ├── EmployeeAPI.Infrastructure.dll
│   └── data/
│       └── employees.db (persistente)
└── /app/wwwroot (archivos estáticos)
```

## Variables de Entorno

| Variable | Valor | Descripción |
|----------|-------|-------------|
| `ASPNETCORE_ENVIRONMENT` | Development/Production | Entorno de la aplicación |
| `ConnectionStrings__DefaultConnection` | Data Source=... | Cadena de conexión |
| `Logging__LogLevel__Default` | Information/Debug | Nivel de logs |

## Puertos

| Puerto | Tipo | Descripción |
|--------|------|-------------|
| 5001 | HTTP | API HTTP |
| 5002 | HTTPS | API HTTPS |

## Volúmenes

La base de datos SQLite se persiste en el volumen `./data/` en tu máquina local.

```bash
# Ver volúmenes
docker volume ls

# Inspeccionar volumen
docker volume inspect <volume_name>

# Limpiar datos
rm -rf data/employees.db
```

## Red Docker

Docker Compose crea automáticamente una red `employeeapi-network` donde los servicios pueden comunicarse.

Para conectar otros contenedores:
```bash
docker network connect employeeapi-network <container_name>
```

## Verificar que esté funcionando

```bash
# Verificar que el contenedor está ejecutándose
docker ps

# Probar la conexión
curl http://localhost:5001/api/employees

# Acceder a Swagger
# Abrir navegador en: http://localhost:5001/swagger
```

## Troubleshooting

### "Port 5001 is already allocated"

```bash
# Opción 1: Usar otro puerto
docker run -p 5003:80 employeeapi:latest

# Opción 2: Detener el contenedor anterior
docker stop employeeapi
docker rm employeeapi

# Opción 3: Ver qué proceso usa el puerto
netstat -ano | findstr :5001  # Windows
lsof -i :5001                 # Linux/Mac
```

### "Database is locked"

```bash
# Eliminar archivos de base de datos
rm data/employees.db*

# Reiniciar el contenedor
docker restart employeeapi
```

### Ver errores en tiempo real

```bash
docker logs -f employeeapi --tail 50
```

### Entrar en el contenedor interactivamente

```bash
docker exec -it employeeapi bash
```

## Performance y Optimización

### Limitar recursos

```bash
docker run -d \
  --name employeeapi \
  --memory=512m \
  --cpus="0.5" \
  -p 5001:80 \
  employeeapi:latest
```

### Health Check

```yaml
# En docker-compose.yml
services:
  employeeapi:
	healthcheck:
	  test: ["CMD", "curl", "-f", "http://localhost/api/employees"]
	  interval: 30s
	  timeout: 10s
	  retries: 3
	  start_period: 40s
```

## Desarrollo Local vs Docker

| Aspecto | Local | Docker |
|---------|-------|--------|
| Velocidad | Más rápido | Ligeramente más lento |
| Debugging | Fácil con IDE | Requiere herramientas extra |
| Consistencia | Depende del SO | Idéntico en cualquier lugar |
| Database | SQLite local | Persistente en volumen |

## Producción

### Cambiar entorno

```bash
docker-compose -f docker-compose.yml -f docker-compose.prod.yml up -d
```

### Sin volúmenes locales (producción)

```yaml
services:
  employeeapi:
	environment:
	  - ASPNETCORE_ENVIRONMENT=Production
	  - ConnectionStrings__DefaultConnection=Server=db;Database=employeedb
```

## Más Recursos

- [Docker Docs](https://docs.docker.com/)
- [Docker Compose Docs](https://docs.docker.com/compose/)
- [Best Practices](https://docs.docker.com/develop/dev-best-practices/)

---

**Tip**: Para desarrollo rápido, ejecuta localmente con `dotnet run`. Para testing/deployment, usa Docker.
