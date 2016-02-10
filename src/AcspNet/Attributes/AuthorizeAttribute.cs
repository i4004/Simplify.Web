using System;

namespace Simplify.Web.Attributes
{
	/// <summary>
	/// Indicates whether controller requires user authorization
	/// </summary>
	public class AuthorizeAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AuthorizeAttribute"/> class.
		/// </summary>
		/// <param name="allowedUserRoles">Allowed user roles.</param>
		public AuthorizeAttribute(string allowedUserRoles = null)
		{
			AllowedUserRoles = allowedUserRoles;
		}

		/// <summary>
		/// Gets the allowed user roles.
		/// </summary>
		/// <value>
		/// The allowed user roles.
		/// </value>
		public string AllowedUserRoles { get; private set; }
	}
}