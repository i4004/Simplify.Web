//using HttpRuntime = AcspNet.Web.HttpRuntime;

//namespace AcspNet
//{
//	/// <summary>
//	/// ACSP.NET manager class
//	/// </summary>
//	public sealed class Manager : IManager
//	{
//		/// <summary>
//		/// Site generation time templates variable name
//		/// </summary>
//		public const string SiteVariableNameSiteGenerationTime = "SV:SiteGenerationTime";

//		/// <summary>
//		/// The site variable name templates dir
//		/// </summary>
//		public const string SiteVariableNameTemplatesPath = "SV:TemplatesDir";

//		/// <summary>
//		/// The site variable name current style
//		/// </summary>
//		public const string SiteVariableNameCurrentStyle = "SV:Style";

//		/// <summary>
//		/// The site variable name current language
//		/// </summary>
//		public const string SiteVariableNameCurrentLanguage = "SV:Language";
//		/// <summary>
//		/// The site variable name current language extension
//		/// </summary>
//		public const string SiteVariableNameCurrentLanguageExtension = "SV:LanguageExt";
//		/// <summary>
//		/// The site variable name site URL
//		/// </summary>
//		public const string SiteVariableNameSiteUrl = "SV:SiteUrl";
//		/// <summary>
//		/// The site variable name site virtual path (returns '/yoursite' if your site is placed in virtual directory like http://yourdomain.com/yoursite/)
//		/// </summary>
//		public const string SiteVariableNameSiteVirtualPath = "SV:SiteVirtualPath";

//		private const string IsNewSessionFieldName = "AcspIsNewSession";

//		private static readonly object Locker = new object();

//		/// <summary>
//		/// The file system abstraction, to work with System.IO functions
//		/// </summary>
//		public readonly IFileSystem FileSystem;

//		/// <summary>
//		/// The HttpRuntime abstration, to work with HttpRuntime functions
//		/// </summary>
//		public readonly IHttpRuntime HttpRuntime;

//		/// <summary>
//		/// Various HTML generation classes
//		/// </summary>
//		public readonly HtmlWrapper HtmlWrapper;

//		/// <summary>
//		/// Interface that is used to control users login/logout/autnenticate via cookie or session and stores current user name/password/id unformation
//		/// </summary>
//		public readonly IAuthenticationModule AuthenticationModule;

//		/// <summary>
//		/// Additional extensions
//		/// </summary>
//		public readonly ExtensionsWrapper ExtensionsWrapper;

//		private string _currentAction;
//		private string _currentMode;

//		/// <summary>
//		///Initialize ACSP .NET engine instance
//		/// </summary>
//		public Manager(RouteData routeData)
//			: this(routeData, , new FileSystem(), new HttpRuntime(), Assembly.GetCallingAssembly())
//		{
//		}

//		/// <summary>
//		/// Initialize ACSP .NET engine instance
//		/// </summary>
//		/// <param name="httpContext">The HTTP context.</param>
//		/// <param name="fileSystem">The file system.</param>
//		/// <param name="httpRuntime">The HTTP runtime.</param>
//		/// <param name="userAssembly">The user web-site assembly.</param>
//		/// <exception cref="System.ArgumentNullException">page
//		/// or
//		/// httpContext</exception>
//		public Manager(, IFileSystem fileSystem, IHttpRuntime httpRuntime, Assembly userAssembly)
//		{

//			if (fileSystem == null)
//				throw new ArgumentNullException("fileSystem");

//			if (httpRuntime == null)
//				throw new ArgumentNullException("httpRuntime");

//			if (userAssembly == null)
//				throw new ArgumentNullException("userAssembly");

//			StopWatch = new Stopwatch();
//			StopWatch.Start();

//			if (!IsStaticInitialized)
//			{
//				lock (Locker)
//				{
//					if (!IsStaticInitialized)
//					{

//						if (Request != null && Request.Url != null)
//						{
//							SiteUrl = String.Format("{0}://{1}{2}",
//								Request.Url.Scheme,
//								Request.Url.Authority,
//								Request.ApplicationPath);

//							if (!SiteUrl.EndsWith("/"))
//								SiteUrl += "/";

//							CompleteUrl = String.Format("{0}://{1}{2}",
//								Request.Url.Scheme,
//								Request.Url.Authority,
//								Request.RawUrl);
//						}

//						if (HttpRuntime.AppDomainAppVirtualPath != null)
//							SiteVirtualPath = HttpRuntime.AppDomainAppVirtualPath == "/" ? "" : HttpRuntime.AppDomainAppVirtualPath;

//						CreateMetaContainers(userAssembly);
//						IsStaticInitialized = true;
//					}
//				}
//			}

//			if (Request != null && Request.Url != null)
//				CompleteUrl = String.Format("{0}://{1}{2}",
//					Request.Url.Scheme,
//					Request.Url.Authority,
//					Request.RawUrl);

