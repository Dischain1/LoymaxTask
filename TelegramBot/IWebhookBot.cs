using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using SQLRepository;
using EFModel;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace TelegramBot
{
    public interface IWebhookBot
    {
        void ListenWithWebhook(string hook);
        void StartListen();

        void OnMessageRecieved(object sender, MessageEventArgs e);
        Task OnWebhookedMessage(Message message);
        Task OnUnsupportedUpdate(Message message);

        //void TestDBAsync();
    }
}
