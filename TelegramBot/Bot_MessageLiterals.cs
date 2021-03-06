﻿using System;
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
        private struct ReplyText
        {
            public static string UnsupportedType = "Я принимаю только текстовые сообщения";
            public static string AlreadyRegistered = "Вы уже были зарегистрированы";
            public static string SuccessfullyRegistered = "Пользователь успешно зарегистрирован";
            public static string WrongRegisterInput = "Для регистрации введите ФИО и дату рождения в указаном формате\r\n /register Иванов Иван Иванович дд.мм.гггг";
            public static string UserNotFound = "Пользователь не зарегистрирован";
            public static string SuccessfullyDeleted = "Пользователь удален из БД";
            public static string BotSupportedCommands = $"Я обрабатываю комманды{Environment.NewLine}/start - Клавиатура{Environment.NewLine}/register - Зарегистрироваться{Environment.NewLine}/get - Получить данные о пользователе{Environment.NewLine}/delete - Удалить регистрацию";
            public static string WrongDateRange = $"Некорректно введенная дата. Допустимый диапазон {OldestPossibleDate.ToShortDateString()} - {DateTime.Now.ToShortDateString()}";
            public static string WrongDate = $"Некорректно введенная дата";
        }
    }
}
