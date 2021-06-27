namespace TicTacToeTelegramBot.GameMap
{
    public interface IGameMapFactory
    {
        public IGameMap GetGameMap(GameMapTypeEnum gameMapType, uint size);
    }
}