FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["MindEdge-1.csproj", "."]
RUN dotnet restore "./MindEdge-1.csproj"
COPY . .
RUN dotnet build "./MindEdge-1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./MindEdge-1.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MindEdge-1.dll"]