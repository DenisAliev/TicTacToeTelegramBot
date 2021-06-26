FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim-arm32v7 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TicTacToeTelegramBot/TicTacToeTelegramBot.csproj", "TicTacToeTelegramBot/"]
RUN dotnet restore -r linux-arm "TicTacToeTelegramBot/TicTacToeTelegramBot.csproj"
COPY . .
WORKDIR "/src/TicTacToeTelegramBot"

FROM build AS publish
RUN dotnet publish "TicTacToeTelegramBot.csproj" -c Release -o /app/publish -r linux-arm

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicTacToeTelegramBot.dll"]
