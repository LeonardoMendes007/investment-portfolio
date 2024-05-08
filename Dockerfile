#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:8000;http://+:80;
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/InvestmentPortfolio.API/InvestmentPortfolio.API.csproj", "src/InvestmentPortfolio.API/"]
COPY ["src/InvestmentPortfolio.CrossCutting/InvestmentPortfolio.CrossCutting.csproj", "src/InvestmentPortfolio.CrossCutting/"]
COPY ["src/InvestmentPortfolio.Application/InvestmentPortfolio.Application.csproj", "src/InvestmentPortfolio.Application/"]
COPY ["src/InvestmentPortfolio.Domain/InvestmentPortfolio.Domain.csproj", "src/InvestmentPortfolio.Domain/"]
COPY ["src/InvestmentPortfolio.Infra/InvestmentPortfolio.Infra.csproj", "src/InvestmentPortfolio.Infra/"]
COPY ["src/InvestmentPortfolio.Job/InvestmentPortfolio.Job.csproj", "src/InvestmentPortfolio.Job/"]
RUN dotnet restore "./src/InvestmentPortfolio.API/InvestmentPortfolio.API.csproj"
COPY . .
WORKDIR "/src/src/InvestmentPortfolio.API"
RUN dotnet build "./InvestmentPortfolio.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InvestmentPortfolio.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InvestmentPortfolio.API.dll"]