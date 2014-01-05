using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Abstractions;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Routing;

using AcspNet.Authentication;
using AcspNet.Extensions;
using AcspNet.Extensions.Executable;
using AcspNet.Html;

namespace AcspNet
{
	/// <summary>
	/// ACSP.NET manager class
	/// </summary>
	public sealed class Manager : IManager
	{
		/// <summary>
		/// Site generation time templates variable name
		/// </summary>
		public const string SiteVariableNameSiteGenerationTime = "SV:SiteGenerationTime";

		/// <summary>
		/// The site variable name templates dir
		/// </summary>
		public const string SiteVariableNameTemplatesPath = "SV:TemplatesDir";

		/// <summary>
		/// The site variable name current style
		/// </summary>
		public const string SiteVariableNameCurrentStyle = "SV:Style";

		/// <summary>
		/// The site variable name current language
		/// </summary>
		public const string SiteVariableNameCurrentLanguage = "SV:Language";
		/// <summary>
		/// The site variable name current language extension
		/// </summary>
		public const string SiteVariableNameCurrentLanguageExtension = "SV:LanguageExt";
		/// <summary>
		/// The site variable name site URL
		/// </summary>
		public const string SiteVariableNameSiteUrl = "SV:SiteUrl";
		/// <summary>
		/// The site variable name site virtual path (returns '/yoursite' if your site is placed in virtual directory like http://yourdomain.com/yoursite/)
		/// </summary>
		public const string SiteVariableNameSiteVirtualPath = "SV:SiteVirtualPath";

		private const string IsNewSessionFieldName = "AcspIsNewSession";

		private static List<ExecExtensionMetaContainer> ExecExtensionsMetaContainers = new List<ExecExtensionMetaContainer>();
		private static List<LibExtensionMetaContainer> LibExtensionsMetaContainers = new List<LibExtensionMetaContainer>();

		private static bool IsStaticInitialized;
		private static readonly object Locker = new object();

		private static readonly Lazy<AcspNetSettings> AcspNetSettingsInstance = new Lazy<AcspNetSettings>(() => new AcspNetSettings());

		private static Lazy<string> SitePhysicalPathInstance;
		private static Lazy<string> SiteUrlInstance;
		private static Lazy<string> SiteVirtualPathInstance;
		
		/// <summary>
		/// The file system instance, to work with System.IO functions
		/// </summary>
		public readonly IFileSystem FileSystem;

		/// <summary>
		/// Current request environment data.
		/// </summary>
		public readonly IEnvironment Environment;

		/// <summary>
		/// Text and XML files loader.
		/// </summary>
		public readonly ExtensionsDataLoader DataLoader;

		/// <summary>
		/// Localizable text items string table.
		/// </summary>
		public readonly StringTable StringTable;

		/// <summary>
		/// Text templates loader.
		/// </summary>
		public readonly ITemplateFactory TemplateFactory;

		/// <summary>
		/// Web-site master page data collector.
		/// </summary>
		public readonly DataCollector DataCollector;

		/// <summary>
		/// Various HTML generation classes
		/// </summary>
		public readonly HtmlWrapper HtmlWrapper;

		/// <summary>
		/// Interface that is used to control users login/logout/autnenticate via cookie or session and stores current user name/password/id unformation
		/// </summary>
		public readonly IAuthenticationModule AuthenticationModule;

		/// <summary>
		/// Additional extensions
		/// </summary>
		public readonly ExtensionsWrapper ExtensionsWrapper;

		private string _currentAction;
		private string _currentMode;
		private string _currentID;

		private bool _isExtensionsExecutionStopped;

		private Dictionary<string, bool> _libExtensionsIsInitializedList;
		private IList<LibExtension> _libExtensionsList;
		private IList<ExecExtension> _execExtensionsList;

		/// <summary>
		///Initialize ACSP .NET engine instance
		/// </summary>
		public Manager(RouteData routeData)
			: this(routeData, new HttpContextWrapper(HttpContext.Current), new FileSystem(), Assembly.GetCallingAssembly())
		{
		}

