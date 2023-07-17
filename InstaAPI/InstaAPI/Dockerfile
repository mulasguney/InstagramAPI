FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["InstaAPI/InstaAPI.csproj", "InstaAPI/"]
RUN dotnet restore "InstaAPI/InstaAPI.csproj"
COPY . .
WORKDIR "/src/InstaAPI"
RUN dotnet build "InstaAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InstaAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InstaAPI.dll"]
