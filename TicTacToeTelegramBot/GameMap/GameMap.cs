using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TicTacToeTelegramBot.GameMap
{
    class GameMap : IGameMap
    {
        private readonly uint _size;
        private readonly int[,] _map;
        private readonly TelegramBotClient _bot;
        public GameMap(TelegramBotClient bot, uint size)
        {
            _bot = bot;
            _size = size;
            _map = new int[_size, _size];
        }
        private List<InlineKeyboardButton[]>RendKeys()
        {
            var keyboardButtons = new List<InlineKeyboardButton[]>();
            for (int i = 0; i < _size; i++)
            {
                var tmpArr = new InlineKeyboardButton[_size];
                for (int j = 0; j < _size; j++)
                {
                    tmpArr[j] = InlineKeyboardButton.WithCallbackData(_map[i, j] == 1 ? "X" : (_map[i, j] == 2 ? "O" : " "), $"{i}|{j}");
                }
                keyboardButtons.Add(tmpArr);
            }
            return keyboardButtons;
        }
        public async Task RenderAsync(Message message, GameMapEnum playerTurn)
        {
            await _bot.EditMessageTextAsync(message.Chat, message.MessageId, 
                $"Player - { ( (playerTurn!=GameMapEnum.PlayerOne)? "X":"O" )}");
            await _bot.EditMessageReplyMarkupAsync(message.Chat, message.MessageId,
                new InlineKeyboardMarkup(RendKeys()));
        }

        public async Task StartAsync(ChatId id)
        {
            await _bot.SendTextMessageAsync(
                chatId: id,
                text: "Player - X",
                parseMode: ParseMode.Markdown,
                disableNotification: true,
                replyMarkup: new InlineKeyboardMarkup(RendKeys())
            );
        }

        public bool SetPosition(GameMapEnum player, int x, int y)
        {
            if (_map[x, y] == 0)
            {
                _map[x, y] = ((int)player);
                return true;
            }
            return false;
        }

        private bool CheckLines(GameMapEnum player)
        {
            for (int i = 0; i < _size; i++)
            {
                int sum1 = 0;
                int sum2 = 0;
                for (int j = 0; j < _size; j++)
                {
                    sum1 += (_map[i, j] == (int) player) ? 1 : 0;
                    sum2 += (_map[j, i] == (int) player) ? 1 : 0;
                }
                if (sum1 == _size || sum2 == _size)
                {
                    return true;
                }
            }
            return false;
        }
        private bool CheckDiagonals(GameMapEnum player)
        {
            int sum1 = 0;
            int sum2 = 0;
            for (int i = 0; i < _size; i++)
            {
                sum1 += (_map[i, _size - 1 - i] == (int) player) ? 1 : 0;
                sum2 += (_map[_size - 1 - i, i] == (int) player) ? 1 : 0;
            }
            return sum1 == _size|| sum2 == _size;
        }
        
        public bool CheckWin(GameMapEnum player)
        {
            return CheckLines(player) || CheckDiagonals(player);
        }
    }
}
