using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using SQLRepository;
using EFModel;
using System.ComponentModel;
using System.Threading;
using TelegramBot;

namespace telegramBotTest
{
    internal class Program
    {
        private static LoymaxTaskBot bot = new LoymaxTaskBot(new SQLRepository.SqlRepository());

        static void Main(string[] args)
        {
            bot.StartListen();
            Console.ReadKey();
        }
    }
}
