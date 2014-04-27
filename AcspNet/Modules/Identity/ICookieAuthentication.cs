namespace AcspNet.Modules.Identity
{
	/// <summary>
	/// Represent cookie authentication handler
	/// </summary>
	public interface ICookieAuthentication : IHideObjectMembers
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
		/// Remove user authentication data cookies
		/// </summary>
		void LogOutCookieUser();

		/// <summary>
		/// Create user authentication cookies (login user via cookies)
		/// </summary>
		void LogInCookieUser(string name, string password, bool autoLogin = false);

		/// <summary>
		/// Checking user cookies authentication data and updating authentication status if success
		/// </summary>
		void AuthenticateCookieUser(int userID, string name, string password);		 
	}
}