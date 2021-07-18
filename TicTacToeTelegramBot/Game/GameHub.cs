using System;
using System.Collections.Generic;
using System.Linq;
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

        private List<GameSession> _games = new();

        public GameHub(TelegramBotClient bot)
        {
            _bot = bot;
            _bot.OnMessage += BotOnMessage;
            _bot.OnCallbackQuery += BotOnClick;
            _gameMapFactory = new GameMapFactory(_bot);
            _gameFactory = new GameFactory(_bot);
            Task.Run(CheckGames);
        }

        private async void BotOnClick(object sender, CallbackQueryEventArgs e)
        {
            _games.Select(g => g.Game).Where(g => g.CurrentChatId.Identifier == e.CallbackQuery.Message.Chat.Id).ToList()
                .ForEach(g =>
                {
                    g.OnClick(e);
                });
        }

        private async void CheckGames()
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                _games.Where(g => DateTime.UtcNow - g.TimeStart >= TimeSpan.FromMinutes(_maxMinutes) || g.Game.IsEnded)
                    .ToList()
                    .ForEach(g =>
                    {
                        _games.Remove(g);
                    });
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
                                var tmpGame = _games.SingleOrDefault(g => g.Game.CurrentChatId.Identifier == e.Message.Chat.Id);
                                if (tmpGame != null)
                                {
                                    _games.Remove(tmpGame);
                                }

                                var tagOne = command[1];
                                var tagTwo = command[2];

                                Player playerOne = new Player {ChatId = e.Message.Chat, Tag = tagOne};
                                Player playerTwo = new Player {ChatId = e.Message.Chat, Tag = tagTwo};
                                int size = ((int.TryParse(command[3], out int result)) ? result : 3);
                                IGameMap gameMap = _gameMapFactory.GetGameMap(GameMapTypeEnum.Default, size);
                                IGame game = _gameFactory.GetGame(GameTypeEnum.PvpGame, gameMap, e.Message.Chat.Id,
                                    playerOne, playerTwo);
                                await gameMap.StartAsync(e.Message.Chat);
                                _games.Add(new() {TimeStart = DateTime.UtcNow, Game = game});
                            }

                            break;
                    }
                }
            }
        }
    }
}