FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/StudyTracker.API/StudyTracker.API.csproj", "StudyTracker.API/"]
COPY ["src/StudyTracker.Application/StudyTracker.Application.csproj", "StudyTracker.Application/"]
COPY ["src/StudyTracker.Infrastructure/StudyTracker.Infrastructure.csproj", "StudyTracker.Infrastructure/"]
COPY ["src/StudyTracker.Domain/StudyTracker.Domain.csproj", "StudyTracker.Domain/"]
RUN dotnet restore "StudyTracker.API/StudyTracker.API.csproj"

COPY src/ .
WORKDIR "/src/StudyTracker.API"
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "StudyTracker.API.dll"]