FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["MindEdge-1/MindEdge-1.csproj", "MindEdge-1/"]
RUN dotnet restore "MindEdge-1/MindEdge-1.csproj"

COPY . .
WORKDIR "/src/MindEdge-1"
RUN dotnet publish "MindEdge-1.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "MindEdge-1.dll"]