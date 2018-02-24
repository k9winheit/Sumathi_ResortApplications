using Business.Managers;
using Business.Managers.Interfaces;
using Microsoft.Practices.Unity;
using Sumathi_ResWebApi.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Sumathi_ResWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {          
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
               
    }
}
