FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY food-delivery-app.sln food-delivery-app.sln
COPY API/API.csproj src/API/API.csproj
COPY Application/Application.csproj src/Application/Application.csproj
COPY Core/Core.csproj src/Core/Core.csproj
COPY Infrastructure/Infrastructure.csproj src/Infrastructure/Infrastructure.csproj

WORKDIR /app/src/API
RUN dotnet restore

WORKDIR /app
COPY API/ src/API
COPY Application/ src/Application
COPY Core/ src/Core
COPY Infrastructure/ src/Infrastructure

WORKDIR /app/src/API
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "API.dll"]
