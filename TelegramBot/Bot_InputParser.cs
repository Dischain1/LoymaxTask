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
using Telegram.Bot.Types;

namespace TelegramBot
{
    public partial class LoymaxTaskBot : IWebhookBot
    {
        private async Task ExecuteIfCommand(Message message)
        {
            Regex commandWordPattern = new Regex(@"^[/][a-z]+");
            var match = commandWordPattern.Match(message.Text);

            if (match.Success)
            {
                BotCommandDelegate commandDelegate;
                Commands.TryGetValue(match.Value, out commandDelegate);
                if (commandDelegate != null)
                    await commandDelegate.Invoke(message);
                else
                    await OnUnknownCommand(message);
            }
            else
                await botClient.SendTextMessageAsync(message.Chat.Id, "Я принимаю только комманды", replyToMessageId: message.MessageId);
        }

        const int userInputFieldsCount = 5;
        enum InputFields
        {
            CommandWord = 0,
            Name,
            Surname,
            Patronymic,
            DateOfBirth
        }
        static readonly Regex namePattern = new Regex(@"^[a-zA-Zа-яА-Я]{1,50}?\z");
        static readonly Regex datePattern = new Regex(@"^\d{1,2}?.\d{1,2}?.\d{4}\z");

        private static EFModel.User ConstructUserFromInput(Message message)
        {
            string[] inputData = Regex.Split(message.Text, @"\s");
            if (inputData.Length == userInputFieldsCount &&
                namePattern.IsMatch(inputData[(int)InputFields.Name]) &&
                namePattern.IsMatch(inputData[(int)InputFields.Surname]) &&
                namePattern.IsMatch(inputData[(int)InputFields.Patronymic]) &&
                datePattern.IsMatch(inputData[(int)InputFields.DateOfBirth]))
            {
                try
                {
                    var inputDate = Convert.ToDateTime(inputData[(int)InputFields.DateOfBirth]);
                    return new EFModel.User
                    {
                        Id = message.From.Id,
                        TelegramUserId = message.From.Id,
                        Name = inputData[(int)InputFields.Name],
                        Surname = inputData[(int)InputFields.Surname],
                        Patronymic = inputData[(int)InputFields.Patronymic],
                        DateOfBirth = inputDate
                    };
                }
                catch{}
            }
            return null;
        }
    }
}
