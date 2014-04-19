using System;
using System.Web;
using System.Web.Routing;

namespace AcspNet
{
	/// <summary>
	/// AcspNet HTTP module
	/// </summary>
	class AcspHttpModule : IHttpModule, IDisposable
	{
		public void Init(HttpApplication application)
		{
			application.BeginRequest += ApplicationBeginRequest;
			application.EndRequest += ApplicationEndRequest;
		}

		private static void ApplicationBeginRequest(Object source, EventArgs e)
		{
			var application = (HttpApplication)source;
			var context = application.Context;

			var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
			var acspContext = new AcspContext(routeData, new HttpContextWrapper(context));
			var acspSettings = new AcspSettings();
			var modulesContainerFactory = new SourceContainerFactory(acspContext, acspSettings);
			var contauinerFactory = new ContainerFactory(modulesContainerFactory.CreateContainer());
			var controllersHandler = new ControllersHandler(ControllersMetaStore.Current, acspContext.CurrentAction,
				acspContext.CurrentMode, contauinerFactory);

			controllersHandler.CreateAndInvokeControllers();

			//context.Response.Write("Hello!");
			context.Response.End();
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
