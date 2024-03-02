#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MarsRoverPhotoApp.Fetcher/MarsRoverPhotoApp.Fetcher.csproj", "MarsRoverPhotoApp.Fetcher/"]
RUN dotnet restore "MarsRoverPhotoApp.Fetcher/MarsRoverPhotoApp.Fetcher.csproj"
COPY . .
WORKDIR "/src/MarsRoverPhotoApp.Fetcher"
RUN dotnet build "MarsRoverPhotoApp.Fetcher.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MarsRoverPhotoApp.Fetcher.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarsRoverPhotoApp.Fetcher.dll"]