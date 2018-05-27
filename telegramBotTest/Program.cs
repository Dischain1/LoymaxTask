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

namespace telegramBotTest
{
    internal class Program
    {
        private static LoymaxTaskBot bot = new LoymaxTaskBot();

        static void Main(string[] args)
        {
            Console.ReadKey();
        }
    }
}
