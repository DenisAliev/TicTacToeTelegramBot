using System.Runtime.InteropServices.ComTypes;
using Telegram.Bot;
using TicTacToeTelegramBot.GameMap;

namespace TicTacToeTelegramBot.Game
{
    public class GameFactory:IGameFactory
    {
        private readonly TelegramBotClient _bot;
        public GameFactory(TelegramBotClient bot)
        {
            _bot = bot;
        }
        public IGame GetGame(GameTypeEnum gameType, IGameMap gameMap, params Player[] players)
        {
            IGame result = null;
            switch (gameType)
            {
                case GameTypeEnum.PvpGame:
                    if (players.Length == 2)
                    {
                        result = new PvpGame(_bot, players[0], players[1], gameMap);
                    }
                    break;
            }

            return result;
        }
    }
}