# Use the ASP.NET runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the .NET SDK to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files
COPY ["MyShop/MyShop.csproj", "MyShop/"]
COPY ["DTO/DTO.csproj", "DTO/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Reposetories/Reposetories.csproj", "Reposetories/"]
COPY ["Entities/Entities.csproj", "Entities/"]

# Restore dependencies
RUN dotnet restore "./MyShop/MyShop.csproj"

# Copy the rest of the application files
COPY . ./
WORKDIR "/src/MyShop"

# Build the application
RUN dotnet build "./MyShop.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MyShop.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Use the runtime image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyShop.dll"]