//			InitializeHtmlWrapper();
//			InitializeExtensionsWrapper();
//		}

//		/// <summary>
//		/// Gets the System.Web.HttpSessionState object for the current HTTP request
//		/// </summary>
//		public HttpSessionStateBase Session { get; private set; }

//		/// <summary>
//		/// Gets the connection of  HTTP query string variables
//		/// </summary>
//		public NameValueCollection QueryString { get; private set; }

//		/// <summary>
//		/// Gets the connection of HTTP post request form variables
//		/// </summary>
//		public NameValueCollection Form { get; private set; }

//		/// <summary>
//		/// The stop watch (for web-page build measurement)
//		/// </summary>
//		public Stopwatch StopWatch { get; private set; }

//		/// <summary>
//		/// Gets the web-site URL, for example: http://yoursite.com/site1/
//		/// </summary>
//		/// <value>
//		/// The site URL.
//		/// </value>
//		public static string SiteUrl { get; private set; }

//		/// <summary>
//		/// Gets the web-site URL, for example: http://yoursite.com/site1/
//		/// </summary>
//		/// <value>
//		/// The site URL.
//		/// </value>
//		public static string SiteVirtualPath { get; private set; }

//		/// <summary>
//		/// Gets current page complete URL, for example: http://yoursite.com/site1/page1/id2
//		/// </summary>
//		/// <value>
//		/// The current page complete URL.
//		/// </value>
//		public string CompleteUrl { get; private set; }

//		/// <summary>
//		/// Indicating whether session was created with the current request
//		/// </summary>
//		public bool IsNewSession
//		{
//			get { return Session[IsNewSessionFieldName] == null; }
//		}

//		/// <summary>
//		/// Gets the current executing extensions types.
//		/// </summary>
//		/// <value>
//		/// The current executing extensions types.
//		/// </value>
//		public IList<Type> ExecExtensionsTypes { get; private set; }

//		/// <summary>
//		/// Gets library extension instance
//		/// </summary>ata
//		/// <typeparam name="T">Library extension instance to get</typeparam>
//		/// <returns>Library extension</returns>
//		public T Get<T>()
//			where T : LibExtension
//		{
//			foreach (var t in _libExtensionsList)
//			{
//				var currentType = t.GetType();

//				if (currentType != typeof(T))
//					continue;

//				if (_libExtensionsIsInitializedList[currentType.Name] == false)
//					throw new AcspNetException("Attempt to call not initialized library extension '" + t.GetType() + "'");

//				return t as T;
//			}

//			throw new AcspNetException("Extension not found: " + typeof(T).FullName);
//		}

//		/// <summary>
//		/// Redirects a client to a new URL
//		/// </summary>
//		public void Redirect(string url)
//		{
//			if (String.IsNullOrEmpty(url))
//				throw new ArgumentNullException("url");

//			StopExtensionsExecution();
//			Response.Redirect(url, false);
//		}

//		private void DisplaySite()
//		{
//			StopWatch.Stop();

//			UpdateNavigatorLinks();
//			SetEnvironmentVariablesToDataCollector();

//			Response.Cache.SetExpires(DateTime.Now);
//			Response.Cache.SetNoStore();
//			DataCollector.DisplaySite();
//		}

//		private void SetEnvironmentVariablesToDataCollector()
//		{
//			DataCollector.Add(SiteVariableNameSiteGenerationTime, StopWatch.Elapsed.ToString("mm\\:ss\\:fff"));
//			DataCollector.Add(SiteVariableNameTemplatesPath, Environment.TemplatesPath);
//			DataCollector.Add(SiteVariableNameCurrentStyle, Environment.SiteStyle);
//			DataCollector.Add(SiteVariableNameCurrentLanguage, Environment.Language);
//			DataCollector.Add(SiteVariableNameCurrentLanguageExtension, Environment.Language != "" ? "." + Environment.Language : "");
//			DataCollector.Add(SiteVariableNameSiteUrl, SiteUrl);
//			DataCollector.Add(SiteVariableNameSiteVirtualPath, SiteVirtualPath);
//		}

//		private void InitializeHtmlWrapper()
//		{
//			HtmlWrapper.ListsGeneratorInstance = new ListsGenerator(this);
//			HtmlWrapper.MessageBoxInstance = new MessageBox(this);
//		}

//		private void InitializeExtensionsWrapper()
//		{
//			ExtensionsWrapper.MessagePageInstance = new MessagePage(this);
//			ExtensionsWrapper.IdProcessorInstance = new IdProcessor(this);
//			ExtensionsWrapper.NavigatorInstance = new Navigator(this);
//		}

//		private void UpdateNavigatorLinks()
//		{
//			ExtensionsWrapper.Navigator.PreviousPageLink = CompleteUrl;
//			ExtensionsWrapper.Navigator.PreviousNavigatedUrl = null;
//		}
//	}
//}