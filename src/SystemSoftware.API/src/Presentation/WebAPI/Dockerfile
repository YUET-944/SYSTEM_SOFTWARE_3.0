FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Presentation/WebAPI/SystemSoftware.WebAPI.csproj", "src/Presentation/WebAPI/"]
COPY ["src/Application/SystemSoftware.Application/SystemSoftware.Application.csproj", "src/Application/SystemSoftware.Application/"]
COPY ["src/Domain/SystemSoftware.Domain/SystemSoftware.Domain.csproj", "src/Domain/SystemSoftware.Domain/"]
COPY ["src/Infrastructure/SystemSoftware.Infrastructure/SystemSoftware.Infrastructure.csproj", "src/Infrastructure/SystemSoftware.Infrastructure/"]
RUN dotnet restore "src/Presentation/WebAPI/SystemSoftware.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/Presentation/WebAPI"
RUN dotnet build "SystemSoftware.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SystemSoftware.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SystemSoftware.WebAPI.dll"]
