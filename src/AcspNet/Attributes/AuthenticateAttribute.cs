using System;

namespace AcspNet.Attributes
{
	/// <summary>
	/// Indicates whether controller requires user authentication
	/// </summary>
	public class AuthenticateAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AuthenticateAttribute"/> class.
		/// </summary>
		/// <param name="requiredUserRoles">Required user roles.</param>
		public AuthenticateAttribute(string requiredUserRoles = null)
		{
			RequiredUserRoles = requiredUserRoles;
		}

		/// <summary>
		/// Gets the required user roles.
		/// </summary>
		/// <value>
		/// The required user roles.
		/// </value>
		public string RequiredUserRoles { get; private set; }
	}
}