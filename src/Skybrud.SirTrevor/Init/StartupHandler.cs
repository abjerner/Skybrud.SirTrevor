using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using Umbraco.Core;
using Umbraco.Web.Routing;

namespace Sniper.Umbraco.SirTrevor
{
    public class StartupHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            base.ApplicationStarted(umbracoApplication, applicationContext);
        }

    }
}
