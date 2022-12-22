FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /StoreCRM
EXPOSE 80

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /StoreCRM
COPY --from=build-env /StoreCRM/out .
ENTRYPOINT ["dotnet", "StoreCRM.dll"]