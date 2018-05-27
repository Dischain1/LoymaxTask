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

namespace TelegramBot
{
    public partial class LoymaxTaskBot : IBot
    {
        // в реальном проекте этот токен был бы скрыт в игнорируемом гитом конфиг. файле
        private static readonly string botAccessToken = "605947598:AAHBgcrZK9SbdYk4qf02JoTQSPOGdlrl3EI";
        private static readonly TelegramBotClient botClient = new TelegramBotClient(botAccessToken);

        private static SqlRepository rep = new SqlRepository();

        public LoymaxTaskBot(string hook)
        {
            SetCommands();
            botClient.SetWebhookAsync(hook);
        }

        private async void ExecuteIfCommand(MessageEventArgs e)
        {
            Regex commandPattern = new Regex(@"^[/][a-z]+\s+*$");
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
        }

    }
}
