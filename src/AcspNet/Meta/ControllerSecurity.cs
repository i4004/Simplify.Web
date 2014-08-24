namespace AcspNet.Meta
{
	/// <summary>
	/// Provides controller security information
	/// </summary>
	public class ControllerSecurity
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerSecurity" /> class.
		/// </summary>
		/// <param name="isAuthenticationRequired">if set to <c>true</c> then indicates whether controller requires user authentication.</param>
		/// <param name="requiredUserRoles">The required user roles.</param>
		public ControllerSecurity(bool isAuthenticationRequired = false, string requiredUserRoles = null)
		{
			IsAuthenticationRequired = isAuthenticationRequired;
			RequiredUserRoles = requiredUserRoles;
		}

		/// <summary>
		/// Gets a value indicating whether controller requires user authentication.
		/// </summary>
		/// <value>
		/// <c>true</c> if controller requires authentication; otherwise, <c>false</c>.
		/// </value>
		public bool IsAuthenticationRequired { get; private set; }

		/// <summary>
		/// Gets the required user roles.
		/// </summary>
		/// <value>
		/// The required user roles.
		/// </value>
		public string RequiredUserRoles { get; private set; }
	}
}