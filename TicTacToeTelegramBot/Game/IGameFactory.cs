using Telegram.Bot.Types;
using TicTacToeTelegramBot.GameMap;

namespace TicTacToeTelegramBot.Game
{
    public interface IGameFactory
    {
        public IGame GetGame(GameTypeEnum gameType, IGameMap gameMap, ChatId chatId,params Player[] players);
    }
}