using BotFramework_WebTest.BotFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BotFramework_WebTest
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_PingEVERYONE_Click(object sender, EventArgs e)
        {
            foreach (BotClient bc in Global.botClients)
            {
                BotClientUtility.SendMessage(bc, "PINNNNGGGGG");
            }
        }
    }
}