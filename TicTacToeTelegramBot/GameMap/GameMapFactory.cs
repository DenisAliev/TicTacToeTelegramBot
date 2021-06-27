using Telegram.Bot;

namespace TicTacToeTelegramBot.GameMap
{
    public class GameMapFactory:IGameMapFactory
    {
        private TelegramBotClient _bot;
        public GameMapFactory(TelegramBotClient bot)
        {
            _bot = bot;
        }
        public IGameMap GetGameMap(GameMapTypeEnum gameMapType, uint size)
        {
            IGameMap result = null;
            switch (gameMapType)
            {
                case GameMapTypeEnum.Default:
                    result = new GameMap(_bot, size);
                    break;
            }
            return result;
        }
    }
}