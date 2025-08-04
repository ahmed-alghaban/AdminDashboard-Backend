# -------- Build stage --------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY AdminDashboard.csproj ./
RUN dotnet restore AdminDashboard.csproj
COPY . ./
RUN dotnet publish AdminDashboard.csproj -c Release -o /app/publish

# -------- Runtime stage --------
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
# Listen on 5000 inside the container (EB routes to this)
ENV ASPNETCORE_URLS=http://0.0.0.0:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "AdminDashboard.dll"]