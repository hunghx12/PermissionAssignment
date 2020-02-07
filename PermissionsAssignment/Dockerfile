FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY PermissionsAssignment/PermissionsAssignment.csproj PermissionsAssignment/
RUN dotnet restore "PermissionsAssignment/PermissionsAssignment.csproj"
COPY . .
WORKDIR "/src/PermissionsAssignment"
RUN dotnet build "PermissionsAssignment.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PermissionsAssignment.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PermissionsAssignment.dll"]
