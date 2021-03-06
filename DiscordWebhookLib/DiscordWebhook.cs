﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DiscordWebhookLib.Discord;

namespace DiscordWebhookLib
{
    public class DiscordWebhook
    {
        private Uri Link;
        private HttpClient client = new HttpClient();
        public string DefaultWebhookName = default;
        public DiscordWebhook(string WebhookUrl)
        {
            Link = new Uri(WebhookUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
        }
        public HttpStatusCode Execute(DiscordMessage Message)
        {
            if (DefaultWebhookName != default)
            {
                Message.username = DefaultWebhookName;
            }
            if (Message.username == default)
            {
                Message.username = "Default chatbot name";
            }
            HttpResponseMessage responseMessage = client.PostAsJsonAsync<DiscordMessage>(Link, Message).Result;
            return responseMessage.StatusCode;
        }
        public HttpStatusCode Log(DiscordMessage Message)
        {
            Message.content = Message.content+"//"+ DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            return Execute(Message);
        }

       

    }
}