		/// <summary>
		/// Initialize ACSP .NET engine instance
		/// </summary>
		/// <param name="routeData">The current page route data.</param>
		/// <param name="httpContext">The HTTP context.</param>
		/// <param name="fileSystem">The file system.</param>
		/// <param name="userAssembly">The user web-site assembly.</param>
		/// <exception cref="System.ArgumentNullException">page
		/// or
		/// httpContext</exception>
		public Manager(RouteData routeData, HttpContextBase httpContext, IFileSystem fileSystem, Assembly userAssembly)
		{
			if (routeData == null)
				throw new ArgumentNullException("routeData");

			if (httpContext == null)
				throw new ArgumentNullException("httpContext");

			if (fileSystem == null)
				throw new ArgumentNullException("fileSystem");

			if (userAssembly == null)
				throw new ArgumentNullException("userAssembly");

			StopWatch = new Stopwatch();
			StopWatch.Start();

			RouteData = routeData;
			Context = httpContext;
			FileSystem = fileSystem;
			Request = Context.Request;
			Response = Context.Response;
			Session = Context.Session;
			QueryString = Request.QueryString;
			Form = Request.Form;

			if (!IsStaticInitialized)
			{
				lock (Locker)
				{
					if (!IsStaticInitialized)
					{
						SitePhysicalPathInstance = new Lazy<string>(() => Request.PhysicalApplicationPath != null
							? Request.PhysicalApplicationPath.Replace("\\", "/")
							: null);

						SiteUrlInstance = new Lazy<string>(() =>
						                                   {
							                                   if (Request == null || Request.Url == null)
								                                   return null;

							                                   var url = string.Format("{0}://{1}{2}",
								                                   Request.Url.Scheme,
								                                   Request.Url.Authority,
								                                   Request.ApplicationPath);

							                                   if (!url.EndsWith("/"))
								                                   url += "/";

							                                   return url;
						                                   });

						SiteVirtualPathInstance = new Lazy<string>(() =>
						{
							if (HttpRuntime.AppDomainAppVirtualPath == null)
								return null;

							return HttpRuntime.AppDomainAppVirtualPath == "/" ? "" : HttpRuntime.AppDomainAppVirtualPath;
						});

						CreateMetaContainers(userAssembly);
						IsStaticInitialized = true;
					}
				}
			}

			Environment = new Environment(this);
			DataLoader = new ExtensionsDataLoader(this);
			StringTable = new StringTable(this);
			TemplateFactory = new TemplateFactory(this);
			DataCollector = new DataCollector(this);
			HtmlWrapper = new HtmlWrapper();
			AuthenticationModule = new AuthenticationModule(this);
			ExtensionsWrapper = new ExtensionsWrapper();

			InitializeHtmlWrapper();
			InitializeExtensionsWrapper();
		}
		
		/// <summary>
		///  Gets the <see cref="T:System.Web.HttpContextBase"/> object for the current HTTP request.
		/// </summary>
		public HttpContextBase Context { get; private set; }

		/// <summary>
		/// Gets the System.Web.HttpRequest object for the current HTTP request
		/// </summary>
		public HttpRequestBase Request { get; private set; }

		/// <summary>
		/// Gets the System.Web.HttpResponse object for the current HTTP response
		/// </summary>
		public HttpResponseBase Response { get; private set; }

		/// <summary>
		/// Gets the System.Web.HttpSessionState object for the current HTTP request
		/// </summary>
		public HttpSessionStateBase Session { get; private set; }

		/// <summary>
		/// Gets the connection of  HTTP query string variables
		/// </summary>
		public NameValueCollection QueryString { get; private set; }

		/// <summary>
		/// Gets the connection of HTTP post request form variables
		/// </summary>
		public NameValueCollection Form { get; private set; }

		/// <summary>
		/// Gets the route data.
		/// </summary>
		public RouteData RouteData { get; private set; }
		
		/// <summary>
		/// The stop watch (for web-page build measurement)
		/// </summary>
		public Stopwatch StopWatch { get; private set; }

		/// <summary>
		/// Gets the AcspNet settings.
		/// </summary>
		public static AcspNetSettings AcspNetSettings
		{
			get
			{				
				return AcspNetSettingsInstance.Value;
			}
		}

		/// <summary>
		/// Gets the web-site physical path, for example: C:\inetpub\wwwroot\YourSite
		/// </summary>
		/// <value>
		/// The site physical path.
		/// </value>
		public static string SitePhysicalPath
		{
			get
			{
				return SitePhysicalPathInstance.Value;
			}
		}

		/// <summary>
		/// Gets the web-site URL, for example: http://yoursite.com/site1/
		/// </summary>
		/// <value>
		/// The site URL.
		/// </value>
		public static string SiteUrl
		{
			get
			{
				return SiteUrlInstance.Value;
			}
		}

