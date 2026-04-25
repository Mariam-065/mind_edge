<<<<<<< HEAD
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
=======
﻿# بنستخدم نسخة الـ SDK عشان نبني الكود
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# بننسخ ملف المشروع وبنعمل Restore للمكتبات
COPY ["MindEdge-1/MindEdge-1.csproj", "MindEdge-1/"]
RUN dotnet restore "MindEdge-1/MindEdge-1.csproj"

# بننسخ باقي الكود وبنعمل بناء (Build)
COPY . .
WORKDIR "/src/MindEdge-1"
RUN dotnet build "MindEdge-1.csproj" -c Release -o /app/build

# بنطلع النسخة النهائية اللي هتشتغل (Publish)
FROM build AS publish
RUN dotnet publish "MindEdge-1.csproj" -c Release -o /app/publish

# المرحلة الأخيرة: تشغيل الـ API
FROM mcr.microsoft.com/dotnet/aspnet:8.0
>>>>>>> 3120eca5c7d2ea1dadb32d8f454c4fa984a18b3a
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MindEdge-1.dll"]