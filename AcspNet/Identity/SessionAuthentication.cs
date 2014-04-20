using System.Web;

namespace AcspNet.Identity
{
	/// <summary>
	/// Session authenticatin handler
	/// </summary>
	public class SessionAuthentication : ISessionAuthentication
	{
		internal const string SessionUserAuthenticationStatusFieldName = "AcspAuthenticationStatus";
		internal const string SessionUserIdFieldName = "AcspAunthenticatedUserID";
		
		private readonly HttpSessionStateBase _session;
		private readonly IAuthenticationState _state;

		internal SessionAuthentication(HttpSessionStateBase session, IAuthenticationState state)
		{
			_session = session;
			_state = state;
		}

		/// <summary>
		/// Create user authentication variable in user's session (login user via session)
		/// </summary>
		public void LogInSessionUser(int userID = -1)
		{
			_session.Add(SessionUserAuthenticationStatusFieldName, "authenticated");
			_session.Add(SessionUserIdFieldName, userID);
		}

		/// <summary>
		/// Remove user session authentication data
		/// </summary>
		public void LogOutSessionUser()
		{
			_session.Remove(SessionUserAuthenticationStatusFieldName);
			_session.Remove(SessionUserIdFieldName);

			_state.Reset();
		}

		/// <summary>
		/// Checking user session authentication data and updating authentication status if success
		/// </summary>
		public void AuthenticateSessionUser()
		{
			if (_session[SessionUserAuthenticationStatusFieldName] == null || (string)_session[SessionUserAuthenticationStatusFieldName] != "authenticated")
					return;

			_state.SetAuthenticated((int)_session[SessionUserIdFieldName]);
		}
	}
}