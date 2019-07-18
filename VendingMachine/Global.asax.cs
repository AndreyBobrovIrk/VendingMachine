using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using VendingMachine.Models;

namespace VendingMachine
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RunTime = new RunTime();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<VendingMachineContext>(new DBInitializer());
        }

        static public RunTime RunTime { get; private set; }

        protected void Application_BeginRequest()
        {
            if (Request.Params["admin"] != null)
            {
                RunTime.IsAdmin = Request.Params["admin"] == "true";
            }
        }
    }
}
