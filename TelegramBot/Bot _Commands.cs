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
        delegate Task BotCommand(MessageEventArgs e);
        Dictionary<string, BotCommand> Commands;
        public void SetCommands()
        {
            Commands = new Dictionary<string, BotCommand>
            {
                {"/register", new BotCommand(Register) },
                {"/delete", new BotCommand(DeleteUser) },
                {"/get", new BotCommand(GetUser) },
                {"/help", new BotCommand(GetUser) },
            };
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
    }
}
