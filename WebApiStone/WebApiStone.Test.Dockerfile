FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /test
COPY . .
RUN dotnet restore ./WebApiStone.Tests/WebApiStone.Tests.csproj
RUN dotnet restore ./WebApiStone/WebApiStone.csproj

FROM build AS test
WORKDIR /test
COPY . .
ENTRYPOINT ["dotnet", "test", "./WebApiStone.Tests/WebApiStone.Tests.csproj","--logger:trx"]

