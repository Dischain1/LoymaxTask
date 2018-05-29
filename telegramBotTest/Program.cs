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
        private static readonly string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SQLRepository.UserContext;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        static LoymaxTaskBot bot; 

        static void Main(string[] args)
        {
            bot = new LoymaxTaskBot(new SQLRepository.SqlRepository(connStr));
            bot.StartListen();
            Console.ReadKey();
        }
    }
}
