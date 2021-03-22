#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["LoadingArtistCrowdSource/Server/LoadingArtistCrowdSource.Server.csproj", "LoadingArtistCrowdSource/Server/"]
COPY ["LoadingArtistCrowdSource/Shared/LoadingArtistCrowdSource.Shared.csproj", "LoadingArtistCrowdSource/Shared/"]
COPY ["LoadingArtistCrowdSource/Client/LoadingArtistCrowdSource.Client.csproj", "LoadingArtistCrowdSource/Client/"]
RUN dotnet restore "LoadingArtistCrowdSource/Server/LoadingArtistCrowdSource.Server.csproj"
COPY . .
WORKDIR "/src/LoadingArtistCrowdSource/Server"
RUN dotnet build "LoadingArtistCrowdSource.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LoadingArtistCrowdSource.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoadingArtistCrowdSource.Server.dll"]