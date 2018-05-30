using System.Threading.Tasks;
using Telegram.Bot;
using EFModel;
using System.Threading;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using System;

namespace TelegramBot
{
    public partial class LoymaxTaskBot: IWebhookBot
    {
        private static TelegramBotClient botClient;

        private static IRepository rep;

        public LoymaxTaskBot(IRepository repository, string botToken)
        {
            rep = repository;
            botClient = new TelegramBotClient(botToken);
        }

        public void ListenWithWebhook(string hookUrl)
        {
             botClient.SetWebhookAsync(hookUrl);
        }
        public async Task OnWebhookedMessage(Message message)
        {
            switch (message.Type)
            {
                case MessageType.Text:
                    await ExecuteIfCommand(message);
                    break;
                default:
                    await botClient.SendTextMessageAsync(message.Chat.Id, ReplyText.UnsupportedType, replyToMessageId: message.MessageId);
                    break;
            }
        }

        public async Task OnUnsupportedUpdate(Message message)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, ReplyText.UnsupportedType, replyToMessageId: message.MessageId);
        }

        #region локальная отладка
        public async void StartListen()
        {
            await botClient.SetWebhookAsync("");
            botClient.OnMessage += OnMessageRecieved;
            botClient.OnCallbackQuery += OnCallbackQueryReceived;
            Thread listener = new Thread(new ThreadStart(() => { botClient.StartReceiving(); }));
            listener.IsBackground = true;
            listener.Start();
        }
        public async void OnMessageRecieved(object sender, MessageEventArgs e)
        {
            try
            {
                switch (e.Message.Type)
                {
                    case MessageType.Text:
                        await ExecuteIfCommand(e.Message);
                        break;
                    default:
                        await botClient.SendTextMessageAsync(e.Message.Chat.Id, ReplyText.UnsupportedType, replyToMessageId: e.Message.MessageId);
                        break;
                }
            }
            catch (Exception exeption)
            {
                await botClient.SendTextMessageAsync(e.Message.Chat.Id, $"Возникла ошибка {exeption.Message}", replyToMessageId: e.Message.MessageId);
            }        
        }
        private static async void OnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var callbackQuery = callbackQueryEventArgs.CallbackQuery;
            await botClient.AnswerCallbackQueryAsync(callbackQuery.Id,$"Получен ответ {callbackQuery.Data}");
        }
        #endregion

        public async Task SendTextAsync(string text, int chatId = 528397367)
        {
            await botClient.SendTextMessageAsync(chatId, text);
        }
    }
}
