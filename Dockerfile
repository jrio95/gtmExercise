FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ./src/GtMotive.Estimate.Microservice.Api ./src/GtMotive.Estimate.Microservice.Api/
COPY ./src/GtMotive.Estimate.Microservice.Host ./src/GtMotive.Estimate.Microservice.Host/
COPY ./src/GtMotive.Estimate.Microservice.ApplicationCore ./src/GtMotive.Estimate.Microservice.ApplicationCore/
COPY ./src/GtMotive.Estimate.Microservice.Domain ./src/GtMotive.Estimate.Microservice.Domain/
COPY ./src/GtMotive.Estimate.Microservice.Infrastructure ./src/GtMotive.Estimate.Microservice.Infrastructure/

COPY ./src/microservice.sln ./src/

WORKDIR /src/src/GtMotive.Estimate.Microservice.Host
RUN dotnet restore GtMotive.Estimate.Microservice.Host.csproj
RUN dotnet build -c Release && \
    dotnet publish -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "GtMotive.Estimate.Microservice.Host.dll"]
