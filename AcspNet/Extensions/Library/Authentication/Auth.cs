using System;
using System.Web;

namespace AcspNet.Extensions.Library.Authentication
{
	/// <summary>
	/// Authentication class
	/// </summary>
	[Priority(-10)]
	[Version("1.0")]
	public class Auth : ILibExtension
	{
		private const string CookieUserNameFieldName = "AcspUserName";
		private const string CookieUserPasswordFieldName = "AcspUserPassword";
		private const string SessionUserAuthenticationStatusFieldName = "AcspAuthenticationStatus";
		private const string SessionUserIdFieldName = "AcspAunthenticatedUserID";

		private int _authenticatedUserID = -1;
		private Manager _manager;

		/// <summary>
		/// Initializes the library extension.
		/// </summary>
		/// <param name="manager">The manager.</param>
		public void Initialize(Manager manager)
		{
			_manager = manager;
		}

		/// <summary>
		///     Gets the authenticated user name from cookie.
		/// </summary>
		/// <value>
		///     The authenticated user name from cookie.
		/// </value>
		public string UserNameFromCookie
		{
			get
			{
				var cookie = _manager.Request.Cookies[CookieUserNameFieldName];

				if (cookie != null)
					return cookie.Value ?? "";

				return null;
			}
		}

		/// <summary>
		///     Gets the authenticated user password from cookie.
		/// </summary>
		/// <value>
		///     The authenticated user password from cookie.
		/// </value>
		public string UserPasswordFromCookie
		{
			get
			{
				var cookie = _manager.Request.Cookies[CookieUserPasswordFieldName];

				if (cookie != null)
					return cookie.Value ?? "";

				return null;
			}
		}

		/// <summary>
		///     Gets a value indicating whether current web-site client is authenticated as user.
		/// </summary>
		/// <value>
		///     <c>true</c> if current web-site client is authenticated as user; otherwise, <c>false</c>.
		/// </value>
		public bool IsAuthenticatedAsUser { get; private set; }

		/// <summary>
		///     Gets the authenticated user identifier.
		/// </summary>
		/// <value>
		///     The authenticated user identifier.
		/// </value>
		public int AuthenticatedUserID
		{
			get { return _authenticatedUserID; }
		}

		/// <summary>
		///     Gets the name of the authenticated user.
		/// </summary>
		/// <value>
		///     The name of the authenticated user.
		/// </value>
		public string AuthenticatedUserName { get; private set; }

		/// <summary>
		/// Create user authentication cookies (login user via cookies)
		/// </summary>
		public void LogInUser(string name, string password, bool autoLogin)
		{
			var cookie = new HttpCookie(CookieUserNameFieldName, name);

			if (autoLogin)
				cookie.Expires = DateTime.Now.AddDays(256);

			_manager.Response.Cookies.Add(cookie);

			cookie = new HttpCookie(CookieUserPasswordFieldName, password);

			if (autoLogin)
				cookie.Expires = DateTime.Now.AddDays(256);

			_manager.Response.Cookies.Add(cookie);
		}

		/// <summary>
		/// Create user authentication variable in user's session (login user via session)
		/// </summary>
		public void LogInSessionUser(int userID = -1)
		{
			_manager.Session.Add(SessionUserAuthenticationStatusFieldName, "authenticated");
			_manager.Session.Add(SessionUserIdFieldName, userID);
		}

		/// <summary>
		/// Checking user cookies authentication data and updating authentication status if success
		/// </summary>
		public void AuthenticateUser(int userID, string name, string password)
		{
			var userNameCookie = _manager.Request.Cookies[CookieUserNameFieldName];
			var userPasswordCookie = _manager.Request.Cookies[CookieUserPasswordFieldName];

			if (userNameCookie != null &&
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
				_manager.Request.Cookies.Remove(CookieUserNameFieldName);
				_manager.Request.Cookies.Remove(CookieUserPasswordFieldName);
			}
		}

		/// <summary>
		///Checking user session authentication data and updating authentication status if success
		/// </summary>
		public void AuthenticateSessionUser()
		{
			if (_manager.Session[SessionUserAuthenticationStatusFieldName] == null || (string)_manager.Session[SessionUserAuthenticationStatusFieldName] != "authenticated")
				return;

			IsAuthenticatedAsUser = true;
			_authenticatedUserID = (int)_manager.Session[SessionUserIdFieldName];
			AuthenticatedUserName = "";
		}

		/// <summary>
		/// Remove user authentication data cookies
		/// </summary>
		public void LogOutUser()
		{
			var myCookie = new HttpCookie(CookieUserNameFieldName)
							{
								Expires = DateTime.Now.AddDays(-1d)
							};

			_manager.Response.Cookies.Add(myCookie);

			myCookie = new HttpCookie(CookieUserPasswordFieldName)
						{
							Expires = DateTime.Now.AddDays(-1d)
						};

			_manager.Response.Cookies.Add(myCookie);

			IsAuthenticatedAsUser = false;
			_authenticatedUserID = -1;
			AuthenticatedUserName = "";
		}

		/// <summary>
		/// Remove user session authentication data
		/// </summary>
		public void LogOutSessionUser()
		{
			_manager.Session.Remove(SessionUserAuthenticationStatusFieldName);
			_manager.Session.Remove(SessionUserIdFieldName);
		}
	}
}