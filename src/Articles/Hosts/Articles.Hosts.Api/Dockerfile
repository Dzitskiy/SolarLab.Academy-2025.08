FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Articles/Hosts/Articles.Hosts.Api/Articles.Hosts.Api.csproj", "src/Articles/Hosts/Articles.Hosts.Api/"]
COPY ["src/Articles/Infrastructure/Articles.Infrastructure.ComponentRegistrar/Articles.Infrastructure.ComponentRegistrar.csproj", "src/Articles/Infrastructure/Articles.Infrastructure.ComponentRegistrar/"]
COPY ["src/Articles/Application/Articles.AppServices/Articles.AppServices.csproj", "src/Articles/Application/Articles.AppServices/"]
COPY ["src/Articles/Contracts/Articles.Contracts/Articles.Contracts.csproj", "src/Articles/Contracts/Articles.Contracts/"]
COPY ["src/Articles/Domain/Articles.Domain/Articles.Domain.csproj", "src/Articles/Domain/Articles.Domain/"]
COPY ["src/Articles/Infrastructure/Articles.Infrastructure.DataAccess/Articles.Infrastructure.DataAccess.csproj", "src/Articles/Infrastructure/Articles.Infrastructure.DataAccess/"]
COPY ["src/Articles/Infrastructure/Articles.Infrastructure/Articles.Infrastructure.csproj", "src/Articles/Infrastructure/Articles.Infrastructure/"]
RUN dotnet restore "src/Articles/Hosts/Articles.Hosts.Api/Articles.Hosts.Api.csproj"
COPY . .
WORKDIR "/src/src/Articles/Hosts/Articles.Hosts.Api"
RUN dotnet build "./Articles.Hosts.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Articles.Hosts.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Articles.Hosts.Api.dll"]
