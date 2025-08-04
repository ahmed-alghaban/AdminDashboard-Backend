# -------- Build stage --------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj first for layer caching
COPY AdminDashboard.csproj ./
RUN dotnet restore AdminDashboard.csproj

# Copy the rest and publish
COPY . ./
RUN dotnet publish AdminDashboard.csproj -c Release -o /app/publish

# -------- Runtime stage --------
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Kestrel must listen on 5000 behind EB's Nginx
ENV ASPNETCORE_URLS=http://0.0.0.0:5000
EXPOSE 5000

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AdminDashboard.dll"]