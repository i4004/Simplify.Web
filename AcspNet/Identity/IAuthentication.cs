namespace AcspNet.Identity
{
	/// <summary>
	/// Represent interface that is used to control users login/logout/autnenticate via cookie or session and stores current user name/password/id unformation
	/// </summary>
	public interface IAuthentication : IAuthenticationState
	{
		/// <summary>
		/// Gets the session authentication handler.
		/// </summary>
		/// <value>
		/// The sessionauthentication handler.
		/// </value>
		ISessionAuthentication Session { get; }

		/// <summary>
		/// Gets the cookie authentication handler.
		/// </summary>
		/// <value>
		/// The cookie authentication handler.
		/// </value>
		ICookieAuthentication Cookie { get; }
	}
}