#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Prevent 'Warning: apt-key output should not be parsed (stdout is not a terminal)'
ENV APT_KEY_DONT_WARN_ON_DANGEROUS_USAGE=1

# install NodeJS 13.x
# see https://github.com/nodesource/distributions/blob/master/README.md#deb
RUN apt-get update -yq 
RUN apt-get install curl gnupg -yq 
RUN curl -sL https://deb.nodesource.com/setup_13.x | bash -
RUN apt-get install -y nodejs

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SamplePOC.csproj", ""]
COPY ["../BusinessLogicLayer/BusinessLogicLayer.csproj", "../BusinessLogicLayer/"]
COPY ["../ViewModels/ViewModels.csproj", "../ViewModels/"]
COPY ["../DataAccessLayer/DataAccessLayer.csproj", "../DataAccessLayer/"]
RUN dotnet restore "./SamplePOC.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SamplePOC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SamplePOC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SamplePOC.dll"]

