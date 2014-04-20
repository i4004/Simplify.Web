using System;
using System.Web;

namespace AcspNet.Identity
{
	/// <summary>
	/// Implementation of IAuthenticationModule interface that is used to control users login/logout/autnenticate via cookie or session and stores current user name/password/id unformation
	/// </summary>
	public class Authentication : IAuthentication
	{
		internal Authentication(HttpSessionStateBase session, HttpCookieCollection requestCookies, HttpCookieCollection responseCookies)
		{
			AuthenticatedUserID = -1;

			Session = new SessionAuthentication(session, this);
			Cookie = new CookieAuthentication(requestCookies, responseCookies, this);
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
		/// Gets the session authentication handler.
		/// </summary>
		/// <value>
		/// The sessionauthentication handler.
		/// </value>
		public ISessionAuthentication Session { get; private set; }

		/// <summary>
		/// Gets the cookie authentication handler.
		/// </summary>
		/// <value>
		/// The cookie authentication handler.
		/// </value>
		public ICookieAuthentication Cookie { get; private set; }

		/// <summary>
		/// Sets current user as authenticated.
		/// </summary>
		/// <param name="userID">The user identifier.</param>
		/// <param name="userName">Name of the user.</param>
		/// <exception cref="System.ArgumentException">User ID is invalid</exception>
		public void SetAuthenticated(int userID, string userName = null)
		{
			if (userID < 0)
				throw new ArgumentException("User ID is invalid");

			AuthenticatedUserID = userID;
			AuthenticatedUserName = userName;
			IsAuthenticatedAsUser = true;
		}

		/// <summary>
		/// Resets user authentication state to anonymous.
		/// </summary>
		public void Reset()
		{
			IsAuthenticatedAsUser = false;
			AuthenticatedUserID = -1;
			AuthenticatedUserName = null;
		}
	}
}