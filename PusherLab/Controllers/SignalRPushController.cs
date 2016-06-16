using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PusherLab.Controllers
{
    public class SignalRPushController : Controller
    {
        public ActionResult Send()
        {
            return View();
        }

        public ActionResult Read(int id)
        {
            var message = Global.JsonDb.UpdateMessageByRead(id);
            return View(message);
        }

        public ActionResult GetUnRead(string channel)
        {
            return Json(Global.JsonDb.UnReadMessages(channel), JsonRequestBehavior.AllowGet);
        }
    }
}