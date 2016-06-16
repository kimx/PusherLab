using PusherLab.Library;
using PusherServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PusherLab.Controllers
{
    public class PusherController : Controller
    {
        string appId = "xxx";
        string appKey = "xxx";
        string appSecret = "xxx";
        string channel = "1000_GRV";
        // GET: Pusher
        public ActionResult Send()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Send(string name)
        {
            var message = new MessageDto { Name = name, Channel = channel, Message = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") };
            Global.JsonDb.InsertMessage(message);
            var pusher = CreatePusher();
            var result = pusher.Trigger(channel, "my_event", message);
            return View();
        }

        public ActionResult Read(int id)
        {
            var message = Global.JsonDb.UpdateMessageByRead(id);
            var pusher = CreatePusher();
            var result = pusher.Trigger(channel, "read_event", message);
            return View(message);
        }

        private Pusher CreatePusher()
        {
            var options = new PusherOptions();
            options.Encrypted = true;
            var pusher = new Pusher(appId, appKey, appSecret, options);
            return pusher;
        }

        public ActionResult GetUnRead()
        {
            return Json(Global.JsonDb.UnReadMessages(channel), JsonRequestBehavior.AllowGet);
        }
    }
}