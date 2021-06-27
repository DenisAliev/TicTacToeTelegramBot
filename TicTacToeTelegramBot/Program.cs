using System;
using Telegram.Bot;
using TicTacToeTelegramBot.Game;

namespace TicTacToeTelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var botClient = new TelegramBotClient("YOUR TOKEN");

            GameHub gameHub = new GameHub(botClient);
            
            botClient.StartReceiving();

            Console.ReadLine();
        }
    }
}