		/// <summary>
		/// Gets the web-site URL, for example: http://yoursite.com/site1/
		/// </summary>
		/// <value>
		/// The site URL.
		/// </value>
		public static string SiteVirtualPath
		{
			get
			{
				return SiteVirtualPathInstance.Value;
			}
		}

		/// <summary>
		/// Indicating whether session was created with the current request
		/// </summary>
		public bool IsNewSession
		{
			get { return Session[IsNewSessionFieldName] == null; }
		}

		/// <summary>
		/// Gets the current web-site request action parameter (/someAction or ?act=someAction).
		/// </summary>
		/// <value>
		/// The current action (?act=someAction).
		/// </value>
		public string CurrentAction
		{
			get
			{
				if (_currentAction != null) return _currentAction;

				string action;

				if (RouteData != null && RouteData.Values.ContainsKey("action"))
					action = (string)RouteData.Values["action"];
				else
					action = Request.QueryString["act"];

				_currentAction = action ?? "";

				return _currentAction;
			}
		}

		/// <summary>
		/// Gets the current web-site mode request parameter (/someAction/someMode/SomeID or ?act=someAction&amp;mode=somMode).
		/// </summary>
		/// <value>
		/// The current mode (?act=someAction&amp;mode=somMode).
		/// </value>
		public string CurrentMode
		{
			get
			{
				if (_currentMode != null) return _currentMode;

				string mode;

				if (RouteData != null && RouteData.Values.ContainsKey("mode"))
					mode = (string)RouteData.Values["mode"];
				else
					mode = Request.QueryString["mode"];

				_currentMode = mode ?? "";

				return _currentMode;
			}
		}

		/// <summary>
		/// Gets the current web-site ID request parameter (/someAction/someID or ?act=someAction&amp;id=someID).
		/// </summary>
		/// <value>
		/// The current mode (?act=someAction&amp;mode=somMode).
		/// </value>
		public string CurrentID
		{
			get
			{
				if (_currentID != null) return _currentID;

				string id;

				if (RouteData != null && RouteData.Values.ContainsKey("id"))
					id = (string)RouteData.Values["id"];
				else
					id = Request.QueryString["id"];

				_currentID = id ?? "";

				return _currentID;
			}
		}
		
		/// <summary>
		/// Gets the current executing extensions types.
		/// </summary>
		/// <value>
		/// The current executing extensions types.
		/// </value>
		public IList<Type> ExecExtensionsTypes { get; private set; }

		/// <summary>
		/// Stop ACSP subsequent extensions execution
		/// </summary>
		public void StopExtensionsExecution()
		{
			_isExtensionsExecutionStopped = true;
		}

		/// <summary>
		/// Run ACSP engine
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Run()
		{
			CreateLibraryExtensionsInstances();
			InitializeLibraryExtensions();

			CreateExecutableExtensionsInstances();
			RunExecutableExtensions();

			if (Session[IsNewSessionFieldName] == null)
				Session.Add(IsNewSessionFieldName, "true");
		}

		/// <summary>
		/// Gets library extension instance
		/// </summary>
		/// <typeparam name="T">Library extension instance to get</typeparam>
		/// <returns>Library extension</returns>
		public T Get<T>()
			where T : LibExtension
		{
			foreach (var t in _libExtensionsList)
			{
				var currentType = t.GetType();

				if (currentType != typeof(T))
					continue;

				if (_libExtensionsIsInitializedList[currentType.Name] == false)
					throw new AcspNetException("Attempt to call not initialized library extension '" + t.GetType() + "'");

				return t as T;
			}

			throw new AcspNetException("Extension not found: " + typeof(T).FullName);
		}

		/// <summary>
		/// Gets current action/mode URL in formal like ?act={0}&amp;mode={1}&amp;id={2}.
		/// </summary>
		/// <returns></returns>
		public string GetActionModeUrl()
		{
			if (string.IsNullOrEmpty(CurrentAction)) return "";

			var url = "?act=" + CurrentAction;

			if(!string.IsNullOrEmpty(CurrentMode))
				url += "&amp;mode=" + CurrentMode;

			if (!string.IsNullOrEmpty(CurrentID))
				url += "&amp;id=" + CurrentID;

			return url;
		}

		/// <summary>
		/// Redirects a client to a new URL
		/// </summary>
		public void Redirect(string url)
		{
			if(string.IsNullOrEmpty(url))
				throw new ArgumentNullException("url");

			StopExtensionsExecution();
			Response.Redirect(url, false);
		}

