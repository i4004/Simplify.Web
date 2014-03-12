using System;
using System.Web;

namespace AcspNet.Authentication
{
	/// <summary>
	/// Implementation of IAuthenticationModule interface that is used to control users login/logout/autnenticate via cookie or session and stores current user name/password/id unformation
	/// </summary>
	public class AuthenticationModule : IAuthenticationModule
	{
		internal AuthenticationModule(HttpSessionStateBase session, HttpCookieCollection requestCookies, HttpCookieCollection responseCookies)
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

		public ISessionAuthentication Session { get; private set; }
		public ICookieAuthentication Cookie { get; private set; }

		public void SetAuthenticated(int userID, string userName = null)
		{
			if (userID < 0)
				throw new ArgumentException("User ID is invalid");

			AuthenticatedUserID = userID;
			AuthenticatedUserName = userName;
			IsAuthenticatedAsUser = true;
		}

		public void Reset()
		{
			IsAuthenticatedAsUser = false;
			AuthenticatedUserID = -1;
			AuthenticatedUserName = null;
		}
	}
}