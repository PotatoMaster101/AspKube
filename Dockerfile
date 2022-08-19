FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR "/src"
COPY . .
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS final
WORKDIR /app
EXPOSE 80 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspKube.dll"]
