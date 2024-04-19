# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy each .csproj file individually
COPY src/ShadowRoller.Domain/ShadowRoller.Domain.csproj src/ShadowRoller.Domain/
COPY src/ShadowRoller.DiscordBot/ShadowRoller.DiscordBot.csproj src/ShadowRoller.DiscordBot/
COPY tests/ShadowRoller.Domain.Tests/ShadowRoller.Domain.Tests.csproj tests/ShadowRoller.Domain.Tests/

# Copy the solution file
COPY ShadowRoller.sln ./

# Restore the projects in the solution
RUN dotnet restore ShadowRoller.sln

# Copy the rest of the application code
COPY . .

# Build the application
RUN dotnet build --no-restore ShadowRoller.sln

# Run the tests
FROM build AS test
ENTRYPOINT ["dotnet", "test", "ShadowRoller.sln",  "--no-build", "/p:CollectCoverage=true", "/p:CoverletOutputFormat=json%2ccobertura%2copencover", "/p:CoverletOutput=../TestResults/", "/p:MergeWith=../TestResults/coverage.json", "/p:SkipAutoProps=true", "/m:1"]