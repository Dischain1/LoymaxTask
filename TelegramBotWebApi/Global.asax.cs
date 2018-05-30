using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TelegramBotWebApi.Controllers;

namespace TelegramBotWebApi
{
    public class WebApiApplication : HttpApplication
    {
        public static TelegramBot.LoymaxTaskBot bot;
        public static readonly string hookUrl = "https://telegrambotwebapi.azurewebsites.net:443/api/bot/update";

        protected void Application_Start()
        {
            //AutofacConfig.ConfigureContainer();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            bot = new TelegramBot.LoymaxTaskBot(new SQLRepository.SqlRepository(connStr));
            bot.ListenWithWebhook(hookUrl);
        }
    }
}
