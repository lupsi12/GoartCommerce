FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["CartService/Application/Application/Application.csproj", "CartService/Application/Application/"]
COPY ["CartService/Domain/Domain/Domain.csproj", "CartService/Domain/Domain/"]
COPY ["CartService/Enum/Enum/Enum.csproj", "CartService/Enum/Enum/"]
COPY ["CartService/Host/Host/Host.csproj", "CartService/Host/Host/"]
COPY ["Core/Core.csproj", "Core/"]

RUN dotnet restore "CartService/Host/Host/Host.csproj"
RUN dotnet restore "Core/Core.csproj"
RUN dotnet restore "CartService/Enum/Enum/Enum.csproj"
RUN dotnet restore "CartService/Application/Application/Application.csproj"
RUN dotnet restore "CartService/Domain/Domain/Domain.csproj"

COPY . .

RUN dotnet build "CartService/Application/Application/Application.csproj" -c Release -o /app/build
RUN dotnet build "CartService/Domain/Domain/Domain.csproj" -c Release -o /app/build
RUN dotnet build "CartService/Enum/Enum/Enum.csproj" -c Release -o /app/build
RUN dotnet build "CartService/Host/Host/Host.csproj" -c Release -o /app/build
RUN dotnet build "Core/Core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CartService/Application/Application/Application.csproj" -c Release -o /app/publish
RUN dotnet publish "CartService/Domain/Domain/Domain.csproj" -c Release -o /app/publish
RUN dotnet publish "CartService/Enum/Enum/Enum.csproj" -c Release -o /app/publish
RUN dotnet publish "CartService/Host/Host/Host.csproj" -c Release -o /app/publish
RUN dotnet publish "Core/Core.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "Host.dll"]
