using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using TicTacToeTelegramBot.GameMap;

namespace TicTacToeTelegramBot.Game
{
    public class GameHub
    {
        private readonly TelegramBotClient _bot;
        private const int _maxGames = 10;
        private const int _maxMinutes = 10;
        private readonly IGameMapFactory _gameMapFactory;
        private readonly IGameFactory _gameFactory;

        private List<(DateTime TimeStart, IGame Game)> _games = new();
        public GameHub(TelegramBotClient bot)
        {
            _bot = bot;
            _bot.OnMessage += BotOnMessage;
            _gameMapFactory = new GameMapFactory(_bot);
            _gameFactory = new GameFactory(_bot);
            Task.Run(CheckGames);
        }
        private async void CheckGames()
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
                _games.RemoveAll(g => DateTime.UtcNow - g.TimeStart >= TimeSpan.FromMinutes(_maxMinutes) || g.Game.IsEnded);
            }
        }
        private async void BotOnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                string[] command = e.Message.Text.Split(' ');
                if (command.Length > 1)
                {
                    switch (command[0])
                    {
                        case "/startPvP":
                            if (_games.Count <= _maxGames && command.Length == 4)
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
                                int size = ((int.TryParse(command[3], out int result)) ? result : 3);
                                IGameMap gameMap = _gameMapFactory.GetGameMap(GameMapTypeEnum.Default, size);
                                IGame game = _gameFactory.GetGame(GameTypeEnum.PvpGame, gameMap, playerOne, playerTwo); 
                                await gameMap.StartAsync(e.Message.Chat);
                                _games.Add( (DateTime.UtcNow , game));
                            }
                            break;
                    }
                }
            }
        }
    }
}
