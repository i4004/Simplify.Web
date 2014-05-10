using System;
using System.IO.Abstractions;
using System.Web;
using System.Web.SessionState;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace AcspNet.Web
{
	/// <summary>
	/// AcspNet HTTP module
	/// </summary>
	public class AcspHttpModule : IHttpModule
	{
		private static Lazy<IFileSystem> _fileSystemInstance = new Lazy<IFileSystem>(() => new FileSystem());
		private static Lazy<IRequestHandler> _requestHandler = new Lazy<IRequestHandler>(() => new AcspRequestHandler());

		/// <summary>
		/// Gets or sets the file system.
		/// </summary>
		/// <value>
		/// The file system.
		/// </value>
		/// <exception cref="System.ArgumentNullException"></exception>
		public static IFileSystem FileSystem
		{
			get { return _fileSystemInstance.Value; }

			set
			{
				if (value == null)
					throw new ArgumentNullException();

				_fileSystemInstance = new Lazy<IFileSystem>(() => value);
			}
		}

		/// <summary>
		/// Gets or sets the request handler.
		/// </summary>
		/// <value>
		/// The request handler.
		/// </value>
		/// <exception cref="System.ArgumentNullException"></exception>
		public static IRequestHandler RequestHandler
		{
			get { return _requestHandler.Value; }

			set
			{
				if (value == null)
					throw new ArgumentNullException();

				_requestHandler = new Lazy<IRequestHandler>(() => value);
			}
		}

		/// <summary>
		/// Registers the HTTP module.
		/// </summary>
		public static void RegisterHttpModule()
		{
			DynamicModuleUtility.RegisterModule(typeof(AcspHttpModule));
		}

		private static void ApplicationBeginRequest(Object source, EventArgs e)
		{
			((HttpApplication)source).Context.SetSessionStateBehavior(SessionStateBehavior.Required);
		}

		static void PreRequestHandlerExecute(object sender, EventArgs e)
		{
			var context = ((HttpApplication)sender).Context;

			// Exclude processing for file URLs (for css and other files to correctly processed)
			if (!FileSystem.File.Exists(context.Request.PhysicalPath))
				_requestHandler.Value.ProcessRequest(new HttpContextWrapper(context));
		}

		/// <summary>
		/// Initializes the specified application.
		/// </summary>
		/// <param name="application">The application.</param>
		public void Init(HttpApplication application)
		{
			application.BeginRequest += ApplicationBeginRequest;
			application.PreRequestHandlerExecute += PreRequestHandlerExecute;
		}

		/// <summary>
		/// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule" />.
		/// </summary>
		public void Dispose()
		{
		}
	}
}
