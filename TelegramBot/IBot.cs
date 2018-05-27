using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using SQLRepository;
using EFModel;
using Telegram.Bot.Args;

namespace TelegramBot
{
    interface IBot
    {
        Task Register(MessageEventArgs e);
        Task DeleteUser(MessageEventArgs e);
        Task GetUser(MessageEventArgs e);
        Task Help(MessageEventArgs e);

        Task OnUnknownCommand(MessageEventArgs e);
    }
}
