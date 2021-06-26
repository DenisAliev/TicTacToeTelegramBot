using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using TicTacToeTelegramBot.GameMap;

namespace TicTacToeTelegramBot.Game
{
    public class GameHub
    {
        private readonly TelegramBotClient _bot;

        private const int _maxGames = 10; 

        private List<(DateTime TimeStart, Game)> _games = new();
        public GameHub(TelegramBotClient bot)
        {
            _bot = bot;
            _bot.OnMessage += BotOnMessage;
        }
        private async void BotOnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                string[] command = e.Message.Text.Split(' ');
                if (command.Length == 3)
                {
                    if (command[0] == "/start" && _games.Count != _maxGames)
                    {
                        Player playerOne = new Player
                        {
                            ChatId = e.Message.Chat,
                            Tag = command[1]
                        };
                        Player playerTwo = new Player
                        {
                            ChatId = e.Message.Chat,
                            Tag = command[2]
                        };
                        IGameMap gameMap = new GameMap.GameMap
                        {
                            Bot = _bot
                        };
                        await gameMap.StartAsync(e.Message.Chat);
                        _games.Add( (DateTime.UtcNow ,new Game( _bot, playerOne, playerTwo, gameMap )));
                    }
                }
            }
        }
    }
}