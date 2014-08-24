using System;
using System.Collections.Generic;

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
		/// <param name="isAuthorizationRequired">if set to <c>true</c> then indicates whether controller requires user authorization.</param>
		/// <param name="requiredUserRoles">The required user roles.</param>
		public ControllerSecurity(bool isAuthorizationRequired = false, string requiredUserRoles = null)
		{
			IsAuthorizationRequired = isAuthorizationRequired;

			if(!string.IsNullOrEmpty(requiredUserRoles))
				RequiredUserRoles = requiredUserRoles.Replace(" ", "").Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
		}

		/// <summary>
		/// Gets a value indicating whether controller requires user authorization.
		/// </summary>
		/// <value>
		/// <c>true</c> if controller requires authorization; otherwise, <c>false</c>.
		/// </value>
		public bool IsAuthorizationRequired { get; private set; }

		/// <summary>
		/// Gets the required user roles.
		/// </summary>
		/// <value>
		/// The required user roles.
		/// </value>
		public IEnumerable<string> RequiredUserRoles { get; private set; }
	}
}