using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Routing;
using AcspNet.Meta;

namespace AcspNet
{
	/// <summary>
	/// Loads and stores ACSP extensions meta information and ACSP settings, creates AcspProcessors for extensions processing
	/// </summary>
	public sealed class AcspApplication : IAcspApplication
	{
		private static IAcspApplication CurrentApplication;
		private Assembly _mainAssembly;
		private HttpContextBase _httpContext;
		private IAcspSettings _settings;

		private List<ExecExtensionMetaContainer> _execExtensionsMetaContainers = new List<ExecExtensionMetaContainer>();
		private List<LibExtensionMetaContainer> _libExtensionsMetaContainers = new List<LibExtensionMetaContainer>();

		private bool _isSetup;

		/// <summary>
		/// Gets or sets the current ACSP application instance.
		/// </summary>
		/// <value>
		/// The current.
		/// </value>
		/// <exception cref="System.ArgumentNullException">value</exception>
		public static IAcspApplication Current
		{
			get
			{
				return CurrentApplication ?? (CurrentApplication = new AcspApplication());
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				CurrentApplication = value;
			}
		}

		/// <summary>
		/// Gets or sets the assembly which contains LoadExtensionsFromAssemblyOf or LoadIndividualExtensions attributes.
		/// </summary>
		/// <exception cref="System.ArgumentNullException">value</exception>
		public Assembly MainAssembly
		{
			get
			{
				return _mainAssembly;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_mainAssembly = value;
			}
		}

		/// <summary>
		/// Gets or sets the HTTP context.
		/// </summary>
		/// <value>
		/// The HTTP context.
		/// </value>
		/// <exception cref="System.ArgumentNullException">value</exception>
		public HttpContextBase HttpContext
		{
			get
			{
				return _httpContext ?? (_httpContext = new HttpContextWrapper(System.Web.HttpContext.Current));
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_httpContext = value;
			}
		}

		/// <summary>
		/// Gets or sets the ACSP settings.
		/// </summary>
		/// <value>
		/// The ACSP settings.
		/// </value>
		/// <exception cref="System.ArgumentNullException">value</exception>
		public IAcspSettings Settings
		{
			get
			{
				return _settings;
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_settings = value;
			}
		}
		
		/// <summary>
		/// Setup ACSP application.
		/// </summary>
		public void SetUp()
		{
			if (_mainAssembly == null)
				_mainAssembly = Assembly.GetCallingAssembly();

			if (_settings == null)
				_settings = new AcspSettings();

			CreateMetaContainers(MainAssembly);

			_isSetup = true;
		}

		/// <summary>
		/// Creates an ACSP extensions processor.
		/// </summary>
		/// <param name="routeData">The route data.</param>
		/// <returns></returns>
		public IAcspProcessor CreateProcessor(RouteData routeData)
		{
			if (!_isSetup)
				throw new AcspException("AcspApplication has not been set up");

			return new AcspProcessor(Settings, new AcspContext(routeData, HttpContext), _execExtensionsMetaContainers, _libExtensionsMetaContainers);
		}

		/// <summary>
		/// Get currently loaded executable extensions meta-data
		/// </summary>
		/// <returns></returns>
		public IList<ExecExtensionMetaContainer> GetExecExtensionsMetaData()
		{
			return _execExtensionsMetaContainers.AsReadOnly();
		}

		/// <summary>
		/// Gets the library extensions meta data.
		/// </summary>
		/// <returns></returns>
		public IList<LibExtensionMetaContainer> GetLibExtensionsMetaData()
		{
			return _libExtensionsMetaContainers.AsReadOnly();
		}

