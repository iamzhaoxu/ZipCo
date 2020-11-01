#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS publish
WORKDIR /src
COPY *.sh ./
RUN apt-get update && apt-get install dos2unix
COPY ZipCo.Users.WebApi/ZipCo.Users.WebApi.csproj ZipCo.Users.WebApi/
RUN dotnet restore "ZipCo.Users.WebApi/ZipCo.Users.WebApi.csproj"
COPY . .
WORKDIR "/src/ZipCo.Users.WebApi"
RUN dotnet publish "ZipCo.Users.WebApi.csproj" -c Release -o /app/publish
WORKDIR /src
RUN find . -type f -name "*.sh" -exec dos2unix {} \+ && cp *.sh /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZipCo.Users.WebApi.dll"]
