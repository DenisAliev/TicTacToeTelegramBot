using System;
using Telegram.Bot;
using TicTacToeTelegramBot.Game;

namespace TicTacToeTelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var botClient = new TelegramBotClient("1799449847:AAG3hDlYDNdCHvA637hKHgUQ3D_rYFpcB5I");

            GameHub gameHub = new GameHub(botClient);
            
            botClient.StartReceiving();

            Console.ReadLine();
        }
    }
}