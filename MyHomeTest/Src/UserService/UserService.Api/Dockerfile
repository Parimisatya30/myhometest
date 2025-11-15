# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8000
EXPOSE 8000


# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files
COPY ["UserService/UserService.Api/UserService.Api.csproj", "UserService/UserService.Api/"]
COPY ["UserService/UserService.Application/UserService.Application.csproj", "UserService/UserService.Application/"]
COPY ["UserService/UserService.Domain/UserService.Domain.csproj", "UserService/UserService.Domain/"]
COPY ["UserService/UserService.Infrastructure/UserService.Infrastructure.csproj", "UserService/UserService.Infrastructure/"]
COPY ["Shared/Shared.csproj", "Shared/"]

# Restore dependencies
RUN dotnet restore "UserService/UserService.Api/UserService.Api.csproj"

# Copy all source files
COPY . .

WORKDIR "/src/UserService/UserService.Api"
RUN dotnet publish "UserService.Api.csproj" -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UserService.Api.dll"]
