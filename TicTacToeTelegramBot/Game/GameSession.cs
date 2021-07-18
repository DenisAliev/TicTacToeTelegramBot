using System;

namespace TicTacToeTelegramBot.Game
{
    public class GameSession
    {
        public DateTime TimeStart { set; get; }
        public IGame Game {set; get;}
    }
}