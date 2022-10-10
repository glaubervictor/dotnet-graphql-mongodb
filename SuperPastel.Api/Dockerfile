#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SuperPastel.Api/SuperPastel.Api.csproj", "SuperPastel.Api/"]
COPY ["SuperPastel.GraphQL/SuperPastel.GraphQL.csproj", "SuperPastel.GraphQL/"]
COPY ["SuperPastel.Dominio/SuperPastel.Dominio.csproj", "SuperPastel.Dominio/"]
COPY ["SuperPastel.Nucleo/SuperPastel.Nucleo.csproj", "SuperPastel.Nucleo/"]
COPY ["SuperPastel.Infra.InversaoDeControle/SuperPastel.Infra.InversaoDeControle.csproj", "SuperPastel.Infra.InversaoDeControle/"]
COPY ["SuperPastel.Infra.Dados/SuperPastel.Infra.Dados.csproj", "SuperPastel.Infra.Dados/"]
COPY ["SuperPastel.Infra.Transporte/SuperPastel.Infra.Transporte.csproj", "SuperPastel.Infra.Transporte/"]
RUN dotnet restore "SuperPastel.Api/SuperPastel.Api.csproj"
COPY . .
WORKDIR "/src/SuperPastel.Api"
RUN dotnet build "SuperPastel.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SuperPastel.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SuperPastel.Api.dll"]