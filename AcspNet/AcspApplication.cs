using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Routing;

namespace AcspNet
{
	/// <summary>
	/// Loads and stores ACSP controllers and views meta information and ACSP settings, creates AcspProcessors for extensions processing
	/// </summary>
	public sealed class AcspApplication : IAcspApplication
	{
		private static IAcspApplication _currentApplication;
		private Assembly _mainAssembly;
		//private IAcspSettings _settings;

		private List<ControllerMetaContainer> _controllersMetaContainers = new List<ControllerMetaContainer>();
		//private List<LibExtensionMetaContainer> _libExtensionsMetaContainers = new List<LibExtensionMetaContainer>();

		//private bool _isSetup;

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
				return _currentApplication ?? (_currentApplication = new AcspApplication());
			}
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				_currentApplication = value;
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

		///// <summary>
		///// Gets or sets the HTTP context.
		///// </summary>
		///// <value>
		///// The HTTP context.
		///// </value>
		///// <exception cref="System.ArgumentNullException">value</exception>
		//public IHttpRuntime HttpRuntime
		//{
		//	get
		//	{
		//		return _httpRuntime ?? (_httpRuntime = new HttpRuntime());
		//	}
		//	set
		//	{
		//		if (value == null)
		//			throw new ArgumentNullException("value");

		//		_httpRuntime = value;
		//	}
		//}

		/// <summary>
		///// Gets or sets the ACSP settings.
		///// </summary>
		///// <value>
		///// The ACSP settings.
		///// </value>
		///// <exception cref="System.ArgumentNullException">value</exception>
		//public IAcspSettings Settings
		//{
		//	get
		//	{
		//		return _settings;
		//	}
		//	set
		//	{
		//		if (value == null)
		//			throw new ArgumentNullException("value");

		//		_settings = value;
		//	}
		//}

		/// <summary>
		/// Setup ACSP application.
		/// </summary>
		public void SetUp()
		{
			if (_mainAssembly == null)
				_mainAssembly = Assembly.GetCallingAssembly();

			//if (_settings == null)
			//	_settings = new AcspSettings();

			CreateControllersMetaContainers(MainAssembly);

			//_isSetup = true;
		}

		///// <summary>
		///// Creates an ACSP extensions processor.
		///// </summary>
		///// <param name="routeData">The route data.</param>
		///// <returns></returns>
		//public IAcspProcessor CreateProcessor(RouteData routeData)
		//{
		//	//if (!_isSetup)
		//	//	throw new AcspException("AcspApplication has not been set up");

		//	//return new AcspProcessor(Settings, new AcspContext(routeData,
		//	//	new HttpContextWrapper(HttpContext.Current)),
		//	//	_execExtensionsMetaContainers, _libExtensionsMetaContainers);
		//	return null;
		//}

		///// <summary>
		///// Get currently loaded executable extensions meta-data
		///// </summary>
		///// <returns></returns>
		//public IList<ExecExtensionMetaContainer> GetExecExtensionsMetaData()
		//{
		//	return _execExtensionsMetaContainers.AsReadOnly();
		//}

		///// <summary>
		///// Gets the library extensions meta data.
		///// </summary>
		///// <returns></returns>
		//public IList<LibExtensionMetaContainer> GetLibExtensionsMetaData()
		//{
		//	return _libExtensionsMetaContainers.AsReadOnly();
		//}

		private void CreateControllersMetaContainers(Assembly callingAssembly)
		{
			var assemblyTypes = callingAssembly.GetTypes();

			var containingClass =
				assemblyTypes.FirstOrDefault(t => t.IsDefined(typeof(LoadClassesFromAssemblyOfAttribute), true)) ??
				assemblyTypes.FirstOrDefault(t => t.IsDefined(typeof(LoadIndividualClassesAttribute), true));

			if (containingClass == null)
				throw new AcspException("LoadClassesFromAssemblyOfAttribute or LoadIndividualClassesAttribute attributes are not found in AcspApplication.MainAssembly");

			var batchExtensionsAttributes = containingClass.GetCustomAttributes(typeof(LoadClassesFromAssemblyOfAttribute), false);
			var individualExtensionsAttributes = containingClass.GetCustomAttributes(typeof(LoadIndividualClassesAttribute), false);

			if (batchExtensionsAttributes.Length <= 1 && individualExtensionsAttributes.Length <= 1)
			{
				if (batchExtensionsAttributes.Length == 1)
					LoadControllersFromAssemblyOf(((LoadClassesFromAssemblyOfAttribute)batchExtensionsAttributes[0]).Types);

				var types = new Type[0];

				if (individualExtensionsAttributes.Length == 1)
					types = ((LoadIndividualClassesAttribute)individualExtensionsAttributes[0]).Types;

				//		//if (!Settings.DisableAcspInternalExtensions)
				//		//	types = types.Concat(new List<Type> { typeof(MessagePageDisplay), typeof(ExtensionsProtector) }).ToArray();

				LoadIndividualControllers(types);

				SortControllersMetaContainers();
			}
			//	else if (batchExtensionsAttributes.Length > 1)
			//		throw new Exception("Multiple LoadExtensionsFromAssemblyOf attributes found");
			//	else if (individualExtensionsAttributes.Length > 1)
			//		throw new Exception("Multiple LoadIndividualExtensions attributes found");
		}

		private void LoadControllersFromAssemblyOf(params Type[] types)
		{
			foreach (var assemblyTypes in types.Select(classType => Assembly.GetAssembly(classType).GetTypes()))
				foreach (var t in assemblyTypes.Where(t => t.BaseType != null && t.BaseType.FullName == "AcspNet.Controller"))
					AddControllerMetaContainer(t);
		}

		private void LoadIndividualControllers(params Type[] types)
		{
			foreach (var t in types.Where(t => t.BaseType != null && t.BaseType.FullName == "AcspNet.ExecExtension").Where(t => _controllersMetaContainers.All(x => x.ControllerType != t)))
				AddControllerMetaContainer(t);
		}

		private void AddControllerMetaContainer(Type controllerType)
		{
			var action = "";
			var mode = "";
			var priority = 0;
			var runOnDefaultPage = false;

			var attributes = controllerType.GetCustomAttributes(typeof(ActionAttribute), false);

			if (attributes.Length > 0)
				action = ((ActionAttribute)attributes[0]).Action;

			attributes = controllerType.GetCustomAttributes(typeof(ModeAttribute), false);

			if (attributes.Length > 0)
				mode = ((ModeAttribute)attributes[0]).Mode;

			attributes = controllerType.GetCustomAttributes(typeof(PriorityAttribute), false);

			if (attributes.Length > 0)
				priority = ((PriorityAttribute)attributes[0]).Priority;

			attributes = controllerType.GetCustomAttributes(typeof(DefaultPageAttribute), false);

			if (attributes.Length > 0)
				runOnDefaultPage = true;

			_controllersMetaContainers.Add(new ControllerMetaContainer(controllerType, action, mode, priority, runOnDefaultPage));
		}

		private void SortControllersMetaContainers()
		{
			_controllersMetaContainers = _controllersMetaContainers.OrderBy(x => x.RunPriority).ToList();
		}
	}
}