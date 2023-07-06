# pull
FROM mcr.microsoft.com/dotnet/sdk:6.0 as base
WORKDIR /app
# copy adn restore
COPY . ./
RUN dotnet restore

# compile publish
RUN dotnet publish -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=base /app/out .
#ENTRYPOINT dotnet watch run --urls=http://+:5000

ENTRYPOINT ["dotnet", "CarSharing.dll"]
