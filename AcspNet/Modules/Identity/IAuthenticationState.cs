namespace AcspNet.Modules.Identity
{
	/// <summary>
	/// Represent authentication state handler
	/// </summary>
	public interface IAuthenticationState : IHideObjectMembers
	{
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
		/// Sets current user as authenticated.
		/// </summary>
		/// <param name="userID">The user identifier.</param>
		/// <param name="userName">Name of the user.</param>
		void SetAuthenticated(int userID, string userName = null);

		/// <summary>
		/// Resets user authentication state to anonymous.
		/// </summary>
		void Reset();
	}
}