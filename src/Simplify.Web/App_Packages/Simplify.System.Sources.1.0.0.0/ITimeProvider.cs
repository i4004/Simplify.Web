using System;

namespace Simplify.Web.App_Packages.Simplify.System.Sources._1._0._0._0
{
	/// <summary>
	/// Represent time provider
	/// </summary>
	internal interface ITimeProvider : IHideObjectMembers
	{
		/// <summary>
		/// Gets the current UTC time.
		/// </summary>
		/// <value>
		/// The current UTC time.
		/// </value>
		DateTime UtcNow { get; }

		/// <summary>
		/// Gets the current time.
		/// </summary>
		/// <value>
		/// The current time.
		/// </value>
		DateTime Now { get; }

		/// <summary>
		/// Gets the today date without time.
		/// </summary>
		/// <value>
		/// The today date without time.
		/// </value>
		DateTime Today { get; }
	}
}
