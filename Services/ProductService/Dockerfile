FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["ProductService/Application/Application/Application.csproj", "ProductService/Application/Application/"]
COPY ["ProductService/Domain/Domain/Domain.csproj", "ProductService/Domain/Domain/"]
COPY ["ProductService/Enum/Enum/Enum.csproj", "ProductService/Enum/Enum/"]
COPY ["ProductService/Host/Host/Host.csproj", "ProductService/Host/Host/"]
COPY ["Core/Core.csproj", "Core/"]

RUN dotnet restore "ProductService/Host/Host/Host.csproj"
RUN dotnet restore "Core/Core.csproj"
RUN dotnet restore "ProductService/Enum/Enum/Enum.csproj"
RUN dotnet restore "ProductService/Application/Application/Application.csproj"
RUN dotnet restore "ProductService/Domain/Domain/Domain.csproj"

COPY . .

RUN dotnet build "ProductService/Application/Application/Application.csproj" -c Release -o /app/build
RUN dotnet build "ProductService/Domain/Domain/Domain.csproj" -c Release -o /app/build
RUN dotnet build "ProductService/Enum/Enum/Enum.csproj" -c Release -o /app/build
RUN dotnet build "ProductService/Host/Host/Host.csproj" -c Release -o /app/build
RUN dotnet build "Core/Core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductService/Application/Application/Application.csproj" -c Release -o /app/publish
RUN dotnet publish "ProductService/Domain/Domain/Domain.csproj" -c Release -o /app/publish
RUN dotnet publish "ProductService/Enum/Enum/Enum.csproj" -c Release -o /app/publish
RUN dotnet publish "ProductService/Host/Host/Host.csproj" -c Release -o /app/publish
RUN dotnet publish "Core/Core.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "Host.dll"]
