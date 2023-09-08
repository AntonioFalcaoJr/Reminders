ARG ASPNET_VERSION="7.0"
ARG SDK_VERSION="7.0"
ARG BASE_ADRESS="mcr.microsoft.com/dotnet"

FROM $BASE_ADRESS/aspnet:$ASPNET_VERSION AS base
WORKDIR /app

FROM $BASE_ADRESS/sdk:$SDK_VERSION AS build

EXPOSE 80

COPY ./global.json ./
COPY ./nuget.config ./
COPY ./Directory.Packages.props ./

WORKDIR /src

COPY ./src/Core/Application/*.csproj ./Core/Application/
COPY ./src/Core/Domain/*.csproj ./Core/Domain/
COPY ./src/Infrastructure/Infrastructure.EventBus/*.csproj ./Infrastructure/Infrastructure.EventBus/
COPY ./src/Infrastructure/Infrastructure.EventStore/*.csproj ./Infrastructure/Infrastructure.EventStore/
COPY ./src/Web/WebAPI/*.csproj ./Web/WebAPI/

RUN dotnet restore -v m ./Web/WebAPI/

COPY ./src/Core/Application/. ./Core/Application/
COPY ./src/Core/Domain/. ./Core/Domain/
COPY ./src/Infrastructure/Infrastructure.EventBus/. ./Infrastructure/Infrastructure.EventBus/
COPY ./src/Infrastructure/Infrastructure.EventStore/. ./Infrastructure/Infrastructure.EventStore/
COPY ./src/Web/WebAPI/. ./Web/WebAPI/

WORKDIR /src/Web/WebAPI

RUN dotnet build -c Release --no-restore -v m -o /app/build 

FROM build AS publish
RUN dotnet publish -c Release --no-restore -v m -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]