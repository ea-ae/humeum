# syntax=docker/dockerfile:1

# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as base
EXPOSE 80
EXPOSE 443

# SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Restore dependencies and build project
WORKDIR /build
COPY . ./
RUN dotnet build src/Web

# Publish the app
FROM build AS publish
RUN dotnet publish src/Web -c Release -o /publish

FROM base as app
WORKDIR /app
COPY --from=publish /publish ./
ENTRYPOINT dotnet Web.dll
