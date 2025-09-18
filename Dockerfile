FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Coldairarrow.Api/05.Coldairarrow.Api.csproj", "src/Coldairarrow.Api/"]
COPY ["src/Coldairarrow.Business/04.Coldairarrow.Business.csproj", "src/Coldairarrow.Business/"]
COPY ["src/Coldairarrow.IBusiness/03.Coldairarrow.IBusiness.csproj", "src/Coldairarrow.IBusiness/"]
COPY ["src/Coldairarrow.Entity/02.Coldairarrow.Entity.csproj", "src/Coldairarrow.Entity/"]
COPY ["src/Coldairarrow.Util/01.Coldairarrow.Util.csproj", "src/Coldairarrow.Util/"]

RUN dotnet restore "src/Coldairarrow.Api/05.Coldairarrow.Api.csproj"
COPY . .
WORKDIR "/src/src/Coldairarrow.Api"
RUN dotnet build "05.Coldairarrow.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "05.Coldairarrow.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Coldairarrow.Api.dll"]