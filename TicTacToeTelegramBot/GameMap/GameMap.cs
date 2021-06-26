using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TicTacToeTelegramBot.GameMap
{
    class GameMap : IGameMap
    {

        private int[,] map = new int[3, 3];
        public TelegramBotClient Bot { get; set; }

        public async Task RenderAsync(ChatId id)
        {
            var a = new InlineKeyboardButton[3][];
            for (int i = 0; i < 3; i++)
            {
                a[i] = new InlineKeyboardButton[3];
                for (int j = 0; j < 3; j++)
                {
                    a[i][j] = InlineKeyboardButton.WithCallbackData(map[i, j] == 1 ? "O" : (map[i, j] == 2 ? "X" : ""), $"{i}|{j}");
                }
                Console.WriteLine();
            }

            Message message = await Bot.SendTextMessageAsync(
                chatId: id,
                text: "Trying *all the parameters* of sendMessage method",
                parseMode: ParseMode.Markdown,
                disableNotification: true,
                replyMarkup: new InlineKeyboardMarkup(a)
            );
        }

        public bool SetPosition(GameMapEnum player, int x, int y)
        {
            if (map[x, y] == 0)
            {
                map[x, y] = ((int)player);
                return true;
            }
            else
            {
                Console.WriteLine("this field is already occupied ");
                return false;
            }
        }
    }
}
