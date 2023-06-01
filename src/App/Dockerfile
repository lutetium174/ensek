#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/App/App.csproj", "src/App/"]
COPY ["src/Domain.Services/Domain.Services.csproj", "src/Domain.Services/"]
COPY ["src/Persistence/Persistence.csproj", "src/Persistence/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Domain.Web/Domain.Web.csproj", "src/Domain.Web/"]
COPY ["src/Persistence.Abstractions/Persistence.Abstractions.csproj", "src/Persistence.Abstractions/"]
RUN dotnet restore "src/App/App.csproj"
COPY . .
WORKDIR "/src/src/App"
RUN dotnet build "App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.dll"]