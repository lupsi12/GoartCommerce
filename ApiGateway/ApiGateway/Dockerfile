FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["ApiGateway/ApiGateway/ApiGateway.csproj", "ApiGateway/ApiGateway/"]
RUN dotnet restore "ApiGateway/ApiGateway/ApiGateway.csproj"

COPY . .

RUN dotnet build "ApiGateway/ApiGateway/ApiGateway.csproj" -c Release -o /app/build

RUN dotnet publish "ApiGateway/ApiGateway/ApiGateway.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "ApiGateway.dll"]
