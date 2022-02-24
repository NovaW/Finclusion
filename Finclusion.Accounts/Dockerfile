FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /App
EXPOSE 7050

# build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY Finclusion.Database/*.csproj ./Finclusion.Database/
COPY Finclusion.Accounts/*.csproj ./Finclusion.Accounts/
RUN dotnet restore ./Finclusion.Accounts/

# copy everything else and build app
COPY ./Finclusion.Database ./Finclusion.Database/
COPY ./Finclusion.Accounts ./Finclusion.Accounts/
WORKDIR /source/Finclusion.Accounts
RUN dotnet build -c Release -o /App

# publish
FROM build AS publish
RUN dotnet publish -c Release -o /App

# final image
FROM base AS final
WORKDIR /App
COPY --from=publish /App .
ENTRYPOINT ["dotnet", "Finclusion.Accounts.dll"]