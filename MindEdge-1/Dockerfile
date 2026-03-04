# دي المرحلة اللي بنجهز فيها الكود
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# بننسخ ملفات المشروع وبنعمل Restore للمكتبات
COPY *.csproj ./
RUN dotnet restore

# بننسخ باقي الملفات وبنعمل Publish
COPY . ./
RUN dotnet publish -c Release -o out

# دي المرحلة اللي بنشغل فيها الـ API
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# بنقول لدوكر يشغل ملف الـ DLL بتاعك
ENTRYPOINT ["dotnet", "MindEdge-1.dll"]