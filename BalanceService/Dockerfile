FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BalanceService/BalanceService.csproj", "BalanceService/"]
RUN dotnet restore "BalanceService/BalanceService.csproj"
COPY . .
WORKDIR "/src/BalanceService"
RUN dotnet build "BalanceService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BalanceService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BalanceService.dll"]
