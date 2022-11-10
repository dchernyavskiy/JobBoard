#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["JobBoard.Identity/JobBoard.Identity.csproj", "JobBoard.Identity/"]
COPY ["JobBoard.Backend/Core/JobBoard.Application/JobBoard.Application.csproj", "JobBoard.Backend/Core/JobBoard.Application/"]
COPY ["JobBoard.Backend/Core/JobBoard.Domain/JobBoard.Domain.csproj", "JobBoard.Backend/Core/JobBoard.Domain/"]
RUN dotnet restore "JobBoard.Identity/JobBoard.Identity.csproj"
COPY . .
WORKDIR "/src/JobBoard.Identity"
RUN dotnet build "JobBoard.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JobBoard.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobBoard.Identity.dll"]