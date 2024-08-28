FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["AuthService/Application/Application/Application.csproj", "AuthService/Application/Application/"]
COPY ["AuthService/Domain/Domain/Domain.csproj", "AuthService/Domain/Domain/"]
COPY ["AuthService/Enum/Enum/Enum.csproj", "AuthService/Enum/Enum/"]
COPY ["AuthService/Host/Host/Host.csproj", "AuthService/Host/Host/"]
COPY ["Core/Core.csproj", "Core/"]

RUN dotnet restore "AuthService/Host/Host/Host.csproj"
RUN dotnet restore "Core/Core.csproj"
RUN dotnet restore "AuthService/Enum/Enum/Enum.csproj"
RUN dotnet restore "AuthService/Application/Application/Application.csproj"
RUN dotnet restore "AuthService/Domain/Domain/Domain.csproj"

COPY . .

RUN dotnet build "AuthService/Application/Application/Application.csproj" -c Release -o /app/build
RUN dotnet build "AuthService/Domain/Domain/Domain.csproj" -c Release -o /app/build
RUN dotnet build "AuthService/Enum/Enum/Enum.csproj" -c Release -o /app/build
RUN dotnet build "AuthService/Host/Host/Host.csproj" -c Release -o /app/build
RUN dotnet build "Core/Core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthService/Application/Application/Application.csproj" -c Release -o /app/publish
RUN dotnet publish "AuthService/Domain/Domain/Domain.csproj" -c Release -o /app/publish
RUN dotnet publish "AuthService/Enum/Enum/Enum.csproj" -c Release -o /app/publish
RUN dotnet publish "AuthService/Host/Host/Host.csproj" -c Release -o /app/publish
RUN dotnet publish "Core/Core.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "Host.dll"]
