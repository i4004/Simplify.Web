using ApplicationHelper;

namespace AcspNet.Authentication
{
	/// <summary>
	/// Interface that is used to control users login/logout/autnenticate via cookie or session and stores current user name/password/id unformation
	/// </summary>
	public interface IAuthenticationModule : IHideObjectMembers
	{
		/// <summary>
		/// Gets the authenticated user name from cookie.
		/// </summary>
		/// <value>
		/// The authenticated user name from cookie.
		/// </value>
		string UserNameFromCookie { get; }

		/// <summary>
		/// Gets the authenticated user password from cookie.
		/// </summary>
		/// <value>
		/// The authenticated user password from cookie.
		/// </value>
		string UserPasswordFromCookie { get; }

		/// <summary>
		/// Gets a value indicating whether current web-site client is authenticated as user.
		/// </summary>
		/// <value>
		/// <c>true</c> if current web-site client is authenticated as user; otherwise, <c>false</c>.
		/// </value>
		bool IsAuthenticatedAsUser { get; }

		/// <summary>
		/// Gets the authenticated user identifier.
		/// </summary>
		/// <value>
		/// The authenticated user identifier.
		/// </value>
		int AuthenticatedUserID { get; }

		/// <summary>
		/// Gets the name of the authenticated user.
		/// </summary>
		/// <value>
		/// The name of the authenticated user.
		/// </value>
		string AuthenticatedUserName { get; }

		/// <summary>
		/// Create user authentication variable in user's session (login user via session)
		/// </summary>
		void LogInSessionUser(int userID = -1);

		/// <summary>
		/// Remove user session authentication data
		/// </summary>
		void LogOutSessionUser();

		/// <summary>
		/// Create user authentication cookies (login user via cookies)
		/// </summary>
		void LogInCookieUser(string name, string password, bool autoLogin = false);

		/// <summary>
		/// Remove user authentication data cookies
		/// </summary>
		void LogOutCookieUser();

		/// <summary>
		/// Checking user session authentication data and updating authentication status if success
		/// </summary>
		void AuthenticateSessionUser();

		/// <summary>
		/// Checking user cookies authentication data and updating authentication status if success
		/// </summary>
		void AuthenticateCookieUser(int userID, string name, string password);
	}
}