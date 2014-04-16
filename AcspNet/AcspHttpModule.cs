using System;
using System.Web;

namespace AcspNet
{
	class AcspHttpModule : IHttpModule, IDisposable
	{
		private static IRoutesProvider _routesProvider;

		public static IRoutesProvider RoutesProvider
		{
			get { return _routesProvider ?? (_routesProvider = new DefaultRoutesProvider()); }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_routesProvider = value;
			}
		}

		public void Init(HttpApplication application)
		{
			application.BeginRequest += ApplicationBeginRequest;
			application.EndRequest += ApplicationEndRequest;
		}

		private static void ApplicationBeginRequest(Object source, EventArgs e)
		{
			//var application = (HttpApplication)source;
			//var context = application.Context;
			//context.Response.Write("<h1><font color=red>HelloWorldModule: Beginning of Request</font></h1><hr>");

			//var a = 

			//context.Response.Write("Hello!");
			//context.Response.End();
		}

		// Your EndRequest event handler.
		private static void ApplicationEndRequest(Object source, EventArgs e)
		{
			//var application = (HttpApplication)source;
			//var context = application.Context;
		}

		public void Dispose()
		{
		}
	}
}
