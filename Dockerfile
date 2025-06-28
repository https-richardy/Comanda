# Use ASP.NET Core 9.0 runtime image as base
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Use SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files to restore dependencies
COPY ["Source/Comanda.WebApi/Comanda.WebApi.csproj", "Comanda.WebApi/"]

# Copy the entire solution or related projects
COPY ["Comanda.sln", "./"]

# restore dependencies for the project (will use the solution to resolve dependencies)
RUN dotnet restore "Comanda.WebApi/Comanda.WebApi.csproj"

# copy all source code into the container
COPY Source/ ./Source/

# set working directory to the web project
WORKDIR "/src/Source/Comanda.WebApi"

# Build in Release mode
RUN dotnet build "Comanda.WebApi.csproj" -c Release -o /app/build

# Publish the project for production
FROM build AS publish
RUN dotnet publish "Comanda.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image to run the app
FROM base AS final
WORKDIR /app

# Copy published files from the publish stage
COPY --from=publish /app/publish .

# Set the command to start the application
ENTRYPOINT ["dotnet", "Comanda.WebApi.dll"]