		/// <summary>
		/// Get currently loaded executable extensions meta-data
		/// </summary>
		/// <returns></returns>
		public IList<ExecExtensionMetaContainer> GetExecExtensionsMetaData()
		{
			return ExecExtensionsMetaContainers.ToArray();
		}

		/// <summary>
		/// Gets the library extensions meta data.
		/// </summary>
		/// <returns></returns>
		public IList<LibExtensionMetaContainer> GetLibExtensionsMetaData()
		{
			return LibExtensionsMetaContainers.ToArray();
		}

		private void CreateLibraryExtensionsInstances()
		{
			_libExtensionsList = new List<LibExtension>(LibExtensionsMetaContainers.Count);
			_libExtensionsIsInitializedList = new Dictionary<string, bool>(LibExtensionsMetaContainers.Count);

			foreach (var container in LibExtensionsMetaContainers)
			{
				var extension = (LibExtension)Activator.CreateInstance(container.ExtensionType);
				extension.ManagerInstance = this;
				extension.TemplateFactoryInstance = TemplateFactory;
				extension.DataCollectorInstance = DataCollector;
				extension.EnvironmentInstance = Environment;
				extension.ExtensionsDataLoaderInstance = DataLoader;
				extension.StringTableInstance = StringTable;
				extension.HtmlInstance = HtmlWrapper;
				extension.AuthenticationModuleInstance = AuthenticationModule;
				extension.ExtensionsInstance = ExtensionsWrapper;

				_libExtensionsList.Add(extension);
				_libExtensionsIsInitializedList.Add(container.ExtensionType.Name, false);
			}
		}

		private void InitializeLibraryExtensions()
		{
			foreach (var extension in _libExtensionsList)
			{
				extension.Initialize();
				_libExtensionsIsInitializedList[extension.GetType().Name] = true;
			}
		}

		private void CreateExecutableExtensionsInstances()
		{
			_execExtensionsList = new List<ExecExtension>(ExecExtensionsMetaContainers.Count);
			ExecExtensionsTypes = new List<Type>(ExecExtensionsMetaContainers.Count);

			foreach (var container in ExecExtensionsMetaContainers)
			{
				if ((CurrentAction == "" && CurrentMode == "" && container.RunType == RunType.MainPage) ||
					(String.Equals(container.Action, CurrentAction, StringComparison.CurrentCultureIgnoreCase) &&
					 String.Equals(container.Mode, CurrentMode, StringComparison.CurrentCultureIgnoreCase)) ||
					(container.Action == "" && container.RunType == RunType.OnAction))
				{
					var extension = (ExecExtension)Activator.CreateInstance(container.ExtensionType);
					extension.ManagerInstance = this;
					extension.TemplateFactoryInstance = TemplateFactory;
					extension.DataCollectorInstance = DataCollector;
					extension.EnvironmentInstance = Environment;
					extension.ExtensionsDataLoaderInstance = DataLoader;
					extension.StringTableInstance = StringTable;
					extension.HtmlInstance = HtmlWrapper;
					extension.AuthenticationModuleInstance = AuthenticationModule;
					extension.ExtensionsInstance = ExtensionsWrapper;

					_execExtensionsList.Add(extension);
					ExecExtensionsTypes.Add(extension.GetType());
				}
			}
		}

		private void RunExecutableExtensions()
		{
			foreach (var extension in _execExtensionsList)
			{
				if (_isExtensionsExecutionStopped)
					return;

				extension.Invoke();
			}

			DisplaySite();
		}
		
