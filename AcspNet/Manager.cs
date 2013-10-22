// /////////////////////////////////////////////////////////////
// Advanced Controls Site Platform .NET Engine
// /////////////////////////////////////////////////////////////
// Version 1.2

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.SessionState;

// /////////////////////////////////////////////////////////////

namespace AcspNet
{
	/// <summary>
	/// ACSP .NET Engine Manager class
	/// </summary>
	public sealed class Manager
	{	
		/// <summary>
		/// Current running ACSP .NET engine version
		/// </summary>
		public static string EngineVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
		}

		private const string CookieUserNameFieldName = "AcspUserName";
		private const string CookieUserPasswordFieldName = "AcspUserPassword";
		private const string SessionUserAuthenticationStatusFieldName = "AcspAuthenticationStatus";
		private const string SessionUserIdFieldName = "AcspAunthenticatedUserID";
		private const string IsNewSessionFieldName = "AcspIsNewSession";

		// /////////////////////////////////////////////////////////////

		private static readonly List<Type> LibExtensionsTypes = new List<Type>();
		private static readonly List<Type> ExecExtensionsTypes = new List<Type>();

		private static bool IsMainExtensionsTypesLoaded;
		private static bool IsSpecificExtensionsTypesLoaded;

		private IList _libExtensionsList;
		private ArrayList _execExtensionsList;

		private Dictionary<string, bool> _libExtensionsIsInitializedList;

		private static readonly object Locker = new object();

		private bool _isExtensionsExecutionStopped;

		// /////////////////////////////////////////////////////////////

		private static string SitePhysicalPathContainer = "";
		public static string SitePhysicalPath
		{
			get
			{
				if(SitePhysicalPathContainer == "")
				{
					SitePhysicalPathContainer = HttpContext.Current.Request.PhysicalApplicationPath;

					if(SitePhysicalPathContainer != null)
						SitePhysicalPathContainer = SitePhysicalPathContainer.Replace("\\", "/");
				}

				return SitePhysicalPathContainer;
			}
		}

		private static string SiteUrlContainer = "";
		public static string SiteUrl
		{
			get
			{
				if(SiteUrlContainer == "")
				{
					SiteUrlContainer = string.Format("{0}://{1}{2}/", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, HttpContext.Current.Request.ApplicationPath);
				}

				return SiteUrlContainer;
			}
		}

		/// <summary>
		/// Indicating whether session was created with the current request
		/// </summary>
		public static bool IsNewSession
		{
			get { return HttpContext.Current.Session[IsNewSessionFieldName] == null; }
		}

		public readonly HttpRequest Request = HttpContext.Current.Request;
		public readonly HttpResponse Response = HttpContext.Current.Response;
		public readonly HttpSessionState Session = HttpContext.Current.Session;
		public readonly NameValueCollection QueryString = HttpContext.Current.Request.QueryString;

		// /////////////////////////////////////////////////////////////

		public DateTime StartExecutionTime;
		public DateTime EndExecutionTime;

		/// <summary>
		/// Total engine execution time for current request
		/// </summary>
		public TimeSpan ExecutionTime
		{
			get { return EndExecutionTime.Subtract(StartExecutionTime); }
		}

		// /////////////////////////////////////////////////////////////

		public bool IsAuthenticatedAsUser { get; private set; }
		public bool IsAuthenticatedAsAdministrator { get; set; }

		private int _authenticatedUserID = -1;
		public int AuthenticatedUserID
		{
			get { return _authenticatedUserID; }
		}

		public string AuthenticatedUserName { get; private set; }

		public string UserNameFromCookie
		{
			get
			{
				var cookie = Request.Cookies[CookieUserNameFieldName];

				if(cookie != null)
					return cookie.Value ?? "";

				return null;
			}
		}

		public string UserPasswordFromCookie
		{
			get
			{
				var cookie = Request.Cookies[CookieUserPasswordFieldName];

				if(cookie != null)
					return cookie.Value ?? "";

				return null;
			}
		}

		// /////////////////////////////////////////////////////////////

		private string _currentAction;
		public string CurrentAction
		{
			get
			{
				if(_currentAction == null)
				{
					var action = HttpContext.Current.Request.QueryString["act"];

					_currentAction = action ?? "";
				}

				return _currentAction;
			}
		}

		private string _currentMode;
		public string CurrentMode
		{
			get
			{
				if(_currentMode == null)
				{
					var mode = HttpContext.Current.Request.QueryString["mode"];

					_currentMode = mode ?? "";
				}

				return _currentMode;
			}
		}

