FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NewsScrapingMonolithic.WebAPI/NewsScrapingMonolithic.WebAPI.csproj", "NewsScrapingMonolithic.WebAPI/"]
COPY ["NewsScrapingMonolithic.Application/NewsScrapingMonolithic.Application.csproj", "NewsScrapingMonolithic.Application/"]
COPY ["NewsScrapingMonolithic.Domain/NewsScrapingMonolithic.Domain.csproj", "NewsScrapingMonolithic.Domain/"]
COPY ["NewsScrapingMonolithic.Persistence/NewsScrapingMonolithic.Persistence.csproj", "NewsScrapingMonolithic.Persistence/"]
RUN dotnet restore "NewsScrapingMonolithic.WebAPI/NewsScrapingMonolithic.WebAPI.csproj"
COPY . .
WORKDIR "/src/NewsScrapingMonolithic.WebAPI"
RUN dotnet build "NewsScrapingMonolithic.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewsScrapingMonolithic.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsScrapingMonolithic.WebAPI.dll"]
