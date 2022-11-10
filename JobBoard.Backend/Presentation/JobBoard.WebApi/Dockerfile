#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["JobBoard.Backend/Presentation/JobBoard.WebApi/JobBoard.WebApi.csproj", "JobBoard.Backend/Presentation/JobBoard.WebApi/"]
COPY ["JobBoard.Backend/Core/JobBoard.Application/JobBoard.Application.csproj", "JobBoard.Backend/Core/JobBoard.Application/"]
COPY ["JobBoard.Backend/Core/JobBoard.Domain/JobBoard.Domain.csproj", "JobBoard.Backend/Core/JobBoard.Domain/"]
COPY ["JobBoard.Backend/Infrastructure/JobBoard.Persistence/JobBoard.Persistence.csproj", "JobBoard.Backend/Infrastructure/JobBoard.Persistence/"]
RUN dotnet restore "JobBoard.Backend/Presentation/JobBoard.WebApi/JobBoard.WebApi.csproj"
COPY . .
WORKDIR "/src/JobBoard.Backend/Presentation/JobBoard.WebApi"
RUN dotnet build "JobBoard.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JobBoard.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobBoard.WebApi.dll"]