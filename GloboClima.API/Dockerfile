# Base usada para execução no container final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# ARG opcional caso queira manter uso de usuário customizado
ARG APP_UID=1000
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Fase de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiando arquivos de projeto
COPY ["GloboClima.API/GloboClima.API.csproj", "GloboClima.API/"]
COPY ["GloboClima.Servico/GloboClima.Servico.csproj", "GloboClima.Servico/"]
COPY ["GloboClima.Dominio/GloboClima.Dominio.csproj", "GloboClima.Dominio/"]

# Restaurando pacotes
RUN dotnet restore "./GloboClima.API/GloboClima.API.csproj"

# Copiando tudo para o container de build
COPY . .

# Compilando o projeto
WORKDIR "/src/GloboClima.API"
RUN dotnet build "./GloboClima.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicação da aplicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GloboClima.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Início da aplicação
ENTRYPOINT ["dotnet", "GloboClima.API.dll"]
