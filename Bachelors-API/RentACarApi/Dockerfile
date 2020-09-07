FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Solution/RentACarApi/RentACarApi.csproj", "Solution/RentACarApi/"]
COPY ["Solution/RentACarApi.Data/RentACarApi.Data.csproj", "Solution/RentACarApi.Data/"]
COPY ["Solution/RentACarApi.Domain/RentACarApi.Domain.csproj", "Solution/RentACarApi.Domain/"]
RUN dotnet restore "Solution/RentACarApi/RentACarApi.csproj"
COPY . .
WORKDIR "/src/Solution/RentACarApi"
RUN dotnet build "RentACarApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RentACarApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentACarApi.dll"]