		/// <summary>
		/// Initialize ACSP .NET engine instance
		/// </summary>
		/// <param name="types">Specify the types of some classes in libraries where ACSP extensions is placed to load all extension from this libraries</param>
		public Manager(params Type[] types)
		{
			StartExecutionTime = DateTime.Now;

			if(Request == null)
				throw new AcspNetException("HTTP Request doest not exist.");

			lock(Locker)
			{
				if(!IsMainExtensionsTypesLoaded)
					foreach(var classType in types)
					{
						var assemblyTypes = Assembly.GetAssembly(classType).GetTypes();

						foreach(var t in assemblyTypes.Where(t => t.GetInterface("ILibExtension") != null))
							LibExtensionsTypes.Add(t);

						foreach(var t in assemblyTypes.Where(t => t.GetInterface("IExtension") != null))
							ExecExtensionsTypes.Add(t);
					}

				IsMainExtensionsTypesLoaded = true;
			}
		}

		/// <summary>
		/// Load specific extensions into engine (this function should be called before Execute() function)
		/// </summary>
		/// <param name="types">Extension types to load</param>
		public void AddExtensions(params Type[] types)
		{
			lock(Locker)
			{
				if (!IsSpecificExtensionsTypesLoaded)
				{
					foreach (var t in types.Where(t => t.GetInterface("ILibExtension") != null))
					{
						if(!LibExtensionsTypes.Contains(t))
							LibExtensionsTypes.Add(t);
					}

					foreach (var t in types.Where(t => t.GetInterface("IExtension") != null))
					{
						if (!ExecExtensionsTypes.Contains(t))
							ExecExtensionsTypes.Add(t);
					}
				}

				IsSpecificExtensionsTypesLoaded = true;
			}
		}

		// /////////////////////////////////////////////////////////////

		private void CreateLibraryExtensionsInstances()
		{
			_libExtensionsList = new ArrayList(LibExtensionsTypes.Count);
			_libExtensionsIsInitializedList = new Dictionary<string, bool>(LibExtensionsTypes.Count);

			foreach(var type in LibExtensionsTypes)
			{
				_libExtensionsList.Add(Activator.CreateInstance(type));
				_libExtensionsIsInitializedList.Add(type.Name, false);				
			}

			_libExtensionsList = _libExtensionsList.Cast<ILibExtension>().OrderBy(x => x.Priority).ToList();
		}

		private void InitializeLibraryExtensions()
		{
			foreach(ILibExtension extension in _libExtensionsList)
			{
				extension.Initialize(this);
				_libExtensionsIsInitializedList[extension.GetType().Name] = true;
			}
		}

		private void CreateExecutableExtensionsInstances()
		{
			_execExtensionsList = new ArrayList(ExecExtensionsTypes.Count);

			foreach(var extensionType in ExecExtensionsTypes)
			{
				var extension = (IExtension)Activator.CreateInstance(extensionType);

				// Checking execution parameters
				if(extension.ExecParams.Action == "")
				{
					if(extension.ExecParams.RunType != ExtensionRunTypes.MainPage ||
					   extension.ExecParams.RunType == ExtensionRunTypes.MainPage && CurrentAction == "")
						_execExtensionsList.Add(extension);
				}
				else
				{
					if(extension.ExecParams.RunType != ExtensionRunTypes.MainPage &&
					   extension.ExecParams.Action.ToLower() == CurrentAction.ToLower() &&
					   extension.ExecParams.Mode.ToLower() == CurrentMode.ToLower())
						_execExtensionsList.Add(extension);
				}
			}
		}

		// /////////////////////////////////////////////////////////////

		/// <summary>
		/// Run ACSP engine
		/// </summary>
		public ManagerResults Execute()
		{
			CreateLibraryExtensionsInstances();
			InitializeLibraryExtensions();

			CreateExecutableExtensionsInstances();

			var result = RunExecutableExtensions();

			Session.Add(IsNewSessionFieldName, "true");

			return result;
		}

		private ManagerResults RunExecutableExtensions()
		{
			if(_execExtensionsList.Count > 0)
			{
				foreach(var extension in _execExtensionsList.Cast<IExtension>().OrderBy(x => x.ExecParams.Priority))
				{
					if(_isExtensionsExecutionStopped)
						return ManagerResults.ExtensionsExecutionStopped;

					// Extension deny checking

					if(extension.ExecParams.ProtectionType == ExtensionProtectionTypes.Guest && (IsAuthenticatedAsUser || IsAuthenticatedAsAdministrator))
						return ManagerResults.ExtensionDenyErrorGuest;

					if(extension.ExecParams.ProtectionType == ExtensionProtectionTypes.User && !IsAuthenticatedAsUser)
						return ManagerResults.ExtensionDenyErrorUser;

					if(extension.ExecParams.ProtectionType == ExtensionProtectionTypes.Administrator && !(IsAuthenticatedAsUser && IsAuthenticatedAsAdministrator))
						return ManagerResults.ExtensionDenyErrorAdministrator;

					extension.Invoke(this);
				}
			}

			return ManagerResults.ExtensionsExecutionSucceed;
		}

