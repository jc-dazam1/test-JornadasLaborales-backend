# EmployeeAPI - Docker Setup Guide

## Descripción
Esta guía explica cómo ejecutar la aplicación EmployeeAPI usando Docker.

## Requisitos Previos
- Docker instalado y en ejecución
- Docker Compose instalado (opcional, pero recomendado)

## Opción 1: Ejecutar con Docker Compose (Recomendado)

### Paso 1: Ejecutar el contenedor
```bash
docker-compose up -d
```

La aplicación se iniciará automáticamente y estará disponible en `http://localhost:5000`

### Paso 2: Verificar estado
```bash
docker-compose ps
```

### Paso 3: Ver logs
```bash
docker-compose logs -f employeeapi
```

### Paso 4: Detener la aplicación
```bash
docker-compose down
```

---

## Opción 2: Ejecutar con Docker CLI

### Paso 1: Construir la imagen
```bash
docker build -t employeeapi:latest .
```

### Paso 2: Crear volumen para datos
```bash
docker volume create employeeapi_data
```

### Paso 3: Ejecutar el contenedor
```bash
docker run -d \
  --name employeeapi \
  -p 5000:5000 \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e ASPNETCORE_URLS=http://+:5000 \
  -v employeeapi_data:/app/data \
  employeeapi:latest
```

### Paso 4: Ver logs
```bash
docker logs -f employeeapi
```

### Paso 5: Detener el contenedor
```bash
docker stop employeeapi
docker rm employeeapi
```

---

## Acceso a la API

### Swagger UI (Documentación interactiva)
```
http://localhost:5000/swagger/index.html
```

### Endpoints disponibles
- `GET /api/employees` - Obtener todos los empleados
- `GET /api/employees/{id}` - Obtener un empleado por ID
- `POST /api/employees` - Crear un nuevo empleado
- `PUT /api/employees/{id}` - Actualizar un empleado
- `DELETE /api/employees/{id}` - Eliminar un empleado
- `GET /api/employees/{id}/workdays` - Obtener jornadas de un empleado

---

## Configuración de Variables de Entorno

Puedes personalizar el comportamiento de la aplicación usando variables de entorno:

### Con Docker Compose
Edita el archivo `docker-compose.yml` en la sección `environment`:

```yaml
environment:
  - ASPNETCORE_ENVIRONMENT=Production
  - ASPNETCORE_URLS=http://+:5000
  - ConnectionStrings__DefaultConnection=Data Source=/app/data/employees.db
```

### Con Docker CLI
Usa la opción `-e` al ejecutar:
```bash
docker run -d \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e ASPNETCORE_URLS=http://+:5000 \
  employeeapi:latest
```

---

## Base de Datos

### Ubicación
La base de datos SQLite se almacena en `/app/data/employees.db` dentro del contenedor.

Con Docker Compose, los datos persisten en el volumen `employeeapi_data`.

### Acceso a la base de datos
Para acceder a la base de datos usando una herramienta SQL, necesitarás extraer el archivo primero:

```bash
docker cp employeeapi:/app/data/employees.db ./employees.db
```

---

## Solución de Problemas

### El contenedor no inicia
```bash
docker logs employeeapi
```

### Puerto 5000 ya está en uso
Cambia el puerto en docker-compose.yml o usa:
```bash
docker run -p 5001:5000 employeeapi:latest
```

### Eliminar datos y empezar de nuevo
```bash
docker-compose down -v
docker-compose up -d
```

---

## Útiles

### Ver tamaño de la imagen
```bash
docker images employeeapi
```

### Limpiar imágenes no usadas
```bash
docker image prune
```

### Limpiar volúmenes no usados
```bash
docker volume prune
```

---

## Notas Importantes

- La aplicación usa .NET 10.0
- La base de datos se inicializa automáticamente con migraciones
- Los datos persisten entre reinicios si usas volúmenes
- El health check verifica la conectividad cada 30 segundos
