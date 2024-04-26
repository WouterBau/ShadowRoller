# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS copyrestore
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

FROM copyrestore as mutation
# Install dotnet-stryker
RUN dotnet tool install --global dotnet-stryker
# Add dotnet tools to PATH
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT dotnet stryker -s ShadowRoller.sln --output MutationResults --dashboard-api-key $STRYKER_API_KEY -v $BRANCH_NAME

FROM copyrestore as qabuild
ARG SONAR_HOST_URL
ARG SONAR_TOKEN
ARG SONAR_ORGANIZATION
ARG SONAR_PROJECT_KEY
ARG SONAR_PROJECT_NAME
ARG SONAR_BRANCH

# Install OpenJDK 17
RUN apt-get update && apt-get install -y openjdk-17-jdk
# Install sonarqube scanner
RUN dotnet tool install --global dotnet-sonarscanner
# Add dotnet tools to PATH
ENV PATH="$PATH:/root/.dotnet/tools"
# Begin SonarCloud analysis
RUN dotnet sonarscanner begin \
 /o:"$SONAR_ORGANIZATION" \
 /k:"$SONAR_PROJECT_KEY" \
 /n:"$SONAR_PROJECT_NAME" \
 /d:sonar.token="$SONAR_TOKEN" \
 /d:sonar.host.url="$SONAR_HOST_URL" \
 /d:sonar.branch.name="$SONAR_BRANCH" \
 /d:sonar.cs.opencover.reportsPaths="**/coverage.opencover.xml"
# Rebuild the application
RUN dotnet build --no-restore ShadowRoller.sln
# Run the tests
RUN dotnet test ShadowRoller.sln --no-build --logger trx --results-directory ./TestResults/ /p:CollectCoverage=true /p:CoverletOutputFormat=json%2ccobertura%2copencover /p:CoverletOutput=../../TestResults/ /p:MergeWith=../../TestResults/coverage.json /p:SkipAutoProps=true /m:1
# End SonarCloud analysis
RUN dotnet sonarscanner end /d:sonar.token="$SONAR_TOKEN"

FROM qabuild as qatest
ENTRYPOINT [ "cp", "-r", "./TestResults", "./volume/TestResults" ]

FROM copyrestore AS build
# Build the application
RUN dotnet build --no-restore ShadowRoller.sln