# RUN BOTH CONTAINERS FROM ROOT (folder with .sln file):
# docker-compose build
# docker-compose up
#
# RUN JUST THIS CONTAINER FROM ROOT (folder with .sln file):
# docker build --pull -t webrazor -f src/WebRazorPages/Dockerfile .
#
# RUN COMMAND
# docker run --name eshopweb --rm -it -p 5107:5107 webrazor
FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5107

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY . .
WORKDIR /src/src/WebRazorPages
RUN dotnet restore -nowarn:msb3202,nu1503
RUN dotnet build --no-restore -c Release -o /app

FROM build as publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS runtime
WORKDIR /app
COPY --from=publish /app ./

# Optional: Set this here if not setting it from docker-compose.yml
# ENV ASPNETCORE_ENVIRONMENT Development

ENTRYPOINT ["dotnet", "Microsoft.eShopWeb.RazorPages.dll"]
