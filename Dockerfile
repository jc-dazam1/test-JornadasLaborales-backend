# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copiar archivos de proyecto
COPY ["src/EmployeeAPI.Api/EmployeeAPI.Api.csproj", "src/EmployeeAPI.Api/"]
COPY ["src/EmployeeAPI.Application/EmployeeAPI.Application.csproj", "src/EmployeeAPI.Application/"]
COPY ["src/EmployeeAPI.Domain/EmployeeAPI.Domain.csproj", "src/EmployeeAPI.Domain/"]
COPY ["src/EmployeeAPI.Infrastructure/EmployeeAPI.Infrastructure.csproj", "src/EmployeeAPI.Infrastructure/"]

# Restaurar dependencias
RUN dotnet restore "src/EmployeeAPI.Api/EmployeeAPI.Api.csproj"

# Copiar código fuente
COPY . .

# Construir la aplicación
RUN dotnet build "src/EmployeeAPI.Api/EmployeeAPI.Api.csproj" -c Release -o /app/build

# Publicar la aplicación
RUN dotnet publish "src/EmployeeAPI.Api/EmployeeAPI.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

# Copiar aplicación publicada desde la etapa de build
COPY --from=build /app/publish .

# Crear directorio para base de datos SQLite
RUN mkdir -p /app/data

# Exponer puertos
EXPOSE 5000
EXPOSE 5001

# Variables de entorno
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Production

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=40s --retries=3 \
    CMD dotnet --version || exit 1

# Punto de entrada
ENTRYPOINT ["dotnet", "EmployeeAPI.Api.dll"]
