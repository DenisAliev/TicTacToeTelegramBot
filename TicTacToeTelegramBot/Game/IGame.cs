using System;
using System.Collections.Generic;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace TicTacToeTelegramBot.Game
{
    public interface IGame
    {
        public void OnClick(CallbackQueryEventArgs e);
        public bool IsEnded { set; get;}
        public ChatId CurrentChatId { init; get; }
    }
}