using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System.EnterpriseServices;

namespace BotFramework_WebTest.BotFramework
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public string userID;

        /// <summary>
        /// From the Post/API messages...
        /// ... respond to anything sent over.
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> Post([FromBody]Microsoft.Bot.Connector.Activity activity)
        {
            // Get a connecting client
            BotClient connectingClient = GetClient(activity);

            // Check the activity type
            if (activity.Type == ActivityTypes.Message) // message is a message coming to the client
            {
                string responseString = "The length of your message to me was: " + activity.Text.Length.ToString();
                BotClientUtility.SendMessage(connectingClient, responseString);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        /// <summary>
        /// Get a client connected to the system at present
        /// </summary>
        /// <param name="activity">The activity from the api controller</param>
        /// <returns></returns>
        private BotClient GetClient([FromBody]Microsoft.Bot.Connector.Activity activity)
        {
            // Get the client (if existing) from the connection
            BotClient connectingClient = Global.botClients.FirstOrDefault(bc => ((bc.BotURL == activity.ServiceUrl) && (bc.UserID == activity.From.Id)));

            // Check if we have a valid client...
            if (connectingClient == null)
            { 
                // ... else create a connecting client
                connectingClient = new BotClient();
                connectingClient.UserID = activity.From.Id;
                connectingClient.UserName = activity.From.Name;
                connectingClient.BotURL = activity.ServiceUrl;
                connectingClient.ConversationAccount = activity.Conversation;
                connectingClient.FromID = activity.Recipient.Id;
                connectingClient.ChannelType = activity.ChannelId;

                // Add it to the list of clients - will be useful for later
                Global.botClients.Add(connectingClient);

                // Tell the client that a connection has been established
                BotClientUtility.SendMessage(connectingClient, "Connection Established");
            }
            
            // Return the client for use elsewhere
            return connectingClient;
        }

        private Microsoft.Bot.Connector.Activity HandleSystemMessage(Microsoft.Bot.Connector.Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }

    /// <summary>
    /// Utility class for Bot Clients
    /// </summary>
    public static class BotClientUtility
    {
        /// <summary>
        /// Send a message to a bot client
        /// </summary>
        /// <param name="bc">The Bot client</param>
        /// <param name="message">The message to send</param>
        public static async void SendMessage(BotClient bc, string message)
        {
            ConnectorClient connector = new ConnectorClient(new Uri(bc.BotURL));
            IMessageActivity newMessage = Microsoft.Bot.Connector.Activity.CreateMessageActivity();
            newMessage.Type = ActivityTypes.Message;
            newMessage.From = new ChannelAccount(bc.FromID);
            newMessage.Conversation = bc.ConversationAccount;
            newMessage.Recipient = new ChannelAccount(bc.UserID);
            newMessage.Text = message;
            await connector.Conversations.SendToConversationAsync((Microsoft.Bot.Connector.Activity)newMessage);
        }
    }

    public class BotClient
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public ConversationAccount ConversationAccount { get; set; }
        public string BotURL { get; set; }
        public string FromID { get; set; }
        public string ChannelType { get; set; }
    }
}