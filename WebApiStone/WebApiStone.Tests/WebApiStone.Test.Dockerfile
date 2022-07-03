FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /test

COPY ./ ./
ENTRYPOINT ["dotnet", "test", "./WebApiStone.Test.csproj"]