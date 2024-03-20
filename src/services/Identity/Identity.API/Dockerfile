FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/Identity/Identity.API/Identity.API.csproj", "services/Identity/Identity.API/"]
COPY ["services/Identity/Identity.Services/Identity.Services.csproj", "services/Identity/Identity.Services/"]
COPY ["services/Identity/Identity.Domain/Identity.Domain.csproj", "services/Identity/Identity.Domain/"]
COPY ["services/Identity/Identity.Infrastructure/Identity.Infrastructure.csproj", "services/Identity/Identity.Infrastructure/"]
COPY ["services/Shared/Shared.csproj", "services/Shared/"]
RUN dotnet restore "services/Identity/Identity.API/Identity.API.csproj"
COPY ./services/Identity/ ./services/Identity/
COPY ./services/Shared/ ./services/Shared/
WORKDIR "/src/services/Identity/Identity.API"
RUN dotnet build "./Identity.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Identity.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.API.dll"]