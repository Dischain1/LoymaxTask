using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using SQLRepository;
using EFModel;
using System.Threading;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Args;
using System.Text.RegularExpressions;

namespace telegramBotTest
{
    class LoymaxTaskBot : IBot
    {
        // в реальном проекте этот токен был бы скрыт
        private static readonly string botAccessToken = "605947598:AAHBgcrZK9SbdYk4qf02JoTQSPOGdlrl3EI";
        private static readonly TelegramBotClient botClient = new TelegramBotClient(botAccessToken);

        private static SqlRepository rep = new SqlRepository();

        delegate Task BotCommand(MessageEventArgs e);
        Dictionary<string, BotCommand> Commands;
        public LoymaxTaskBot()
        {
            Commands = new Dictionary<string, BotCommand>
            {
                {"/register", new BotCommand(Register) },
                {"/delete", new BotCommand(DeleteUser) },
                {"/get", new BotCommand(GetUser) },
                {"/help", new BotCommand(GetUser) },
            };

            botClient.OnMessage += OnMessageRecieved;
            StartListen();
        }

        public void StartListen()
        {
            Thread listener = new Thread(new ThreadStart(ListenForMessages));
            listener.IsBackground = true;
            listener.Start();
        }

        public async Task Register(MessageEventArgs e)
        {
            var user = new User {
            };

            await rep.AddUser(user);
        }
        public async Task DeleteUser(MessageEventArgs e)
        {
            await rep.RemoveUser(1);
        }
        public async Task GetUser(MessageEventArgs e)
        {
            await rep.GetUser(1);
        }
        public async Task OnUnknownCommand(MessageEventArgs e)
        {
            await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Я знаю только комманды\r\n /register\r\n /get\r\n /del\r\n");
        }
        public Task Help(MessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        private async void OnMessageRecieved(object sender, MessageEventArgs e)
        {
            switch (e.Message.Type)
            {
                case MessageType.Text:
                    ExecuteIfCommand(e);
                    break;
                default:
                    await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Я принимаю только текст)");
                    break;
            }          
        }

        private async void ExecuteIfCommand(MessageEventArgs e)
        {
            Regex commandPattern = new Regex(@"^[/][a-z]+\s*$");
            if (commandPattern.IsMatch(e.Message.Text))
            {
                BotCommand command;
                Commands.TryGetValue(e.Message.Text, out command);

                if (command != null)
                    await command.Invoke(e);
                else
                    await OnUnknownCommand(e);
            }
            else
                await botClient.SendTextMessageAsync(e.Message.Chat.Id, "Я принимаю только комманды");
        }

        

        private void ListenForMessages()
        {
            botClient.StartReceiving();
            Console.ReadKey();
            botClient.StopReceiving();
        }

        #region проверки работоспособности
        public async void CheckBot()
        {
            var myBot = await botClient.GetMeAsync();
            Console.WriteLine($"Бот {myBot.FirstName} запущен");
        }
        private static async void CheckRepo()
        {
            var users = await rep.GetUsers();
            Console.WriteLine($"Количество пользователей: {users.Count}");
        }

       
        #endregion
    }
}
