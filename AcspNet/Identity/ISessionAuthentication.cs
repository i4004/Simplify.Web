namespace AcspNet.Identity
{
	/// <summary>
	/// Represent session authentication handler
	/// </summary>
	public interface ISessionAuthentication : IHideObjectMembers
	{
		/// <summary>
		/// Create user authentication variable in user's session (login user via session)
		/// </summary>
		void LogInSessionUser(int userID = -1);

		/// <summary>
		/// Remove user session authentication data
		/// </summary>
		void LogOutSessionUser();

		/// <summary>
		/// Checking user session authentication data and updating authentication status if success
		/// </summary>
		void AuthenticateSessionUser(); 
	}
}