FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5208

ENV ASPNETCORE_URLS=http://+:5208

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["robotstakeover.csproj", "./"]
RUN dotnet restore "robotstakeover.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "robotstakeover.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "robotstakeover.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "robotstakeover.dll"]
