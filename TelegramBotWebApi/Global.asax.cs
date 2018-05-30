using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TelegramBot;
using TelegramBotWebApi.Controllers;

namespace TelegramBotWebApi
{
    public class WebApiApplication : HttpApplication
    {
        public static TelegramBot.LoymaxTaskBot bot;
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var botToken = ConfigurationManager.AppSettings["DefaultToken"];
            var hookUrl = ConfigurationManager.AppSettings["WebhookUrl"];
            bot = new TelegramBot.LoymaxTaskBot(new SQLRepository.SqlRepository(connStr), botToken);
            bot.ListenWithWebhook(hookUrl);
        }
    }
}
