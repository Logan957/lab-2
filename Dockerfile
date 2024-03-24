FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY lab-2/lab-2.csproj .
RUN dotnet restore lab-2.csproj

COPY *.csproj ./
RUN dotnet restore
 
COPY . ./
RUN dotnet publish -c Release -o out
 
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
 
# Запускаем приложение
ENTRYPOINT ["dotnet", "lab-2.dll"]