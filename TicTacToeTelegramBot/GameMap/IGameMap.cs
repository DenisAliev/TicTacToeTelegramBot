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
        public TelegramBotClient Bot { init; get; }
        public Task RenderAsync(Message message, GameMapEnum playerTurn);
        public Task StartAsync(ChatId id);
        public bool SetPosition(GameMapEnum player, int x, int y);
        public bool CheckWin();
    }
}
