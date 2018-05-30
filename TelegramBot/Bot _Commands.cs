using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public partial class LoymaxTaskBot : IWebhookBot
    {
        public delegate Task BotCommandDelegate(Message e);
        public static Dictionary<string, BotCommandDelegate> Commands = new Dictionary<string, BotCommandDelegate>
        {
            {"/register", new BotCommandDelegate(Register) },
            {"/delete", new BotCommandDelegate(DeleteUser) },
            {"/get", new BotCommandDelegate(GetUser) },
            {"/start", new BotCommandDelegate(Start) },
        };

        public static async Task Register(Message msg)
        {
            var user = ConstructUserFromInput(msg);
            if (user != null)
            {
                if (await rep.GetUser(msg.From.Id) == null)
                {
                    await rep.AddUser(user);
                    await botClient.SendTextMessageAsync(msg.Chat.Id, ReplyText.SuccessfullyRegistered, replyToMessageId: msg.MessageId);
                }
                else
                    await botClient.SendTextMessageAsync(msg.Chat.Id, ReplyText.AlreadyRegistered, replyToMessageId: msg.MessageId);
            }
            else
                await botClient.SendTextMessageAsync(msg.Chat.Id, ReplyText.WrongRegisterInput, replyToMessageId: msg.MessageId);
        }

        public static async Task DeleteUser(Message msg)
        {
            var user = await rep.GetUser(msg.From.Id);
            if (user != null)
            {
                await rep.RemoveUser(msg.From.Id);
                await botClient.SendTextMessageAsync(msg.Chat.Id, ReplyText.SuccessfullyDeleted, replyToMessageId: msg.MessageId);
            }
            else
                await botClient.SendTextMessageAsync(msg.Chat.Id, ReplyText.UserNotFound, replyToMessageId: msg.MessageId);
        }

        public static async Task GetUser(Message msg)
        {
            var user = await rep.GetUser(msg.From.Id);
            if (user != null)
            {
                string reply = $"Пользователь найден.\r\nФамилия: {user.Surname}\r\nИмя: {user.Name}\r\nОтчество: {user.Patronymic}\r\nДата рождения: {user.DateOfBirth.ToShortDateString()}";
                await botClient.SendTextMessageAsync(msg.Chat.Id, reply, replyToMessageId: msg.MessageId);
            }
            else
                await botClient.SendTextMessageAsync(msg.Chat.Id, ReplyText.UserNotFound, replyToMessageId: msg.MessageId);
        }

        public async Task OnUnknownCommand(Message msg)
        {
            await botClient.SendTextMessageAsync(msg.Chat.Id, ReplyText.BotSupportedCommands, replyToMessageId: msg.MessageId);
        }

        public static async Task Start(Message msg)
        {
            var keyboard = new ReplyKeyboardMarkup
            {
                Keyboard = new[] 
                {
                    new KeyboardButton[]
                    {
                        "/register",
                    },
                    new KeyboardButton[]
                    {
                        "/get",
                        "/delete",
                    }
                }
            };
            await botClient.SendTextMessageAsync(msg.Chat.Id, ReplyText.BotSupportedCommands, replyToMessageId: msg.MessageId, replyMarkup: keyboard);
        }
    }
}
