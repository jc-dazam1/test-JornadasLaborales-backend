# Guía de Inicio Rápido - Docker

Esta guía te ayuda a ejecutar la aplicación inmediatamente.

## ⚡ 30 Segundos de Inicio

```bash
# 1. Clonar (si no lo has hecho)
git clone https://github.com/jc-dazam1/test-JornadasLaborales-backend.git
cd test-JornadasLaborales-backend

# 2. Ejecutar
docker-compose up -d

# 3. Acceder
# Abre en tu navegador: http://localhost:5000/swagger/index.html
```

**¡Listo!** La API está disponible en `http://localhost:5000`

---

## 🎯 Comandos Más Útiles

| Comando | Descripción |
|---------|-------------|
| `docker-compose up -d` | Iniciar la aplicación |
| `docker-compose down` | Detener la aplicación |
| `docker-compose logs -f` | Ver logs en tiempo real |
| `docker-compose ps` | Ver estado de contenedores |
| `docker-compose down -v` | Limpiar todo incluyendo datos |

**Con Make:**
```bash
make up      # Iniciar
make down    # Detener
make logs    # Ver logs
make clean   # Limpiar todo
```

---

## 🌐 Acceso Inmediato

### Swagger UI (Documentación interactiva)
👉 **http://localhost:5000/swagger/index.html**

### API Base
👉 **http://localhost:5000/api**

### Ejemplos de endpoints
- `GET http://localhost:5000/api/employees` - Obtener empleados
- `POST http://localhost:5000/api/employees` - Crear empleado

---

## 📋 Requisitos

✅ Docker Desktop instalado
✅ Docker Compose (incluido en Docker Desktop)

**¿No tienes Docker?** Alterna a ejecución local:
```bash
dotnet run --project src/EmployeeAPI.Api
```

---

## 🆘 Problemas Comunes

### El contenedor no inicia
```bash
docker-compose logs employeeapi
```

### Puerto 5000 en uso
```bash
# Cambiar puerto en docker-compose.yml (línea 10)
ports:
  - "5001:5000"  # Cambiar 5001 por otro puerto libre
```

### Limpiar y empezar de nuevo
```bash
docker-compose down -v
docker-compose up -d
```

---

## 📚 Más Información

- Ver [README.md](./README.md) para documentación completa
- Ver [DOCKER_GUIDE.md](./DOCKER_GUIDE.md) para guía avanzada de Docker
- Ver [Makefile](./Makefile) para todos los comandos disponibles

---

**¡Diviértete desarrollando! 🚀**
