using System;

namespace AcspNet.Authentication
{
	/// <summary>
	/// Implementation of IAuthenticationModule interface that is used to control users login/logout/autnenticate via cookie or session and stores current user name/password/id unformation
	/// </summary>
	public class AuthenticationModule : IAuthenticationModule
	{		internal AuthenticationModule()
		{
			AuthenticatedUserID = -1;
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

		public void SetAuthenticated(int userID, string userName = null)
		{
			throw new NotImplementedException();
		}

		public void Reset()
		{
			//	IsAuthenticatedAsUser = false;
			//	AuthenticatedUserID = -1;
			//	AuthenticatedUserName = null;

			throw new NotImplementedException();
		}
	}
}