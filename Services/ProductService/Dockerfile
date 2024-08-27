# Start by setting the Docker context in the appropriate directory for each service.
# Example for ProductService:

    FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
    WORKDIR /src
    
    # Copy the solution file and restore dependencies
    COPY *.sln ./
    COPY Application/Application/Application.csproj ./Application/
    COPY Domain/Domain/Domain.csproj ./Domain/
    COPY Enum/Enum/Enum.csproj ./Enum/
    COPY Host/Host/Host.csproj ./Host/
    RUN dotnet restore ./Host/Host.csproj
    
    # Copy the entire service directory
    COPY . .
    
    # Build the service
    WORKDIR /src/Host
    RUN dotnet publish -c Release -o /app/publish
    
    # Build the runtime image
    FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
    WORKDIR /app
    COPY --from=build /app/publish .
    ENTRYPOINT ["dotnet", "Host.dll"]
    