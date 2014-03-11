namespace AcspNet.Authentication
{
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

		void SetAuthenticated(int userID, string userName = null);
		void Reset();
	}
}