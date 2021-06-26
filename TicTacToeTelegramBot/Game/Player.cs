using Telegram.Bot.Types;

namespace TicTacToeTelegramBot.Game
{
    public class Player
    {
        public long Id { set; get; }
        public ChatId ChatId { set; get; }
        public string Name { set; get; }
    }
}