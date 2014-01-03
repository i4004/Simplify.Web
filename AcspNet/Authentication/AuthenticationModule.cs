using System;
using System.ComponentModel;
using System.Web;

namespace AcspNet.Authentication
{
	/// <summary>
	/// Authentication class
	/// </summary>
	public class AuthenticationModule : IAuthenticationModule
	{
		public const string CookieUserNameFieldName = "AcspUserName";
		public const string CookieUserPasswordFieldName = "AcspUserPassword";
		public const string SessionUserAuthenticationStatusFieldName = "AcspAuthenticationStatus";
		public const string SessionUserIdFieldName = "AcspAunthenticatedUserID";

		internal Manager Manager;

		internal AuthenticationModule(Manager manager)
		{
			AuthenticatedUserID = -1;
			Manager = manager;
		}

		/// <summary>
		/// Gets the authenticated user name from cookie.
		/// </summary>
		/// <value>
		/// The authenticated user name from cookie.
		/// </value>
		public string UserNameFromCookie
		{
			get
			{
				var cookie = Manager.Request.Cookies[CookieUserNameFieldName];

				if (cookie != null)
					return cookie.Value ?? "";

				return null;
			}
		}

		/// <summary>
		/// Gets the authenticated user password from cookie.
		/// </summary>
		/// <value>
		/// The authenticated user password from cookie.
		/// </value>
		public string UserPasswordFromCookie
		{
			get
			{
				var cookie = Manager.Request.Cookies[CookieUserPasswordFieldName];

				if (cookie != null)
					return cookie.Value ?? "";

				return null;
			}
		}

		/// <summary>
		/// Gets a value indicating whether current web-site client is authenticated as user.
		/// </summary>
		/// <value>
		/// <c>true</c> if current web-site client is authenticated as user; otherwise, <c>false</c>.
		/// </value>
		public bool IsAuthenticatedAsUser { get; private set; }

		/// <summary>
		/// Gets the authenticated user identifier.
		/// </summary>
		/// <value>
		/// The authenticated user identifier.
		/// </value>
		public int AuthenticatedUserID { get; private set; }

		/// <summary>
		/// Gets the name of the authenticated user.
		/// </summary>
		/// <value>
		/// The name of the authenticated user.
		/// </value>
		public string AuthenticatedUserName { get; private set; }

		/// <summary>
		/// Create user authentication variable in user's session (login user via session)
		/// </summary>
		public void LogInSessionUser(int userID = -1)
		{
			Manager.Session.Add(SessionUserAuthenticationStatusFieldName, "authenticated");
			Manager.Session.Add(SessionUserIdFieldName, userID);
		}

		/// <summary>
		/// Remove user session authentication data
		/// </summary>
		public void LogOutSessionUser()
		{
			Manager.Session.Remove(SessionUserAuthenticationStatusFieldName);
			Manager.Session.Remove(SessionUserIdFieldName);

			IsAuthenticatedAsUser = false;
			AuthenticatedUserID = -1;
			AuthenticatedUserName = null;
		}

		/// <summary>
		/// Create user authentication cookies (login user via cookies)
		/// </summary>
		public void LogInCookieUser(string name, string password, bool autoLogin = false)
		{
			if(string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			if (string.IsNullOrEmpty(password))
				throw new ArgumentNullException("password");

			var cookie = new HttpCookie(CookieUserNameFieldName, name);

			if (autoLogin)
				cookie.Expires = DateTime.Now.AddDays(256);

			Manager.Response.Cookies.Add(cookie);

			cookie = new HttpCookie(CookieUserPasswordFieldName, password);

			if (autoLogin)
				cookie.Expires = DateTime.Now.AddDays(256);

			Manager.Response.Cookies.Add(cookie);
		}

		/// <summary>
		/// Remove user authentication data cookies
		/// </summary>
		public void LogOutCookieUser()
		{
			var myCookie = new HttpCookie(CookieUserNameFieldName)
							{
								Expires = DateTime.Now.AddDays(-1d)
							};

			Manager.Response.Cookies.Add(myCookie);

			myCookie = new HttpCookie(CookieUserPasswordFieldName)
						{
							Expires = DateTime.Now.AddDays(-1d)
						};

			Manager.Response.Cookies.Add(myCookie);

			IsAuthenticatedAsUser = false;
			AuthenticatedUserID = -1;
			AuthenticatedUserName = null;
		}
		
		/// <summary>
		/// Checking user session authentication data and updating authentication status if success
		/// </summary>
		public void AuthenticateSessionUser()
		{
			if (Manager.Session[SessionUserAuthenticationStatusFieldName] == null || (string)Manager.Session[SessionUserAuthenticationStatusFieldName] != "authenticated")
				return;

			IsAuthenticatedAsUser = true;
			AuthenticatedUserID = (int)Manager.Session[SessionUserIdFieldName];
			AuthenticatedUserName = "";
		}

		/// <summary>
		/// Checking user cookies authentication data and updating authentication status if success
		/// </summary>
		public void AuthenticateCookieUser(int userID, string name, string password)
		{
			if (userID <= 0)
				throw new InvalidEnumArgumentException("userID");

			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			if (string.IsNullOrEmpty(password))
				throw new ArgumentNullException("password");

			var userNameCookie = Manager.Request.Cookies[CookieUserNameFieldName];
			var userPasswordCookie = Manager.Request.Cookies[CookieUserPasswordFieldName];

			if (userNameCookie != null &&
			   userPasswordCookie != null &&
			   userNameCookie.Value == name &&
			   userPasswordCookie.Value == password)
			{
				IsAuthenticatedAsUser = true;
				AuthenticatedUserID = userID;
				AuthenticatedUserName = name;
			}
			else
			{
				Manager.Request.Cookies.Remove(CookieUserNameFieldName);
				Manager.Request.Cookies.Remove(CookieUserPasswordFieldName);
			}
		}
	}
}