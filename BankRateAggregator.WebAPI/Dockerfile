FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BankRateAggregator.WebAPI/BankRateAggregator.WebAPI.csproj", "BankRateAggregator.WebAPI/"]
COPY ["BankRateAggregator.Infrastructure/BankRateAggregator.Infrastructure.csproj", "BankRateAggregator.Infrastructure/"]
COPY ["BankRateAggregator.Application/BankRateAggregator.Application.csproj", "BankRateAggregator.Application/"]
COPY ["BankRateAggregator.Domain/BankRateAggregator.Domain.csproj", "BankRateAggregator.Domain/"]
RUN dotnet restore "BankRateAggregator.WebAPI/BankRateAggregator.WebAPI.csproj"
COPY . .
WORKDIR "/src/BankRateAggregator.WebAPI"
RUN dotnet build "BankRateAggregator.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BankRateAggregator.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankRateAggregator.WebAPI.dll"]