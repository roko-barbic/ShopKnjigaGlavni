FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8088

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ShopKnjigaGlavni/*.csproj", "./ShopKnjigaGlavni/"]
COPY ["ShopKnjigaGlavni.Models/*.csproj", "./ShopKnjigaGlavni.Models/"]
COPY ["ShopKnjigaGlavni.DataAccess/*.csproj", "./ShopKnjigaGlavni.DataAccess/"]
COPY ["ShopKnjigaGlavni.Utility/*.csproj", "./ShopKnjigaGlavni.Utility/"]

RUN dotnet restore "ShopKnjigaGlavni/ShopKnjigaGlavni.csproj" 

COPY . ./
WORKDIR "/src/ShopKnjigaGlavni"
RUN dotnet build "ShopKnjigaGlavni.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ShopKnjigaGlavni.csproj" -c $BUILD_CONFIGURATION -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "ShopKnjigaGlavni.dll" ]
