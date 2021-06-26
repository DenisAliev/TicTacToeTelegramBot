using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TicTacToeTelegramBot.GameMap
{
    class GameMap : IGameMap
    {
        private uint _size = 3;
        private int[,] _map = new int[3, 3];
        public TelegramBotClient Bot { init; get; }
        private InlineKeyboardButton[][] RendKeys()
        {
            var keyboardButtons = new InlineKeyboardButton[_size][];
            for (int i = 0; i < _size; i++)
            {
                keyboardButtons[i] = new InlineKeyboardButton[_size];
                for (int j = 0; j < _size; j++)
                {
                    keyboardButtons[i][j] = InlineKeyboardButton.WithCallbackData(_map[i, j] == 1 ? "X" : (_map[i, j] == 2 ? "O" : ""), $"{i}|{j}");
                }
            }
            return keyboardButtons;
        }
        public async Task RenderAsync(Message message, GameMapEnum playerTurn)
        {
            await Bot.EditMessageTextAsync(message.Chat, message.MessageId, 
                $"Player - { ( (playerTurn==GameMapEnum.PlayerOne)? "X":"O" )}");
            await Bot.EditMessageReplyMarkupAsync(message.Chat, message.MessageId,
                new InlineKeyboardMarkup(RendKeys()));
        }

        public async Task StartAsync(ChatId id)
        {
            await Bot.SendTextMessageAsync(
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
            Console.WriteLine("this field is already occupied ");
            return false;
        }

        public bool CheckWin()
        {
            return false;
        }
    }
}
