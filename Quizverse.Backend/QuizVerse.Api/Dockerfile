FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base 

WORKDIR /app
EXPOSE 8081
ENV ASPNETCORE_URLS=http://+:8081

# Instalação do Certbot (opcional, se necessário)
RUN apt-get update && apt-get install -y certbot

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build 
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["QuizVerse.Api/QuizVerse.Api.csproj", "QuizVerse.Api/"]
COPY ["QuizverseBack.Domain/QuizverseBack.Domain.csproj", "QuizverseBack.Domain/"]
COPY ["QuizVerse.Platform/QuizVerse.Platform.csproj", "QuizVerse.Platform/"]
RUN dotnet restore "./QuizVerse.Api/QuizVerse.Api.csproj"
COPY . .
WORKDIR "/src/QuizVerse.Api"
RUN dotnet build "./QuizVerse.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./QuizVerse.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuizVerse.Api.dll"]
