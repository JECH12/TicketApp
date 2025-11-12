# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Ticket.Application/Ticket.Application.csproj", "Ticket.Application/"]
COPY ["Ticket.Data/Ticket.Data.csproj", "Ticket.Data/"]
COPY ["Ticket.Services/Ticket.Services.csproj", "Ticket.Services/"]
COPY ["Ticket.Contracts/Ticket.Contracts.csproj", "Ticket.Contracts/"]

RUN dotnet restore "Ticket.Application/Ticket.Application.csproj"

COPY . .
WORKDIR "/src/Ticket.Application"
RUN dotnet publish "Ticket.Application.csproj" -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "Ticket.Application.dll"]