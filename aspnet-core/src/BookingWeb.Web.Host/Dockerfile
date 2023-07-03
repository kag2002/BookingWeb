FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/BookingWeb.Web.Host/BookingWeb.Web.Host.csproj", "src/BookingWeb.Web.Host/"]
COPY ["src/BookingWeb.Web.Core/BookingWeb.Web.Core.csproj", "src/BookingWeb.Web.Core/"]
COPY ["src/BookingWeb.Application/BookingWeb.Application.csproj", "src/BookingWeb.Application/"]
COPY ["src/BookingWeb.Core/BookingWeb.Core.csproj", "src/BookingWeb.Core/"]
COPY ["src/BookingWeb.EntityFrameworkCore/BookingWeb.EntityFrameworkCore.csproj", "src/BookingWeb.EntityFrameworkCore/"]
WORKDIR "/src/src/BookingWeb.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/BookingWeb.Web.Host", "src/BookingWeb.Web.Host"]
COPY ["src/BookingWeb.Web.Core", "src/BookingWeb.Web.Core"]
COPY ["src/BookingWeb.Application", "src/BookingWeb.Application"]
COPY ["src/BookingWeb.Core", "src/BookingWeb.Core"]
COPY ["src/BookingWeb.EntityFrameworkCore", "src/BookingWeb.EntityFrameworkCore"]
WORKDIR "/src/src/BookingWeb.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "BookingWeb.Web.Host.dll"]
