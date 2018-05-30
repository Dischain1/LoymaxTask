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
using Autofac;

namespace telegramBotTest
{
    public static class ContainerConfig
    {
        private static readonly string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SQLRepository.UserContext;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static Autofac.IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<LoymaxTaskBot>().As<IWebhookBot>();
            builder.RegisterType<SqlRepository>().As<IRepository>()
                .WithParameter(new TypedParameter(typeof(string), connStr));

            return builder.Build();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var container = ContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var bot = scope.Resolve<IWebhookBot>();
                bot.StartListen();
            }
            Console.ReadKey();
        }
    }
}
