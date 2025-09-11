FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Evently.API/Evently.API.csproj", "src/Evently.API/"]
COPY ["src/Evently.Application/Evently.Application.csproj", "src/Evently.Application/"]
COPY ["src/Evently.Domain/Evently.Domain.csproj", "src/Evently.Domain/"]
COPY ["src/Evently.Infrastructure/Evently.Infrastructure.csproj", "src/Evently.Infrastructure/"]
RUN dotnet restore "src/Evently.API/Evently.API.csproj"
COPY . .
WORKDIR "/src/src/Evently.API"
RUN dotnet build "./Evently.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Evently.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Evently.API.dll"]
