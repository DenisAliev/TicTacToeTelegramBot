using System;
using Telegram.Bot;
using TicTacToeTelegramBot.Game;

namespace TicTacToeTelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var botClient = new TelegramBotClient("1879835337:AAHifY9W5AEYJi7VKPgx03KXI5yuIRkuVTE");

            GameHub gameHub = new GameHub(botClient);
            
            botClient.StartReceiving();

            Console.ReadLine();
        }
    }
}