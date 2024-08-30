FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["HomeService/Application/Application/Application.csproj", "HomeService/Application/Application/"]
COPY ["HomeService/Domain/Domain/Domain.csproj", "HomeService/Domain/Domain/"]
COPY ["HomeService/Host/Host/Host.csproj", "HomeService/Host/Host/"]
COPY ["Core/Core.csproj", "Core/"]

RUN dotnet restore "HomeService/Host/Host/Host.csproj"
RUN dotnet restore "Core/Core.csproj"
RUN dotnet restore "HomeService/Application/Application/Application.csproj"
RUN dotnet restore "HomeService/Domain/Domain/Domain.csproj"

COPY . .

RUN dotnet build "HomeService/Application/Application/Application.csproj" -c Release -o /app/build
RUN dotnet build "HomeService/Domain/Domain/Domain.csproj" -c Release -o /app/build
RUN dotnet build "HomeService/Host/Host/Host.csproj" -c Release -o /app/build
RUN dotnet build "Core/Core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HomeService/Application/Application/Application.csproj" -c Release -o /app/publish
RUN dotnet publish "HomeService/Domain/Domain/Domain.csproj" -c Release -o /app/publish
RUN dotnet publish "HomeService/Host/Host/Host.csproj" -c Release -o /app/publish
RUN dotnet publish "Core/Core.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "Host.dll"]
