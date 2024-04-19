FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/Chat/Chat.API/Chat.API.csproj", "services/Chat/Chat.API/"]
COPY ["services/Chat/Chat.Services/Chat.Services.csproj", "services/Chat/Chat.Services/"]
COPY ["services/Chat/Chat.Interfaces/Chat.Interfaces.csproj", "services/Chat/Chat.Interfaces/"]
COPY ["services/Chat/Chat.Domain/Chat.Domain.csproj", "services/Chat/Chat.Domain/"]
COPY ["services/Chat/Chat.Infrastructure/Chat.Infrastructure.csproj", "services/Chat/Chat.Infrastructure/"]
COPY ["services/Shared.Messages/Shared.Messages.csproj", "services/Shared.Messages/"]
RUN dotnet restore "services/Chat/Chat.API/Chat.API.csproj"
COPY ./services/Chat/ ./services/Chat/
COPY ./services/Shared.Messages/ ./services/Shared.Messages/
WORKDIR "/src/services/Chat/Chat.API"
RUN dotnet build "./Chat.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Chat.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chat.API.dll"]