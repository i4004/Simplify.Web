using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.SessionState;

namespace AcspNet
{
	/// <summary>
	/// ACSP.NET main class
	/// </summary>
	public sealed class Manager
	{
		private const string IsNewSessionFieldName = "AcspIsNewSession";

		private static List<ExecExtensionMetaContainer> ExecExtensionsMetaContainers = new List<ExecExtensionMetaContainer>();
		private static List<LibExtensionMetaContainer> LibExtensionsMetaContainers = new List<LibExtensionMetaContainer>();

		private static bool IsExtensionsTypesLoaded;

		private static readonly object Locker = new object();

		private static string SitePhysicalPathContainer = "";

		private static string SiteUrlContainer = "";

		/// <summary>
		///     Gets the connection of  HTTP query string variables
		/// </summary>
		public readonly NameValueCollection QueryString = HttpContext.Current.Request.QueryString;

		/// <summary>
		///     Gets the System.Web.HttpRequest object for the current HTTP request
		/// </summary>
		public readonly HttpRequest Request = HttpContext.Current.Request;

		/// <summary>
		///     Gets the System.Web.HttpResponse object for the current HTTP response
		/// </summary>
		public readonly HttpResponse Response = HttpContext.Current.Response;

		/// <summary>
		///     Gets the System.Web.HttpSessionState object for the current HTTP request
		/// </summary>
		public readonly HttpSessionState Session = HttpContext.Current.Session;

		/// <summary>
		///     Gets the connection of HTTP post request form variables
		/// </summary>
		public readonly NameValueCollection Form = HttpContext.Current.Request.Form;

		/// <summary>
		/// The stop watch (for web-page build measurement)
		/// </summary>
		public readonly Stopwatch StopWatch;

		private string _currentAction;
		private string _currentMode;

		private IList<IExecExtension> _execExtensionsList;
		private bool _isExtensionsExecutionStopped;

		private Dictionary<string, bool> _libExtensionsIsInitializedList;
		private IList<ILibExtension> _libExtensionsList;

		/// <summary>
		///     Initialize ACSP .NET engine instance
		/// </summary>
		public Manager()
		{
			if (Request == null)
				throw new AcspNetException("HTTP Request doest not exist.");

			StopWatch = new Stopwatch();
			StopWatch.Start();

			if (IsExtensionsTypesLoaded)
				return;

			lock (Locker)
			{
				if (IsExtensionsTypesLoaded) return;

				CreateMetaContainers(Assembly.GetCallingAssembly());
				IsExtensionsTypesLoaded = true;
			}
		}

		/// <summary>
		///     Gets the web-site physical path, for example: C:\inetpub\wwwroot\YourSite
		/// </summary>
		/// <value>
		///     The site physical path.
		/// </value>
		public static string SitePhysicalPath
		{
			get
			{
				if (SitePhysicalPathContainer != "") return SitePhysicalPathContainer;

				SitePhysicalPathContainer = HttpContext.Current.Request.PhysicalApplicationPath;

				if (SitePhysicalPathContainer != null)
					SitePhysicalPathContainer = SitePhysicalPathContainer.Replace("\\", "/");

				return SitePhysicalPathContainer;
			}
		}

		/// <summary>
		///     Gets the web-site URL, for example: http://yoursite.com/site1/
		/// </summary>
		/// <value>
		///     The site URL.
		/// </value>
		public static string SiteUrl
		{
			get
			{
				if (SiteUrlContainer == "")
				{
					SiteUrlContainer = string.Format("{0}://{1}{2}/", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority,
						HttpContext.Current.Request.ApplicationPath);
				}

				return SiteUrlContainer;
			}
		}

		/// <summary>
		///     Indicating whether session was created with the current request
		/// </summary>
		public static bool IsNewSession
		{
			get { return HttpContext.Current.Session[IsNewSessionFieldName] == null; }
		}

		/// <summary>
		///     Gets the current web-site action (?act=someAction).
		/// </summary>
		/// <value>
		///     The current action (?act=someAction).
		/// </value>
		public string CurrentAction
		{
			get
			{
				if (_currentAction != null) return _currentAction;

				var action = HttpContext.Current.Request.QueryString["act"];

				_currentAction = action ?? "";

				return _currentAction;
			}
		}

		/// <summary>
		///     Gets the current web-site mode (?act=someAction&amp;mode=somMode).
		/// </summary>
		/// <value>
		///     The current mode (?act=someAction&amp;mode=somMode).
		/// </value>
		public string CurrentMode
		{
			get
			{
				if (_currentMode != null) return _currentMode;

				var mode = HttpContext.Current.Request.QueryString["mode"];

				_currentMode = mode ?? "";

				return _currentMode;
			}
		}

