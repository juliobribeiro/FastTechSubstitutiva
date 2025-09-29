# Etapa base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8081

# Etapa build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY FastTechKitchen/FasTechKitchenApi/FasTechKitchen.Api.csproj FasTechKitchenApi/
COPY FastTechKitchen/FastTechKitchenApplication/FastTechKitchen.Application.csproj FastTechKitchenApplication/
COPY FastTechKitchen/FastTechKitchen.Domain/FastTechKitchen.Domain.csproj FastTechKitchen.Domain/
COPY FastTechKitchen/FastTechKitchen.Infraestructure/FastTechKitchen.Infraestructure.csproj FastTechKitchen.Infraestructure/

RUN dotnet restore FasTechKitchenApi/FasTechKitchen.Api.csproj

COPY FastTechKitchen/FasTechKitchenApi/ FasTechKitchenApi/
COPY FastTechKitchen/FastTechKitchenApplication/ FastTechKitchenApplication/
COPY FastTechKitchen/FastTechKitchen.Domain/ FastTechKitchen.Domain/
COPY FastTechKitchen/FastTechKitchen.Infraestructure/ FastTechKitchen.Infraestructure/

WORKDIR /src/FasTechKitchenApi
RUN dotnet build "FasTechKitchen.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FasTechKitchen.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FasTechKitchen.Api.dll"]
