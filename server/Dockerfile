# syntax=docker/dockerfile:1

# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as base
EXPOSE 80
EXPOSE 443

# SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Copy the source project files and download dependencies
WORKDIR /src
COPY *.sln ./
COPY */*.csproj ./
RUN dotnet restore

# Copy the source and build it
COPY . ./
CMD dotnet build

# Publish the app
FROM build AS publish
RUN dotnet publish -c Release -o /publish

FROM base as final
WORKDIR /app
COPY --from=publish /publish ./
ENTRYPOINT dotnet Web.dll
