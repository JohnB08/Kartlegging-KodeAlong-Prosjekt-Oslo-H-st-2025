FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Kartleggign_LiveChat_Applikasjon/Kartleggign_LiveChat_Applikasjon.csproj", "Kartleggign_LiveChat_Applikasjon/"]
RUN dotnet restore "Kartleggign_LiveChat_Applikasjon/Kartleggign_LiveChat_Applikasjon.csproj"
COPY . .
WORKDIR "/src/Kartleggign_LiveChat_Applikasjon"
RUN dotnet build "./Kartleggign_LiveChat_Applikasjon.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Kartleggign_LiveChat_Applikasjon.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kartleggign_LiveChat_Applikasjon.dll"]
