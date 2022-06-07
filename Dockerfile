#https://joehonour.medium.com/a-guide-to-setting-up-a-net-core-project-using-docker-with-integrated-unit-and-component-tests-a326ca5a0284
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

# copy sln and csproj files into the image
COPY *.sln .
COPY src/ShadowRoller.Domain/*.csproj ./src/ShadowRoller.Domain/
COPY src/ShadowRoller.DiscordBot/*.csproj ./src/ShadowRoller.DiscordBot/
COPY tests/ShadowRoller.Domain.Tests/*.csproj ./tests/ShadowRoller.Domain.Tests/
COPY tests/ShadowRoller.DiscordBot.Tests/*.csproj ./tests/ShadowRoller.DiscordBot.Tests/

# restore package dependencies for the solution
RUN dotnet restore

# copy full solution over
COPY . .

# build the solution
RUN dotnet build

# Create new build target to run only the tests
FROM build AS test

# Navigate to unit test directory
WORKDIR /app/tests/ShadowRoller.Domain.Tests

# Execute command to run the unit tests
CMD ["dotnet", "test", "--logger:trx"]

# Run unit tests
#FROM build AS test

# Set directory to unit test project
#WORKDIR /app/tests/ShadowRoller.Domain.Tests

#RUN dotnet test --logger:trx

# create a new layer from the build later
FROM build AS publish

# set the working directory to be the web api project
WORKDIR /app/src/ShadowRoller.DiscordBot

# publish the web api project to a directory called out
RUN dotnet publish -c Release -o out

# create a new layer using the cut-down aspnet runtime image
FROM mcr.microsoft.com/dotnet/runtime:6.0 AS runtime

WORKDIR /app

# copy over the files produced when publishing the service
COPY --from=publish /app/src/ShadowRoller.DiscordBot/out ./

# expose port 80 as our application will be listening on this port
#EXPOSE 80

# run the web api when the docker image is started
ENTRYPOINT ["dotnet", "ShadowRoller.DiscordBot.dll"]