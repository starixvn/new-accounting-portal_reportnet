FROM hub.nexdev.net/base/dotnet/core/sdk:8.0-alpine AS builder
WORKDIR /src

COPY src/Main/Nap-Reports.HouseWinLoss/*.csproj ./src/Main/Nap-Reports.HouseWinLoss/
COPY src/Shared/Nap-Reports.Utilities/*.csproj ./src/Shared/Nap-Reports.Utilities/
COPY Nap-Reports.Sites/*.csproj ./Nap-Reports.Sites/


RUN dotnet nuget add source https://nuget.nexdev.net/api/v2
RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet restore Nap-Reports.Sites/Nap-Reports.Sites.csproj

COPY src ./src
COPY Nap-Reports.Sites ./Nap-Reports.Sites

WORKDIR /src/Nap-Reports.Sites

RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet publish -c Release -o /app

FROM hub.nexdev.net/base/dotnet/core/aspnet:8.0-alpine AS base

ENV TZ="America/La_Paz" 
ENV COMPlus_EnableDiagnostics=0


# Don’t run as root
RUN adduser --disabled-password \
  --home /app \
  --gecos '' dotnetuser && chown -R dotnetuser /app && chown -R dotnetuser /tmp
USER dotnetuser
#

WORKDIR /app
COPY --from=builder /app .

ENTRYPOINT ["dotnet", "Nap-Reports.Sites.dll"]
