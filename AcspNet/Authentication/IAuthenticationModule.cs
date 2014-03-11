namespace AcspNet.Authentication
{
	/// <summary>
	/// Represent interface that is used to control users login/logout/autnenticate via cookie or session and stores current user name/password/id unformation
	/// </summary>
	public interface IAuthenticationModule : IAuthenticationState
	{
		//ISessionAuthentication Session { get; }
		//ICookieAuthentication Cookie { get; }
	}
}