		private static void CreateMetaContainers(Assembly callingAssembly)
		{
			var assemblyTypes = callingAssembly.GetTypes();

			var containingClass =
				assemblyTypes.FirstOrDefault(t => t.IsDefined(typeof(LoadExtensionsFromAssemblyOfAttribute), true)) ??
				assemblyTypes.FirstOrDefault(t => t.IsDefined(typeof(LoadIndividualExtensionsAttribute), true));

			if (containingClass == null)
				throw new AcspNetException("LoadExtensionsFromAssemblyOf attribute not found in your classes");

			var batchExtensionsAttributes = containingClass.GetCustomAttributes(typeof(LoadExtensionsFromAssemblyOfAttribute), false);
			var individualExtensionsAttributes = containingClass.GetCustomAttributes(typeof(LoadIndividualExtensionsAttribute), false);

			if (batchExtensionsAttributes.Length <= 1 && individualExtensionsAttributes.Length <= 1)
			{
				if (batchExtensionsAttributes.Length == 1)
					LoadExtensionsFromAssemblyOf(((LoadExtensionsFromAssemblyOfAttribute)batchExtensionsAttributes[0]).Types);

				var types = new Type[0];

				if (individualExtensionsAttributes.Length == 1)
					types = ((LoadIndividualExtensionsAttribute)individualExtensionsAttributes[0]).Types;

				if (!AcspNetSettingsInstance.Value.DisableAcspInternalExtensions)
					types = types.Concat(new List<Type> { typeof(MessagePageDisplay), typeof(ExtensionsProtector) }).ToArray();

				LoadIndividualExtensions(types);

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
				foreach (var t in assemblyTypes.Where(t => t.BaseType != null && t.BaseType.FullName == "AcspNet.LibExtension"))
					AddLibExtensionMetaContainer(t);

				foreach (var t in assemblyTypes.Where(t => t.BaseType != null && t.BaseType.FullName == "AcspNet.ExecExtension"))
					AddExecExtensionMetaContainer(t);
			}
		}

		private static void LoadIndividualExtensions(params Type[] types)
		{
			foreach (var t in types.Where(t => t.BaseType != null && t.BaseType.FullName == "AcspNet.LibExtension").Where(t => LibExtensionsMetaContainers.All(x => x.ExtensionType != t)))
				AddLibExtensionMetaContainer(t);

			foreach (var t in types.Where(t => t.BaseType != null && t.BaseType.FullName == "AcspNet.ExecExtension").Where(t => ExecExtensionsMetaContainers.All(x => x.ExtensionType != t)))
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

			var attributes = extensionType.GetCustomAttributes(typeof(ActionAttribute), false);

			if (attributes.Length > 0)
				action = ((ActionAttribute)attributes[0]).Action;

			attributes = extensionType.GetCustomAttributes(typeof(ModeAttribute), false);

			if (attributes.Length > 0)
				mode = ((ModeAttribute)attributes[0]).Mode;

			attributes = extensionType.GetCustomAttributes(typeof(RunTypeAttribute), false);

			if (attributes.Length > 0)
				runType = ((RunTypeAttribute)attributes[0]).RunType;

			ExecExtensionsMetaContainers.Add(new ExecExtensionMetaContainer(CreateExtensionMetaContainer(extensionType), action, mode, runType));
		}

		private static ExtensionMetaContainer CreateExtensionMetaContainer(Type extensionType)
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

		private static void SortLibraryExtensionsMetaContainers()
		{
			LibExtensionsMetaContainers = LibExtensionsMetaContainers.OrderBy(x => x.Priority).ToList();
		}

		private static void SortExecExtensionsMetaContainers()
		{
			ExecExtensionsMetaContainers = ExecExtensionsMetaContainers.OrderBy(x => x.Priority).ToList();
		}

		private void DisplaySite()
		{
			StopWatch.Stop();

			SetEnvironmentVariablesToDataCollector();

			Response.Cache.SetExpires(DateTime.Now);
			Response.Cache.SetNoStore();
			DataCollector.DisplaySite();
		}

		private void SetEnvironmentVariablesToDataCollector()
		{
			DataCollector.Add(SiteVariableNameSiteGenerationTime, StopWatch.Elapsed.ToString("mm\\:ss\\:fff"));
			DataCollector.Add(SiteVariableNameTemplatesPath, Environment.TemplatesPath);
			DataCollector.Add(SiteVariableNameCurrentStyle, Environment.SiteStyle);
			DataCollector.Add(SiteVariableNameCurrentLanguage, Environment.Language);
			DataCollector.Add(SiteVariableNameCurrentLanguageExtension, Environment.Language != "" ? "." + Environment.Language : "");
			DataCollector.Add(SiteVariableNameSiteUrl, SiteUrl);
			DataCollector.Add(SiteVariableNameSiteVirtualPath, SiteVirtualPath);
		}

		private void InitializeHtmlWrapper()
		{
			HtmlWrapper.ListsGeneratorInstance = new ListsGenerator(this);
			HtmlWrapper.MessageBoxInstance = new MessageBox(this);
		}

		private void InitializeExtensionsWrapper()
		{
			ExtensionsWrapper.MessagePageInstance = new MessagePage(this);
			ExtensionsWrapper.IdProcessorInstance = new IdProcessor(this);
		}
	}
}