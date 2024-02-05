FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["gateways/OcelotApiGateway/OcelotApiGateway.csproj", "gateways/OcelotApiGateway/"]
RUN dotnet restore "gateways/OcelotApiGateway/OcelotApiGateway.csproj"
COPY ./gateways/ ./gateways
WORKDIR "/src/gateways/OcelotApiGateway"
RUN dotnet build "./OcelotApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OcelotApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OcelotApiGateway.dll"]