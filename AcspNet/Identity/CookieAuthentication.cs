using System;
using System.Web;
using Simplify.Core;

namespace AcspNet.Identity
{
	/// <summary>
	/// Cookie authentication handler
	/// </summary>
	public class CookieAuthentication : ICookieAuthentication
	{
		internal const string CookieUserNameFieldName = "AcspUserName";
		internal const string CookieUserPasswordFieldName = "AcspUserPassword";

		private readonly HttpCookieCollection _requestCookies;
		private readonly HttpCookieCollection _responseCookies;
		private readonly IAuthenticationState _state;

		/// <summary>
		/// Initializes a new instance of the <see cref="CookieAuthentication"/> class.
		/// </summary>
		/// <param name="requestCookies">The request cookies.</param>
		/// <param name="responseCookies">The response cookies.</param>
		/// <param name="state">The state.</param>
		internal CookieAuthentication(HttpCookieCollection requestCookies, HttpCookieCollection responseCookies, IAuthenticationState state)
		{
			_requestCookies = requestCookies;
			_responseCookies = responseCookies;
			_state = state;
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
				var cookie = _requestCookies[CookieUserNameFieldName];

				return cookie != null ? cookie.Value : null;
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
				var cookie = _requestCookies[CookieUserPasswordFieldName];

				return cookie != null ? cookie.Value : null;
			}
		}		

		/// <summary>
		/// Create user authentication cookies (login user via cookies)
		/// </summary>
		public void LogInCookieUser(string name, string password, bool autoLogin = false)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			if (string.IsNullOrEmpty(password))
				throw new ArgumentNullException("password");

			var cookie = new HttpCookie(CookieUserNameFieldName, name);

			if (autoLogin)
				cookie.Expires = TimeProvider.Current.Now.AddDays(256);

			_responseCookies.Add(cookie);

			cookie = new HttpCookie(CookieUserPasswordFieldName, password);

			if (autoLogin)
				cookie.Expires = TimeProvider.Current.Now.AddDays(256);

			_responseCookies.Add(cookie);
		}

		/// <summary>
		/// Remove user authentication data cookies
		/// </summary>
		public void LogOutCookieUser()
		{
			var myCookie = new HttpCookie(CookieUserNameFieldName)
							{
								// check the correctness of way removing this cookie
								Expires = TimeProvider.Current.Now.AddDays(-1d)
							};

			_responseCookies.Add(myCookie);

			myCookie = new HttpCookie(CookieUserPasswordFieldName)
						{
							Expires = TimeProvider.Current.Now.AddDays(-1d)
						};

			_responseCookies.Add(myCookie);

			_state.Reset();
		}

		/// <summary>
		/// Checking user cookies authentication data and updating authentication status if success
		/// </summary>
		public void AuthenticateCookieUser(int userID, string name, string password)
		{
			if (userID < 0)
				throw new ArgumentException("User ID is invalid");

			if (string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");

			if (string.IsNullOrEmpty(password))
				throw new ArgumentNullException("password");

			var userNameCookie = _requestCookies[CookieUserNameFieldName];
			var userPasswordCookie = _requestCookies[CookieUserPasswordFieldName];

			if (userNameCookie != null &&
			   userPasswordCookie != null &&
			   userNameCookie.Value == name &&
			   userPasswordCookie.Value == password)
			{
				_state.SetAuthenticated(userID, name);
			}
			else
			{
				// check Request cookies should be removed or response cookies?
				// check the correctness of way removing this cookie
				_requestCookies.Remove(CookieUserNameFieldName);
				_requestCookies.Remove(CookieUserPasswordFieldName);
			}
		}
	}
}