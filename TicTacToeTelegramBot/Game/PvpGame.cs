using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using TicTacToeTelegramBot.GameMap;

namespace TicTacToeTelegramBot.Game
{
    public class PvpGame: IGame
    {
        private readonly Player _playerOne;
        private readonly Player _playerTwo;
        private GameMapEnum _playerTurn;
        private readonly TelegramBotClient _bot;
        private readonly IGameMap _gameMap;
        public bool IsEnded { set; get; }
        public ChatId CurrentChatId { init; get; }

        public PvpGame( TelegramBotClient bot, ChatId chatId, Player playerOne, Player playerTwo, IGameMap gameMap)
        {
            _playerOne = playerOne;
            _playerTwo = playerTwo;
            _bot = bot;
            _playerTurn = GameMapEnum.PlayerOne;
            _gameMap = gameMap;
            CurrentChatId = chatId;
        }

        public async void OnClick(CallbackQueryEventArgs e)
        {
            int[] coords = e.CallbackQuery.Data.Split('|').Select(x => int.Parse(x)).ToArray();
            var curPlayer = (_playerTurn == GameMapEnum.PlayerOne) ? _playerOne : _playerTwo;
            if ("@" + e.CallbackQuery.From.Username == curPlayer.Tag)
            {
                if (_gameMap.SetPosition(_playerTurn, coords[0], coords[1]))
                {
                    await _gameMap.RenderAsync(e.CallbackQuery.Message, _playerTurn);
                    if (_gameMap.CheckWin(_playerTurn))
                    {
                        await _bot.SendTextMessageAsync(e.CallbackQuery.Message.Chat,
                            $"Player {curPlayer.Tag} is winner :)");
                        IsEnded = true;
                    }
                    else
                    {
                        _playerTurn = (_playerTurn == GameMapEnum.PlayerOne)
                            ? GameMapEnum.PlayerTwo
                            : GameMapEnum.PlayerOne;
                    }
                }
            }
        }

    }
}