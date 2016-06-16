using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace PusherLab.Library
{
    public class messageHub : Hub
    {
        public void Register(string groupName)
        {
            Groups.Add(Context.ConnectionId, groupName);
            var messages = Global.JsonDb.UnReadMessages(groupName);
            //System.Threading.Thread.Sleep(1200);
            //  Clients.Group(groupName).registerCompleted(messages);需要Sleep,否則通知不到..
            Clients.All.registerCompleted(messages);
        }

        public void Send(string groupName, string name)
        {
            var message = new MessageDto { Name = name, Channel = groupName, Message = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") };
            Global.JsonDb.InsertMessage(message);
            Clients.Group(groupName).addMessage(message);
        }
        public void NotifyRead(int id)
        {
            var message = Global.JsonDb.UpdateMessageByRead(id);
            Clients.Group(message.Channel).removeMessage(message);
        }


    }
}