using TicTacToeTelegramBot.GameMap;

namespace TicTacToeTelegramBot.Game
{
    public interface IGameFactory
    {
        public IGame GetGame(GameTypeEnum gameType, IGameMap gameMap, params Player[] players);
    }
}