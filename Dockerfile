# Etapa de build - usando Alpine Linux para una imagen más ligera
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

# Copiar solo el archivo de proyecto primero y restaurar dependencias
# Esto mejora el uso de caché de Docker
COPY ["*.csproj", "./"]
RUN dotnet restore

# Copiar el resto del código fuente
COPY . .
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Etapa de runtime - usando Alpine Linux para una imagen más ligera
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app

# Configuración del contenedor
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Copiar los archivos publicados
COPY --from=build /app/publish .

# Configurar el usuario no-root por seguridad
USER $APP_UID

ENTRYPOINT ["dotnet", "CalcService.dll"]
