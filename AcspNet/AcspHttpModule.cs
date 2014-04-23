using System;
using System.IO.Abstractions;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace AcspNet
{
	/// <summary>
	/// AcspNet HTTP module
	/// </summary>
	public class AcspHttpModule : IHttpModule
	{
		private static IFileSystem _fileSystemInstance;
		private static IRequestHandler _requestHandler;
		
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
		/// Gets or sets the request handler.
		/// </summary>
		/// <value>
		/// The request handler.
		/// </value>
		/// <exception cref="System.ArgumentNullException"></exception>
		public static IRequestHandler RequestHandler
		{
			get { return _requestHandler; }

			set
			{
				if (value == null)
					throw new ArgumentNullException();

				_requestHandler = value;
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
			var application = (HttpApplication)source;
			var context = application.Context;

			// Exclude processing for file URLs (for css and other files to correctly processed)
			if (!FileSystem.File.Exists(context.Request.PhysicalPath))
				_requestHandler.ProcessRequest(new HttpContextWrapper(context));
		}

		/// <summary>
		/// Initializes the specified application.
		/// </summary>
		/// <param name="application">The application.</param>
		public void Init(HttpApplication application)
		{
			if (_fileSystemInstance == null)
				_fileSystemInstance = new FileSystem();

			if (_requestHandler == null)
				_requestHandler = new RequestHandler();

			application.BeginRequest += ApplicationBeginRequest;
		}

		/// <summary>
		/// Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule" />.
		/// </summary>
		public void Dispose()
		{
		}
	}
}
