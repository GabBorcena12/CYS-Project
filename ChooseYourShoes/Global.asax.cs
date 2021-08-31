using ChooseYourShoes.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ChooseYourShoes
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


        }
        protected void Session_Start()
        {
            if (Session["USER_SESSION"] == null)
            {
                UserSettings userSettings = new UserSettings();
                string userId = Request.LogonUserIdentity.Name.ToString();

                var employeeDetails = userSettings.GetEmployeeDetails(userId);

                if (employeeDetails.UserId != null)
                {
                    Session["USER_SESSION"] = employeeDetails;
                }
                else
                {
                    // Redirect to error page (Failed to authenticate user due to null userId).
                    // Log to ErrorLogging
                }
            }
        }
        protected void Session_End()
        {
            Session["USER_SESSION"] = null;
        }

        protected void Application_Error()
        {
            Exception err = Server.GetLastError().GetBaseException();
            string url = Request.Url.ToString();
            string message = err.Message;

            Session["ErrorURl"] = url;
            Session["ErrorMessage"] = message;
        }
    }
}
