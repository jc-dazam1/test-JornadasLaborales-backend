# Usar imagen base de .NET SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
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
WORKDIR "/src/src/EmployeeAPI.Api"
RUN dotnet build "EmployeeAPI.Api.csproj" -c Release -o /app/build

# Publicar la aplicación
FROM build AS publish
RUN dotnet publish "EmployeeAPI.Api.csproj" -c Release -o /app/publish

# Usar imagen base de .NET Runtime para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .

# Exponer puerto
EXPOSE 80
EXPOSE 443

# Establecer punto de entrada
ENTRYPOINT ["dotnet", "EmployeeAPI.Api.dll"]
