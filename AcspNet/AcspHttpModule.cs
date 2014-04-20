using System;
using System.Diagnostics;
using System.IO.Abstractions;
using System.Web;
using System.Web.Routing;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace AcspNet
{
	/// <summary>
	/// AcspNet HTTP module
	/// </summary>
	public class AcspHttpModule : IHttpModule
	{
		private static IFileSystem _fileSystemInstance;
		private static IAcspSettings _settings;

		/// <summary>
		/// Gets or sets the file system.
		/// </summary>
		/// <value>
		/// The file system.
		/// </value>
		/// <exception cref="System.ArgumentNullException"></exception>
		public static IFileSystem FileSystem
		{
			get { return _fileSystemInstance; }

			set
			{
				if (value == null)
					throw new ArgumentNullException();

				_fileSystemInstance = value;
			}
		}

		/// <summary>
		/// Gets or sets the AcspNet settings.
		/// </summary>
		/// <value>
		/// The ACSP settings.
		/// </value>
		/// <exception cref="System.ArgumentNullException">value</exception>
		public static IAcspSettings Settings
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
		/// Registers the HTTP module.
		/// </summary>
		public static void RegisterHttpModule()
		{
			DynamicModuleUtility.RegisterModule(typeof(AcspHttpModule));
		}

		/// <summary>
		/// Initializes the specified application.
		/// </summary>
		/// <param name="application">The application.</param>
		public void Init(HttpApplication application)
		{
			if (_fileSystemInstance == null)
				_fileSystemInstance = new FileSystem();

			if (_settings == null)
				_settings = new AcspSettings();

			application.BeginRequest += ApplicationBeginRequest;
		}

		/// <summary>
		/// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule" />.
		/// </summary>
		public void Dispose()
		{
		}

		private static void ApplicationBeginRequest(Object source, EventArgs e)
		{
			var application = (HttpApplication)source;
			var context = application.Context;

			// Exclude processing for file URLs (for css and other files to correctly processed)
			if (!FileSystem.File.Exists(context.Request.PhysicalPath))
				ProcessRequest(context);
		}

		private static void ProcessRequest(HttpContext context)
		{
			var stopWatch = new Stopwatch();
			stopWatch.Start();

			var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
			var acspContext = new AcspContext(routeData, new HttpContextWrapper(context));

			var sourceContainer = new SourceContainerFactory(acspContext, Settings).CreateContainer();
			var viewFactory = new ViewFactory(sourceContainer);
			var controllerFactory = new ControllerFactory(sourceContainer, viewFactory);
			var controllersHandler = new ControllersHandler(ControllersMetaStore.Current, acspContext.CurrentAction, acspContext.CurrentMode, controllerFactory);

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
