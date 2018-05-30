using EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Telegram.Bot.Types;
using TelegramBot;

namespace TelegramBotWebApi.Controllers
{
    public class BotController : ApiController
    {
        [Route(@"api/bot/update")] //webhook uri
        public async Task<OkResult> Update([FromBody]Update update)
        {
            switch (update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:
                    await WebApiApplication.bot.OnWebhookedMessage(update.Message);
                    break;
                //case Telegram.Bot.Types.Enums.UpdateType.InlineQuery:
                //    await WebApiApplication.bot.SendTextAsync("123");
                //    break;
                default:
                    await WebApiApplication.bot.OnUnsupportedUpdate(update.Message);
                    break;
            }
           
            return Ok();
        }
    }
}
