using System;
using System.Configuration;
using Es.Udc.DotNet.ModelUtil.Log;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Es.Udc.DotNet.SportsNow.Web.HTTP.Session;

namespace Es.Udc.DotNet.SportsNow.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

            /*
             * We read the UnityConfigurationSection from the default 
             * configuration file, Web.config, and then populate the 
             * UnityContainer.
             */
            IUnityContainer container = new UnityContainer();

            UnityConfigurationSection section =
                (UnityConfigurationSection)ConfigurationManager.GetSection("unity");

            section.Configure(container, section.Containers.Default.Name);


            Application["UnityContainer"] = container;

            LogManager.RecordMessage("Unity Container started", MessageType.Info);
           
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            SessionManager.TouchSession(Context);

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            ((UnityContainer)Application["UnityContainer"]).Dispose();

            LogManager.RecordMessage("Unity Container disposed", MessageType.Info);   
        }
    }
}