#https://joehonour.medium.com/a-guide-to-setting-up-a-net-core-project-using-docker-with-integrated-unit-and-component-tests-a326ca5a0284
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

# copy everything
COPY . .

# build the solution
RUN dotnet build ShadowRoller.sln

# Create new build target to run only the tests
FROM build AS test

# Navigate to unit test directory
WORKDIR /app

# Execute command to run the unit tests
CMD ["dotnet", "test", "ShadowRoller.sln", "--logger:trx", "--collect", "XPlat Code Coverage"]