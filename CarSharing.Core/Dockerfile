FROM mcr.microsoft.com/dotnet/sdk:6.0 as base
WORKDIR /app
COPY . ./
RUN dotnet restore

RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=base /publish .

ENTRYPOINT ["dotnet", "CarSharing.dll"]