		/// <summary>
		/// Gets the current executing extensions types.
		/// </summary>
		/// <value>
		/// The current executing extensions types.
		/// </value>
		public IList<Type> ExecExtensionsTypes { get; private set; }

		private static void CreateMetaContainers(Assembly callingAssembly)
		{
			var assemblyTypes = callingAssembly.GetTypes();

			var containingClass = assemblyTypes.FirstOrDefault(t => t.IsDefined(typeof (LoadExtensionsFromAssemblyOfAttribute), true)) ??
			                      assemblyTypes.FirstOrDefault(t => t.IsDefined(typeof (LoadIndividualExtensionsAttribute), true));

			if (containingClass == null)
				throw new AcspNetException("LoadExtensionsFromAssemblyOf attribute not found in your class");

			var batchExtensionsAttributes = containingClass.GetCustomAttributes(typeof (LoadExtensionsFromAssemblyOfAttribute), false);
			var individualExtensionsAttributes = containingClass.GetCustomAttributes(typeof (LoadIndividualExtensionsAttribute), false);

			if (batchExtensionsAttributes.Length <= 1 && individualExtensionsAttributes.Length <= 1)
			{
				if (batchExtensionsAttributes.Length == 1)
					LoadExtensionsFromAssemblyOf(((LoadExtensionsFromAssemblyOfAttribute) batchExtensionsAttributes[0]).Types);

				if (individualExtensionsAttributes.Length == 1)
					LoadIndividualExtensions(((LoadIndividualExtensionsAttribute)individualExtensionsAttributes[0]).Types);

				SortLibraryExtensionsMetaContainers();
				SortExecExtensionsMetaContainers();
			}
			else if (batchExtensionsAttributes.Length > 1)
				throw new Exception("Multiple LoadExtensionsFromAssemblyOf attributes found");
			else if (individualExtensionsAttributes.Length > 1)
				throw new Exception("Multiple LoadIndividualExtensions attributes found");
		}

		private static void LoadExtensionsFromAssemblyOf(params Type[] types)
		{
			foreach (var assemblyTypes in types.Select(classType => Assembly.GetAssembly(classType).GetTypes()))
			{
				foreach (var t in assemblyTypes.Where(t => t.GetInterface("ILibExtension") != null))
					AddLibExtensionMetaContainer(t);

				foreach (var t in assemblyTypes.Where(t => t.GetInterface("IExecExtension") != null))
					AddExecExtensionMetaContainer(t);
			}
		}

		private static void LoadIndividualExtensions(params Type[] types)
		{
			foreach (var t in types.Where(t => t.GetInterface("ILibExtension") != null).Where(t => LibExtensionsMetaContainers.All(x => x.ExtensionType != t)))
				AddLibExtensionMetaContainer(t);

			foreach (var t in types.Where(t => t.GetInterface("IExecExtension") != null).Where(t => ExecExtensionsMetaContainers.All(x => x.ExtensionType != t)))
				AddExecExtensionMetaContainer(t);
		}

		private static void AddLibExtensionMetaContainer(Type extensionType)
		{
			LibExtensionsMetaContainers.Add(new LibExtensionMetaContainer(CreateExtensionMetaContainer(extensionType)));
		}

		private static void AddExecExtensionMetaContainer(Type extensionType)
		{
			var action = "";
			var mode = "";
			var runType = RunType.OnAction;

			var attributes = extensionType.GetCustomAttributes(typeof (ActionAttribute), false);

			if (attributes.Length > 0)
				action = ((ActionAttribute) attributes[0]).Action;

			attributes = extensionType.GetCustomAttributes(typeof (ModeAttribute), false);

			if (attributes.Length > 0)
				mode = ((ModeAttribute) attributes[0]).Mode;

			attributes = extensionType.GetCustomAttributes(typeof (RunTypeAttribute), false);

			if (attributes.Length > 0)
				runType = ((RunTypeAttribute) attributes[0]).RunType;

			ExecExtensionsMetaContainers.Add(new ExecExtensionMetaContainer(CreateExtensionMetaContainer(extensionType), action, mode, runType));
		}

		private static ExtensionMetaContainer CreateExtensionMetaContainer(Type extensionType)
		{
			var priority = 0;
			var version = "";

			var attributes = extensionType.GetCustomAttributes(typeof (PriorityAttribute), false);

			if (attributes.Length > 0)
				priority = ((PriorityAttribute) attributes[0]).Priority;

			attributes = extensionType.GetCustomAttributes(typeof (VersionAttribute), false);

			if (attributes.Length > 0)
				version = ((VersionAttribute) attributes[0]).Version;

			return new ExtensionMetaContainer(extensionType, priority, version);
		}