		/// <summary>
		/// Stop ACSP subsequent extensions execution
		/// </summary>
		public void StopExtensionsExecution()
		{
			_isExtensionsExecutionStopped = true;
		}

		/// <summary>
		/// Returns specific library extensions
		/// </summary>
		/// <typeparam name="T">Library extension to return</typeparam>
		/// <returns>Library extension</returns>
		public T GetLibExtension<T>()
			where T : class, ILibExtension
		{
			foreach(var t in _libExtensionsList)
			{
				var currentType = t.GetType();

				if(currentType != typeof(T))
					continue;

				if(_libExtensionsIsInitializedList[currentType.Name] == false)
					throw new AcspNetException("Attempt to call not initialized library extension '" + t.GetType() + "'");

				return t as T;
			}

			return null;
		}

		/// <summary>
		/// Redirects a client to a new URL
		/// </summary>
		public void Redirect(string url)
		{
			StopExtensionsExecution();
			Response.Redirect(url, false);
		}

		/// <summary>
		/// Create user authentication cookies
		/// </summary>
		public void LogInUser(string name, string password, bool autoLogin)
		{
			var cookie = new HttpCookie(CookieUserNameFieldName, name);

			if(autoLogin)
				cookie.Expires = DateTime.Now.AddDays(256);

			Response.Cookies.Add(cookie);

			cookie = new HttpCookie(CookieUserPasswordFieldName, password);

			if(autoLogin)
				cookie.Expires = DateTime.Now.AddDays(256);

			Response.Cookies.Add(cookie);
		}

		/// <summary>
		/// Create user authentication variable in user's session
		/// </summary>
		public void LogInSessionUser(int userID = -1)
		{
			Session.Add(SessionUserAuthenticationStatusFieldName, "authenticated");
			Session.Add(SessionUserIdFieldName, userID);		
		}

		/// <summary>
		/// Checking user cookies with parameters information and setting authentication status if success
		/// </summary>
		public void AuthenticateUser(int userID, string name, string password)
		{
			var userNameCookie = Request.Cookies[CookieUserNameFieldName];
			var userPasswordCookie = Request.Cookies[CookieUserPasswordFieldName];

			if(userNameCookie != null &&
			   userPasswordCookie != null &&
			   userNameCookie.Value == name &&
			   userPasswordCookie.Value == password)
			{
				IsAuthenticatedAsUser = true;
				_authenticatedUserID = userID;
				AuthenticatedUserName = name;
			}
			else
			{
				Request.Cookies.Remove(CookieUserNameFieldName);
				Request.Cookies.Remove(CookieUserPasswordFieldName);
			}
		}

		public void AuthenticateSessionUser()
		{
			if(Session[SessionUserAuthenticationStatusFieldName] == null || (string)Session[SessionUserAuthenticationStatusFieldName] != "authenticated")
				return;

			IsAuthenticatedAsUser = true;
			_authenticatedUserID = (int)Session[SessionUserIdFieldName];
			AuthenticatedUserName = "";
		}

		/// <summary>
		/// Remove user authentication cookies
		/// </summary>
		public void LogOutUser()
		{
			var myCookie = new HttpCookie(CookieUserNameFieldName)
			               	{
			               		Expires = DateTime.Now.AddDays(-1d)
			               	};

			Response.Cookies.Add(myCookie);

			myCookie = new HttpCookie(CookieUserPasswordFieldName)
			           	{
			           		Expires = DateTime.Now.AddDays(-1d)
			           	};

			Response.Cookies.Add(myCookie);

			IsAuthenticatedAsUser = false;
			_authenticatedUserID = -1;
			AuthenticatedUserName = "";
		}

		/// <summary>
		/// Remove user session authentication information
		/// </summary>
		public void LogOutSessionUser()
		{
			Session.Remove(SessionUserAuthenticationStatusFieldName);
			Session.Remove(SessionUserIdFieldName);
		}

		public static IList<Type> GetLibExtensionsTypesList()
		{
			return LibExtensionsTypes.ToArray();
		}

		public static IList<Type> GetExecExtensionsTypesList()
		{
			return ExecExtensionsTypes.ToArray();
		}
	}
}

/////////////////////////////////////////////////