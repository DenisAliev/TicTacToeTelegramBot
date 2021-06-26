using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TicTacToeTelegramBot.GameMap
{
    public interface IGameMap
    {
        public TelegramBotClient Bot { set; get; }
        public Task RenderAsync(ChatId id);
        public bool SetPosition(GameMapEnum player, int x, int y);
    }
}