		private static void SortLibraryExtensionsMetaContainers()
		{
			LibExtensionsMetaContainers = LibExtensionsMetaContainers.OrderBy(x => x.Priority).ToList();
		}

		private static void SortExecExtensionsMetaContainers()
		{
			ExecExtensionsMetaContainers = ExecExtensionsMetaContainers.OrderBy(x => x.Priority).ToList();
		}

		/// <summary>
		///     Run ACSP engine
		/// </summary>
		public void Run()
		{
			CreateLibraryExtensionsInstances();
			InitializeLibraryExtensions();

			CreateExecutableExtensionsInstances();
			RunExecutableExtensions();

			Session.Add(IsNewSessionFieldName, "true");
		}

		private void CreateLibraryExtensionsInstances()
		{
			_libExtensionsList = new List<ILibExtension>(LibExtensionsMetaContainers.Count);
			_libExtensionsIsInitializedList = new Dictionary<string, bool>(LibExtensionsMetaContainers.Count);

			foreach (var container in LibExtensionsMetaContainers)
			{
				_libExtensionsList.Add((ILibExtension) Activator.CreateInstance(container.ExtensionType));
				_libExtensionsIsInitializedList.Add(container.ExtensionType.Name, false);
			}
		}

		private void InitializeLibraryExtensions()
		{
			foreach (var extension in _libExtensionsList)
			{
				extension.Initialize(this);
				_libExtensionsIsInitializedList[extension.GetType().Name] = true;
			}
		}

		private void CreateExecutableExtensionsInstances()
		{
			_execExtensionsList = new List<IExecExtension>(ExecExtensionsMetaContainers.Count);
			ExecExtensionsTypes = new List<Type>(ExecExtensionsMetaContainers.Count);

			foreach (var container in ExecExtensionsMetaContainers)
			{
				var extension = (IExecExtension) Activator.CreateInstance(container.ExtensionType);

				// Checking execution parameters
				if (container.Action == "")
				{
					if (container.RunType == RunType.MainPage && (container.RunType != RunType.MainPage || CurrentAction != ""))
						continue;

					_execExtensionsList.Add(extension);
					ExecExtensionsTypes.Add(extension.GetType());
				}
				else
				{
					if (container.RunType == RunType.MainPage ||
					    !String.Equals(container.Action, CurrentAction, StringComparison.CurrentCultureIgnoreCase)
					    || !String.Equals(container.Mode, CurrentMode, StringComparison.CurrentCultureIgnoreCase))
						continue;

					_execExtensionsList.Add(extension);
					ExecExtensionsTypes.Add(extension.GetType());
				}
			}
		}

		private void RunExecutableExtensions()
		{
			if (_execExtensionsList.Count <= 0) return;

			foreach (var extension in _execExtensionsList)
			{
				if (_isExtensionsExecutionStopped)
					return;

				extension.Invoke(this);
			}
		}

		/// <summary>
		///     Stop ACSP subsequent extensions execution
		/// </summary>
		public void StopExtensionsExecution()
		{
			_isExtensionsExecutionStopped = true;
		}

		/// <summary>
		///     Gets library extension instance
		/// </summary>
		/// <typeparam name="T">Library extension instance to get</typeparam>
		/// <returns>Library extension</returns>
		public T Get<T>()
			where T : class, ILibExtension
		{
			foreach (var t in _libExtensionsList)
			{
				var currentType = t.GetType();

				if (currentType != typeof (T))
					continue;

				if (_libExtensionsIsInitializedList[currentType.Name] == false)
					throw new AcspNetException("Attempt to call not initialized library extension '" + t.GetType() + "'");

				return t as T;
			}

			throw new AcspNetException("Extension not found: " + typeof(T).FullName);
		}

		/// <summary>
		/// Gets current action/mode URL in formal like ?act={0}&amp;mode={1}.
		/// </summary>
		/// <returns></returns>
		public string GetActionModeUrl()
		{
			return string.Format("?act={0}&amp;mode={1}", CurrentAction, CurrentMode);
		}

		/// <summary>
		///     Redirects a client to a new URL
		/// </summary>
		public void Redirect(string url)
		{
			StopExtensionsExecution();
			Response.Redirect(url, false);
		}

		/// <summary>
		/// Get currently loaded executable extensions meta-data
		/// </summary>
		/// <returns></returns>
		public static IList<ExecExtensionMetaContainer> GetExecExtensionsMetaData()
		{
			return ExecExtensionsMetaContainers.ToArray();
		}

		/// <summary>
		/// Gets the library extensions meta data.
		/// </summary>
		/// <returns></returns>
		public static IList<LibExtensionMetaContainer> GetLibExtensionsMetaData()
		{
			return LibExtensionsMetaContainers.ToArray();
		}
	}
}