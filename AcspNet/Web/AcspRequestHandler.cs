using System;
using System.Diagnostics;
using System.Web;
using AcspNet.Meta;
using AcspNet.Modules;

namespace AcspNet.Web
{
	/// <summary>
	/// HTTP request handler
	/// </summary>
	public class AcspRequestHandler : IRequestHandler
	{
		private IAcspSettings _settings;
		private IControllersMetaStore _currentMetaStore;

		/// <summary>
		/// Gets or sets the current ACSP application instance.
		/// </summary>
		/// <value>
		/// The current.
		/// </value>
		/// <exception cref="System.ArgumentNullException">value</exception>
		public IControllersMetaStore MetaStore
		{
			get
			{
				return _currentMetaStore ?? (_currentMetaStore = new ControllersMetaStore());
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_currentMetaStore = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AcspRequestHandler"/> class.
		/// </summary>
		public AcspRequestHandler()
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

			var acspContext = new AcspContext(context);

			var sourceContainer = new ModulesContainerFactory(acspContext, Settings).CreateContainer();
			var viewFactory = new ViewFactory(sourceContainer);
			var controllerFactory = new ControllerFactory(sourceContainer, viewFactory);
			var displayer = new Displayer(sourceContainer.Context.Response);
			var executionAgent = new ControllerExecutionAgent(sourceContainer.Authentication, acspContext.CurrentAction, acspContext.CurrentMode,
				context.Request.HttpMethod);
			var controllersHandler = new ControllersHandler(MetaStore, controllerFactory,
				executionAgent);

			var result = controllersHandler.Execute();

			switch (result)
			{
				case ControllersHandlerResult.AjaxRequest:
					displayer.DisplayNoCache(controllersHandler.AjaxResult);
					break;
				case ControllersHandlerResult.Ok:
					if (acspContext.Request.Url != null)
						sourceContainer.Navigator.PreviousPageLink = acspContext.Request.Url.AbsoluteUri;

					var pageBuilder = new PageBuilder(sourceContainer.Environment.MasterTemplateFileName,
						sourceContainer.TemplateFactory);
					var dcSetter = new DataCollectorDataSetter(sourceContainer.DataCollector);

					if (!Settings.DisableAutomaticSiteTitleSet)
						dcSetter.SetSiteTitleFromStringTable(acspContext.CurrentAction, acspContext.CurrentMode);

					dcSetter.SetEnvironmentVariables(sourceContainer.Environment);
					dcSetter.SetContextVariables(acspContext);
					dcSetter.SetLanguageVariables(sourceContainer.LanguageManager.Language);

					stopWatch.Stop();

					dcSetter.SetExecutionTimeVariable(stopWatch.Elapsed);

					displayer.DisplayNoCache(pageBuilder.Buid(sourceContainer.DataCollector.Items));
					break;
				case ControllersHandlerResult.Http400:
					acspContext.Response.StatusCode = 400;
					break;
				case ControllersHandlerResult.Http403:
					acspContext.Response.StatusCode = 403;
					break;
				case ControllersHandlerResult.Http404:
					acspContext.Response.StatusCode = 404;
					break;
			}

			context.Response.End();
		}
	}
}