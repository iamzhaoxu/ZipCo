#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS publish
WORKDIR /src
COPY *.sh ./
RUN apt-get update && apt-get install dos2unix
COPY ["ZipCo.Users.DbUpdater/ZipCo.Users.DbUpdater.csproj", "ZipCo.Users.DbUpdater/"]
RUN dotnet restore "ZipCo.Users.DbUpdater/ZipCo.Users.DbUpdater.csproj"
COPY . .
WORKDIR "/src/ZipCo.Users.DbUpdater"
RUN dotnet publish "ZipCo.Users.DbUpdater.csproj" -c Release -o /app/publish 
WORKDIR /src
RUN find . -type f -name "*.sh" -exec dos2unix {} \+ && cp *.sh /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZipCo.Users.DbUpdater.dll"]