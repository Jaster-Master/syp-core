FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7139

ENV ASPNETCORE_URLS http://+:7139

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Core.Backend.Secure/Core.Backend.Secure.csproj", "Core.Backend.Secure/"]
COPY ["Core.AuthLib/Core.AuthLib.csproj", "Core.AuthLib/"]
COPY ["Core.Secure.Database/Core.Secure.Database.csproj", "Core.Secure.Database/"]
COPY ["Core.Ldap.Interface/Core.Ldap.Interface.csproj", "Core.Ldap.Interface/"]
COPY ["Core.Ldap.Implementation/Core.Ldap.Implementation.csproj", "Core.Ldap.Implementation/"]
COPY ["Core.WebUntis.Implementation/Core.WebUntis.Implementation.csproj", "Core.WebUntis.Implementation/"]
COPY ["Core.WebUntis.Interface/Core.WebUntis.Interface.csproj", "Core.WebUntis.Interface/"]
RUN dotnet restore "Core.Backend.Secure/Core.Backend.Secure.csproj"
COPY . .
WORKDIR "/src/Core.Backend.Secure"
RUN dotnet build "Core.Backend.Secure.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Core.Backend.Secure.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Core.Backend.Secure.dll"]
