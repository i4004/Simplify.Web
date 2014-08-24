using System;

namespace AcspNet.Attributes
{
	/// <summary>
	/// Indicates whether controller requires user authorization
	/// </summary>
	public class AuthorizeAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AuthorizeAttribute"/> class.
		/// </summary>
		/// <param name="requiredUserRoles">Required user roles.</param>
		public AuthorizeAttribute(string requiredUserRoles = null)
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