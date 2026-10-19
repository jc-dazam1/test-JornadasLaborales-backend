.PHONY: help build up down logs ps clean rebuild shell test

# Variables
IMAGE_NAME := employeeapi
CONTAINER_NAME := employeeapi
PORT := 5000

## Ayuda
help:
	@echo "EmployeeAPI - Docker Management"
	@echo ""
	@echo "Comandos disponibles:"
	@echo "  make build       - Construir la imagen Docker"
	@echo "  make up          - Iniciar la aplicación"
	@echo "  make down        - Detener la aplicación"
	@echo "  make logs        - Ver logs"
	@echo "  make ps          - Ver estado de contenedores"
	@echo "  make clean       - Limpiar todo (contenedores, volúmenes, imágenes)"
	@echo "  make rebuild     - Reconstruir y reiniciar"
	@echo "  make shell       - Acceder a terminal del contenedor"
	@echo "  make test        - Ejecutar tests"
	@echo "  make api         - Abrir Swagger UI en navegador"
	@echo ""

## Construir imagen
build:
	@echo "🔨 Construyendo imagen Docker..."
	docker build -t $(IMAGE_NAME):latest .
	@echo "✅ Imagen construida exitosamente"

## Iniciar aplicación
up: build
	@echo "🚀 Iniciando aplicación..."
	docker-compose up -d
	@echo "⏳ Esperando a que la aplicación se inicie..."
	@sleep 5
	@echo "✅ Aplicación iniciada en http://localhost:$(PORT)"
	@echo "📚 Swagger UI: http://localhost:$(PORT)/swagger/index.html"

## Detener aplicación
down:
	@echo "⏹️  Deteniendo aplicación..."
	docker-compose down
	@echo "✅ Aplicación detenida"

## Ver logs
logs:
	@echo "📋 Mostrando logs..."
	docker-compose logs -f $(CONTAINER_NAME)

## Ver estado
ps:
	@echo "📊 Estado de los contenedores:"
	docker-compose ps

## Limpiar todo
clean:
	@echo "🧹 Limpiando contenedores, volúmenes e imágenes..."
	docker-compose down -v 2>/dev/null || true
	docker rmi $(IMAGE_NAME):latest 2>/dev/null || true
	@echo "✅ Limpieza completada"

## Reconstruir y reiniciar
rebuild: clean up
	@echo "✅ Reconstrucción completada"

## Acceder a terminal
shell:
	@echo "🐚 Accediendo a la terminal del contenedor..."
	docker-compose exec $(CONTAINER_NAME) /bin/bash

## Ejecutar tests
test:
	@echo "🧪 Ejecutando tests..."
	dotnet test

## Abrir Swagger UI
api:
	@echo "🌐 Abriendo API Swagger UI..."
	@command -v open >/dev/null && open http://localhost:$(PORT)/swagger/index.html || \
	command -v xdg-open >/dev/null && xdg-open http://localhost:$(PORT)/swagger/index.html || \
	command -v start >/dev/null && start http://localhost:$(PORT)/swagger/index.html || \
	echo "Por favor abre http://localhost:$(PORT)/swagger/index.html en tu navegador"

## Información del proyecto
info:
	@echo "📦 EmployeeAPI - Información del Proyecto"
	@echo ""
	@echo "Proyectos:"
	@dotnet sln EmployeeAPI.sln list 2>/dev/null || echo "Solución no encontrada"
	@echo ""
	@echo "Docker:"
	@docker --version
	@docker-compose --version

## Ver versión de .NET
version:
	@dotnet --version

## Sincronizar cambios de código
sync: down build up
	@echo "✅ Cambios sincronizados"

## Status completo
status: info ps logs
	@echo ""
	@echo "✅ Status completado"
