#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["KratosBlazorApp/Server/KratosBlazorApp.Server.csproj", "KratosBlazorApp/Server/"]
COPY ["KratosBlazorApp/Client/KratosBlazorApp.Client.csproj", "KratosBlazorApp/Client/"]
COPY ["KratosBlazorApp/Shared/KratosBlazorApp.Shared.csproj", "KratosBlazorApp/Shared/"]
RUN dotnet restore "KratosBlazorApp/Server/KratosBlazorApp.Server.csproj"
COPY . .
WORKDIR "/src/KratosBlazorApp/Server"
RUN dotnet build "KratosBlazorApp.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KratosBlazorApp.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

RUN /usr/sbin/update-ca-certificates

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KratosBlazorApp.Server.dll"]