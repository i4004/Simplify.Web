using System;
using System.Diagnostics;
using System.Web;
using System.Web.Routing;

namespace AcspNet
{
	/// <summary>
	/// HTTP request handler
	/// </summary>
	public class RequestHandler : IRequestHandler
	{
		private IAcspSettings _settings;

		/// <summary>
		/// Initializes a new instance of the <see cref="RequestHandler"/> class.
		/// </summary>
		public RequestHandler()
		{
			if (_settings == null)
				_settings = new AcspSettings();
		}

		/// <summary>
		/// Gets or sets the AcspNet settings.
		/// </summary>
		/// <value>
		/// The ACSP settings.
		/// </value>
		/// <exception cref="System.ArgumentNullException">value</exception>
		public IAcspSettings Settings
		{
			get { return _settings; }

			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_settings = value;
			}
		}

		/// <summary>
		/// Processes the request.
		/// </summary>
		/// <param name="context">The context.</param>
		public void ProcessRequest(HttpContextBase context)
		{
			var stopWatch = new Stopwatch();
			stopWatch.Start();

			var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
			var acspContext = new AcspContext(routeData, context);

			var sourceContainer = new ModulesContainerFactory(acspContext, Settings).CreateContainer();
			var viewFactory = new ViewFactory(sourceContainer);
			var controllerFactory = new ControllerFactory(sourceContainer, viewFactory);
			var controllersHandler = new ControllersHandler(ControllersMetaStore.Current, controllerFactory, acspContext.CurrentAction, acspContext.CurrentMode);

			controllersHandler.CreateAndInvokeControllers();

			var pageBuilder = new PageBuilder(sourceContainer.Environment.MasterTemplateFileName, sourceContainer.TemplateFactory);
			var displayer = new Displayer(sourceContainer.Context.Response);
			var dcSetter = new DataCollectorDataSetter(sourceContainer.DataCollector);

			if (!Settings.DisableAutomaticSiteTitleSet)
				dcSetter.SetSiteTitleFromStringTable(acspContext.CurrentAction, acspContext.CurrentMode);

			dcSetter.SetEnvironmentVariables(sourceContainer.Environment);
			dcSetter.SetContextVariables(acspContext);
			dcSetter.SetLanguageVariables(sourceContainer.LanguageManager.Language);

			stopWatch.Stop();

			dcSetter.SetExecutionTimeVariable(stopWatch.Elapsed);

			displayer.DisplayNoCache(pageBuilder.Buid(sourceContainer.DataCollector.Items));

			context.Response.End();
		}
	}
}