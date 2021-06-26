FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TicTacToeTelegramBot/TicTacToeTelegramBot.csproj", "TicTacToeTelegramBot/"]
RUN dotnet restore "TicTacToeTelegramBot/TicTacToeTelegramBot.csproj"
COPY . .
WORKDIR "/src/TicTacToeTelegramBot"
RUN dotnet build "TicTacToeTelegramBot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TicTacToeTelegramBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicTacToeTelegramBot.dll"]
