#!/bin/bash

# Script para gestionar la aplicación EmployeeAPI con Docker

set -e

# Colores para salida
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Función de ayuda
show_help() {
	cat << EOF
Uso: ./docker-script.sh [COMANDO]

Comandos disponibles:
	build       Construir la imagen Docker
	up          Iniciar la aplicación con docker-compose
	down        Detener la aplicación
	logs        Ver logs de la aplicación
	ps          Ver estado de los contenedores
	clean       Limpiar contenedores, volúmenes e imágenes
	rebuild     Reconstruir y reiniciar la aplicación
	shell       Acceder a la terminal del contenedor
	help        Mostrar esta ayuda

Ejemplos:
	./docker-script.sh build
	./docker-script.sh up
	./docker-script.sh logs
	./docker-script.sh down
EOF
}

# Función para imprimir con color
print_info() {
	echo -e "${GREEN}[INFO]${NC} $1"
}

print_error() {
	echo -e "${RED}[ERROR]${NC} $1"
}

print_warning() {
	echo -e "${YELLOW}[ADVERTENCIA]${NC} $1"
}

# Verificar si Docker está disponible
check_docker() {
	if ! command -v docker &> /dev/null; then
		print_error "Docker no está instalado o no está en el PATH"
		exit 1
	fi
	print_info "Docker encontrado: $(docker --version)"
}

# Construir imagen
build_image() {
	print_info "Construyendo imagen Docker..."
	docker build -t employeeapi:latest .
	print_info "Imagen construida exitosamente"
}

# Iniciar aplicación
start_app() {
	print_info "Iniciando aplicación con docker-compose..."
	docker-compose up -d
	print_info "Esperando a que la aplicación se inicie..."
	sleep 5
	print_info "Aplicación iniciada. Acceso en http://localhost:5000"
	print_info "Swagger UI disponible en http://localhost:5000/swagger/index.html"
}

# Detener aplicación
stop_app() {
	print_info "Deteniendo aplicación..."
	docker-compose down
	print_info "Aplicación detenida"
}

# Ver logs
show_logs() {
	print_info "Mostrando logs (Ctrl+C para salir)..."
	docker-compose logs -f employeeapi
}

# Ver estado
show_ps() {
	print_info "Estado de los contenedores:"
	docker-compose ps
}

# Limpiar
cleanup() {
	print_warning "Se eliminarán contenedores, volúmenes e imágenes"
	read -p "¿Deseas continuar? (s/n): " -n 1 -r
	echo
	if [[ $REPLY =~ ^[Ss]$ ]]; then
		print_info "Deteniendo contenedores..."
		docker-compose down -v 2>/dev/null || true
		print_info "Eliminando imagen..."
		docker rmi employeeapi:latest 2>/dev/null || true
		print_info "Limpieza completada"
	else
		print_info "Limpieza cancelada"
	fi
}

# Reconstruir
rebuild() {
	print_info "Reconstruyendo aplicación..."
	stop_app
	build_image
	start_app
}

# Shell
enter_shell() {
	print_info "Accediendo a la terminal del contenedor..."
	docker-compose exec employeeapi /bin/bash
}

# Comando principal
main() {
	check_docker

	case "${1:-help}" in
		build)
			build_image
			;;
		up)
			start_app
			;;
		down)
			stop_app
			;;
		logs)
			show_logs
			;;
		ps)
			show_ps
			;;
		clean)
			cleanup
			;;
		rebuild)
			rebuild
			;;
		shell)
			enter_shell
			;;
		help|--help|-h)
			show_help
			;;
		*)
			print_error "Comando desconocido: $1"
			show_help
			exit 1
			;;
	esac
}

main "$@"
