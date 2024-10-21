FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EdenGarden_API/EdenGarden_API.csproj", "EdenGarden_API/"]
RUN dotnet restore "EdenGarden_API/EdenGarden_API.csproj"
COPY . .
WORKDIR "/src/EdenGarden_API"
RUN dotnet build "EdenGarden_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EdenGarden_API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "EdenGarden_API.dll"]