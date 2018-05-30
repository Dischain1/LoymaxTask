using Autofac;
using Autofac.Integration.WebApi;
using EFModel;
using SQLRepository;
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
    public static class ContainerConfig
    {
        //public static void Configure()
        //{
        //    var builder = new ContainerBuilder();

        //    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //    builder.RegisterType<SqlRepository>().As<IRepository>()
        //        .WithParameter(new TypedParameter(typeof(string), connStr));

        //    string botToken = ConfigurationManager.AppSettings["DefaultToken"];
        //    builder.RegisterType<LoymaxTaskBot>().As<IWebhookBot>().
        //        WithParameter(new TypedParameter(typeof(string), botToken));

        //    var container = builder.Build();
        //    var resolver = new AutofacWebApiDependencyResolver(container);
        //    GlobalConfiguration.Configuration.DependencyResolver = resolver;
        //}

        //public static IContainer Configure()
        //{
        //    var builder = new ContainerBuilder();

        //    string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //    builder.RegisterType<SqlRepository>().As<IRepository>()
        //        .WithParameter(new TypedParameter(typeof(string), connStr));

        //    string botToken = ConfigurationManager.AppSettings["DefaultToken"];
        //    builder.RegisterType<LoymaxTaskBot>().As<IWebhookBot>().
        //        WithParameter(new TypedParameter(typeof(string), botToken));

        //    return builder.Build();
        //}

        // слету не получилось, нужно почитать
    }

    public class WebApiApplication : HttpApplication
    {
        public static TelegramBot.LoymaxTaskBot bot;
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //ContainerConfig.Configure();
            //var bot = GlobalConfiguration.Configuration.DependencyResolver.GetRootLifetimeScope().Resolve<IWebhookBot>();

            //var container = ContainerConfig.Configure();
            //var bot = container.Resolve<IWebhookBot>();

            var connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var botToken = ConfigurationManager.AppSettings["DefaultToken"];
            string webhook = ConfigurationManager.AppSettings["WebhookUrl"];
            bot = new TelegramBot.LoymaxTaskBot(new SQLRepository.SqlRepository(connStr), botToken);
            bot.ListenWithWebhook(webhook);

            //bot.StartListen();
        }
    }
}
