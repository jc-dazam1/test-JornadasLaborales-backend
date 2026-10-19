#!/usr/bin/env bash

# Script de verificación del setup Docker
# Verifica que todo esté instalado y configurado correctamente

echo "🔍 Verificando setup Docker de EmployeeAPI..."
echo ""

# Color codes
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

errors=0

# Función para validar comando
check_command() {
	if command -v $1 &> /dev/null; then
		version=$($1 --version 2>&1 | head -n 1)
		echo -e "${GREEN}✓${NC} $1: $version"
	else
		echo -e "${RED}✗${NC} $1: NO ENCONTRADO"
		errors=$((errors+1))
	fi
}

# Función para validar archivo
check_file() {
	if [ -f "$1" ]; then
		size=$(wc -c < "$1")
		echo -e "${GREEN}✓${NC} $1 ($(numfmt --to=iec-i --suffix=B $size 2>/dev/null || echo "$size bytes"))"
	else
		echo -e "${RED}✗${NC} $1: NO ENCONTRADO"
		errors=$((errors+1))
	fi
}

# Función para validar volumen Docker
check_volume() {
	if docker volume inspect $1 &> /dev/null; then
		echo -e "${GREEN}✓${NC} Volumen Docker '$1' existe"
	else
		echo -e "${YELLOW}⚠${NC} Volumen Docker '$1' no existe (se creará al iniciar)"
	fi
}

echo "📋 Verificando herramientas:"
echo "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
check_command "git"
check_command "dotnet"
check_command "docker"
check_command "docker-compose"
echo ""

echo "📁 Verificando archivos de configuración:"
echo "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
check_file "Dockerfile"
check_file "docker-compose.yml"
check_file ".dockerignore"
check_file ".env.example"
check_file "Makefile"
check_file "docker-script.sh"
echo ""

echo "📚 Verificando documentación:"
echo "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
check_file "README.md"
check_file "DOCKER_GUIDE.md"
check_file "QUICK_START.md"
check_file "SETUP_COMPLETE.md"
echo ""

echo "🐳 Verificando Docker:"
echo "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
docker ps > /dev/null 2>&1 && echo -e "${GREEN}✓${NC} Docker daemon está ejecutándose" || echo -e "${RED}✗${NC} Docker daemon NO está ejecutándose"
check_volume "employeeapi_data"
echo ""

echo "🐙 Verificando Git:"
echo "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
current_branch=$(git branch --show-current 2>/dev/null)
if [ ! -z "$current_branch" ]; then
	echo -e "${GREEN}✓${NC} Rama actual: $current_branch"
	echo -e "${GREEN}✓${NC} Remote: $(git remote get-url origin 2>/dev/null || echo 'N/A')"
else
	echo -e "${RED}✗${NC} No se pudo obtener información de Git"
	errors=$((errors+1))
fi
echo ""

echo ".NET Verificando solución:"
echo "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
if [ -f "EmployeeAPI.sln" ]; then
	echo -e "${GREEN}✓${NC} Solución encontrada"
	echo "  Proyectos:"
	dotnet sln EmployeeAPI.sln list 2>/dev/null | grep -v "^$" | sed 's/^/    - /'
else
	echo -e "${RED}✗${NC} Solución no encontrada"
	errors=$((errors+1))
fi
echo ""

echo "✅ Verificación completada"
echo "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"

if [ $errors -eq 0 ]; then
	echo -e "${GREEN}🎉 TODO VERIFICADO CORRECTAMENTE${NC}"
	echo ""
	echo "Próximos pasos:"
	echo "  1. docker-compose up -d"
	echo "  2. Abrir http://localhost:5000/swagger/index.html"
	echo "  3. ¡Comenzar a desarrollar!"
else
	echo -e "${YELLOW}⚠ Se encontraron $errors problema(s)${NC}"
	echo ""
	if ! command -v docker &> /dev/null; then
		echo "📦 Instalar Docker:"
		echo "   https://docs.docker.com/get-docker/"
	fi
	if ! command -v dotnet &> /dev/null; then
		echo "📦 Instalar .NET SDK 10:"
		echo "   https://dotnet.microsoft.com/download"
	fi
fi
echo ""
