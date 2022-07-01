FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80;http://+:5000;
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApiStone/WebApiStone.csproj", "./WebApiStone/"]
RUN dotnet restore "WebApiStone/WebApiStone.csproj"
COPY . .
WORKDIR "/src/WebApiStone/"
RUN dotnet build "WebApiStone.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApiStone.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiStone.dll"]