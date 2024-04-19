FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
EXPOSE  85
EXPOSE 436

COPY ShopKnjigaGlavni/*.csproj ./ShopKnjigaGlavni/
COPY ShopKnjigaGlavni.Models/*.csproj ./ShopKnjigaGlavni.Models/
COPY ShopKnjigaGlavni.DataAccess/*.csproj ./ShopKnjigaGlavni.DataAccess/
COPY ShopKnjigaGlavni.Utility/*.csproj ./ShopKnjigaGlavni.Utility/

RUN for file in $(ls *.csproj); do dotnet restore $file; done

COPY . ./
RUN dotnet publish -c Release  -o out

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final-env
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "ShopKnjigaGlavni.dll" ]
