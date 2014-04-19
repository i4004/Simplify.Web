using System;
using System.Diagnostics;
using System.IO.Abstractions;
using System.Web;
using System.Web.Routing;

namespace AcspNet
{
	/// <summary>
	/// AcspNet HTTP module
	/// </summary>
	class AcspHttpModule : IHttpModule
	{
		private static IFileSystem _fileSystemInstance;

		/// <summary>
		/// Gets or sets the file system.
		/// </summary>
		/// <value>
		/// The file system.
		/// </value>
		/// <exception cref="System.ArgumentNullException"></exception>
		public static IFileSystem FileSystem
		{
			get { return _fileSystemInstance ?? (_fileSystemInstance = new FileSystem()); }

			set
			{
				if (value == null)
					throw new ArgumentNullException();

				_fileSystemInstance = value;
			}
		}

		public void Init(HttpApplication application)
		{
			application.BeginRequest += ApplicationBeginRequest;
		}

		public void Dispose()
		{
		}

		private static void ApplicationBeginRequest(Object source, EventArgs e)
		{
			var application = (HttpApplication)source;
			var context = application.Context;

			// Exclude processing for files URLs

			if (FileSystem.File.Exists(context.Request.PhysicalPath))
				ProcessRequest(context);

		}

		private static void ProcessRequest(HttpContext context)
		{
			var stopWatch = new Stopwatch();
			stopWatch.Start();

			var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
			var acspContext = new AcspContext(routeData, new HttpContextWrapper(context));
			var acspSettings = new AcspSettings();
			var sourceContainer = new SourceContainerFactory(acspContext, acspSettings).CreateContainer();
			var contauinerFactory = new ContainerFactory(sourceContainer);
			var controllersHandler = new ControllersHandler(ControllersMetaStore.Current, acspContext.CurrentAction, acspContext.CurrentMode, contauinerFactory);

			controllersHandler.CreateAndInvokeControllers();

			var pageBuilder = new PageBuilder(sourceContainer.Environment.MasterTemplateFileName, sourceContainer.TemplateFactory);
			var displayer = new Displayer(sourceContainer.Context.Response);

			if (!acspSettings.DisableAutomaticSiteTitleSet)
				SiteTitleSetter.SetSiteTitleFromStringTable(sourceContainer.DataCollector, acspContext.CurrentAction, acspContext.CurrentMode);

			stopWatch.Stop();

			SystemVariablesSetter.Set(sourceContainer.DataCollector, sourceContainer.Environment, acspContext, stopWatch.Elapsed, sourceContainer.LanguageManager.Language);

			displayer.DisplayNoCache(pageBuilder.Buid(sourceContainer.DataCollector.Items));

			context.Response.End();
		}
	}
}
