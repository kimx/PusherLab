using PusherLab.Library;
using PusherLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace PusherLab
{
    public class Global : System.Web.HttpApplication
    {
        public static JsonDataBase JsonDb;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Pusher", action = "Send", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            string folder = System.Web.HttpContext.Current.Server.MapPath("~/App_Data");
            JsonDb = new JsonDataBase(folder);

        }

        protected void Application_OnPostAuthenticateRequest(object sender, EventArgs e)
        {
            // Get a reference to the current User
            IPrincipal currentUser = HttpContext.Current.User;

            // If we are dealing with an authenticated forms authentication request
            if (currentUser.Identity.IsAuthenticated
                && currentUser.Identity.GetType() != typeof(CustomIdentity))
            {
                // Create a CustomIdentity based on the FormsAuthenticationTicket           
                var identity = new CustomIdentity(((FormsIdentity)currentUser.Identity).Ticket);

                // Create the CustomPrincipal
                var user = new GenericPrincipal(identity, null);

                // Attach the CustomPrincipal to HttpContext.User and Thread.CurrentPrincipal
                HttpContext.Current.User = user;
                Thread.CurrentPrincipal = user;
            }
        }
    }
}