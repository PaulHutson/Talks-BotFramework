using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using BotFramework_WebTest.BotFramework;

namespace BotFramework_WebTest
{
    public class Global : System.Web.HttpApplication
    {
        public static List<BotClient> botClients = new List<BotClient>();

        protected void Application_Start(object sender, EventArgs e)
        {
            botClients = new List<BotClient>();
            GlobalConfiguration.Configure(WebAPIConfig.Register);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}