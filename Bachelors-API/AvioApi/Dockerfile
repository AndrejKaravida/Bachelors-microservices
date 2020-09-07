FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Solution/AvioApi/AvioApi.csproj", "Solution/AvioApi/"]
COPY ["Solution/AvioApi.Data/AvioApi.Data.csproj", "Solution/AvioApi.Data/"]
COPY ["Solution/AvioApi.Domain/AvioApi.Domain.csproj", "Solution/AvioApi.Domain/"]
RUN dotnet restore "Solution/AvioApi/AvioApi.csproj"
COPY . .
WORKDIR "/src/Solution/AvioApi"
RUN dotnet build "AvioApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AvioApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AvioApi.dll"]