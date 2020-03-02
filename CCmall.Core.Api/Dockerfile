#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5123

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["/CCmall.Core.Api/CCmall.Core.Api.csproj", "CCmall.Core.Api/"]
COPY ["/CCmall.Repository/CCmall.Repository.csproj", "CCmall.Repository/"]
COPY ["/CCmall.Model/CCmall.Model.csproj", "CCmall.Model/"]
COPY ["/CCmall.Common/CCmall.Common.csproj", "CCmall.Common/"]
RUN dotnet restore "CCmall.Core.Api/CCmall.Core.Api.csproj"
COPY . .
WORKDIR "/src/CCmall.Core.Api"
RUN dotnet build "CCmall.Core.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CCmall.Core.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CCmall.Core.Api.dll"]