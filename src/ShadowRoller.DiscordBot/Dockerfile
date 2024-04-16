FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src
COPY ["src/ShadowRoller.DiscordBot/ShadowRoller.DiscordBot.csproj", "./ShadowRoller.DiscordBot/"]
COPY ["src/ShadowRoller.Domain/ShadowRoller.Domain.csproj", "./ShadowRoller.Domain/"]
RUN dotnet restore "./ShadowRoller.DiscordBot/ShadowRoller.DiscordBot.csproj"

COPY ./src .

WORKDIR "/src/ShadowRoller.DiscordBot"
RUN dotnet build "./ShadowRoller.DiscordBot.csproj" -c "$BUILD_CONFIGURATION" -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ShadowRoller.DiscordBot.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
USER app
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShadowRoller.DiscordBot.dll"]