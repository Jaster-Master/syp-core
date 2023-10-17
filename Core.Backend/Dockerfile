FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5001

ENV ASPNETCORE_URLS http://+:5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Core.Backend/Core.Backend.csproj", "Core.Backend/"]
COPY ["Core.AuthLib/Core.AuthLib.csproj", "Core.AuthLib/"]
COPY ["Core.Plugin.Interface/Core.Plugin.Interface.csproj", "Core.Plugin.Interface/"]
COPY ["Core.Database/Core.Database.csproj", "Core.Database/"]
COPY ["Core.Secure.Database/Core.Secure.Database.csproj", "Core.Secure.Database/"]
COPY ["Core.Ldap.Interface/Core.Ldap.Interface.csproj", "Core.Ldap.Interface/"]
RUN dotnet restore "Core.Backend/Core.Backend.csproj"
COPY . .
WORKDIR "/src/Core.Backend"
RUN dotnet build "Core.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Core.Backend.csproj" -c Release -o /app/publish /p:UseAppHost=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Core.Backend.dll"]
