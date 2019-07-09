FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["../ThinkerThings.Service.Manager.User.Account.Api/ThinkerThings.Service.Manager.User.Account.Api.csproj", "../ThinkerThings.Service.Manager.User.Account.Api/"]
RUN dotnet restore "../ThinkerThings.Service.Manager.User.Account.Api/ThinkerThings.Service.Manager.User.Account.Api.csproj"
COPY . .
WORKDIR "/src/../ThinkerThings.Service.Manager.User.Account.Api"
RUN dotnet build "ThinkerThings.Service.Manager.User.Account.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ThinkerThings.Service.Manager.User.Account.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ThinkerThings.Service.Manager.User.Account.Api.dll"]