		private void CreateMetaContainers(Assembly callingAssembly)
		{
			var assemblyTypes = callingAssembly.GetTypes();

			var containingClass =
				assemblyTypes.FirstOrDefault(t => t.IsDefined(typeof(LoadExtensionsFromAssemblyOfAttribute), true)) ??
				assemblyTypes.FirstOrDefault(t => t.IsDefined(typeof(LoadIndividualExtensionsAttribute), true));

			if (containingClass == null)
				throw new AcspException("LoadExtensionsFromAssemblyOf or LoadIndividualExtensionsAttribute attributes are not found in AcspApplication.MainAssembly");

			var batchExtensionsAttributes = containingClass.GetCustomAttributes(typeof(LoadExtensionsFromAssemblyOfAttribute), false);
			var individualExtensionsAttributes = containingClass.GetCustomAttributes(typeof(LoadIndividualExtensionsAttribute), false);

			if (batchExtensionsAttributes.Length <= 1 && individualExtensionsAttributes.Length <= 1)
			{
				if (batchExtensionsAttributes.Length == 1)
					LoadExtensionsFromAssemblyOf(((LoadExtensionsFromAssemblyOfAttribute)batchExtensionsAttributes[0]).Types);

				var types = new Type[0];

				if (individualExtensionsAttributes.Length == 1)
					types = ((LoadIndividualExtensionsAttribute)individualExtensionsAttributes[0]).Types;

				//if (!Settings.DisableAcspInternalExtensions)
				//	types = types.Concat(new List<Type> { typeof(MessagePageDisplay), typeof(ExtensionsProtector) }).ToArray();

				LoadIndividualExtensions(types);

				SortLibraryExtensionsMetaContainers();
				SortExecExtensionsMetaContainers();
			}
			else if (batchExtensionsAttributes.Length > 1)
				throw new Exception("Multiple LoadExtensionsFromAssemblyOf attributes found");
			else if (individualExtensionsAttributes.Length > 1)
				throw new Exception("Multiple LoadIndividualExtensions attributes found");
		}

		private void LoadExtensionsFromAssemblyOf(params Type[] types)
		{
			foreach (var assemblyTypes in types.Select(classType => Assembly.GetAssembly(classType).GetTypes()))
			{
				foreach (var t in assemblyTypes.Where(t => t.BaseType != null && t.BaseType.FullName == "AcspNet.LibExtension"))
					AddLibExtensionMetaContainer(t);

				foreach (var t in assemblyTypes.Where(t => t.BaseType != null && t.BaseType.FullName == "AcspNet.ExecExtension"))
					AddExecExtensionMetaContainer(t);
			}
		}

		private void LoadIndividualExtensions(params Type[] types)
		{
			foreach (var t in types.Where(t => t.BaseType != null && t.BaseType.FullName == "AcspNet.LibExtension").Where(t => _libExtensionsMetaContainers.All(x => x.ExtensionType != t)))
				AddLibExtensionMetaContainer(t);

			foreach (var t in types.Where(t => t.BaseType != null && t.BaseType.FullName == "AcspNet.ExecExtension").Where(t => _execExtensionsMetaContainers.All(x => x.ExtensionType != t)))
				AddExecExtensionMetaContainer(t);
		}

		private void AddLibExtensionMetaContainer(Type extensionType)
		{
			_libExtensionsMetaContainers.Add(new LibExtensionMetaContainer(CreateExtensionMetaContainer(extensionType)));
		}

		private ExtensionMetaContainer CreateExtensionMetaContainer(Type extensionType)
		{
			var priority = 0;
			var version = "";

			var attributes = extensionType.GetCustomAttributes(typeof(PriorityAttribute), false);

			if (attributes.Length > 0)
				priority = ((PriorityAttribute)attributes[0]).Priority;

			attributes = extensionType.GetCustomAttributes(typeof(VersionAttribute), false);

			if (attributes.Length > 0)
				version = ((VersionAttribute)attributes[0]).Version;

			return new ExtensionMetaContainer(extensionType, priority, version);
		}

		private void AddExecExtensionMetaContainer(Type extensionType)
		{
			var action = "";
			var mode = "";
			var runType = RunType.OnAction;

			var attributes = extensionType.GetCustomAttributes(typeof(ActionAttribute), false);

			if (attributes.Length > 0)
				action = ((ActionAttribute)attributes[0]).Action;

			attributes = extensionType.GetCustomAttributes(typeof(ModeAttribute), false);

			if (attributes.Length > 0)
				mode = ((ModeAttribute)attributes[0]).Mode;

			attributes = extensionType.GetCustomAttributes(typeof(RunTypeAttribute), false);

			if (attributes.Length > 0)
				runType = ((RunTypeAttribute)attributes[0]).RunType;

			_execExtensionsMetaContainers.Add(new ExecExtensionMetaContainer(CreateExtensionMetaContainer(extensionType), action, mode, runType));
		}

		private void SortLibraryExtensionsMetaContainers()
		{
			_libExtensionsMetaContainers = _libExtensionsMetaContainers.OrderBy(x => x.Priority).ToList();
		}

		private void SortExecExtensionsMetaContainers()
		{
			_execExtensionsMetaContainers = _execExtensionsMetaContainers.OrderBy(x => x.Priority).ToList();
		}
	}
}