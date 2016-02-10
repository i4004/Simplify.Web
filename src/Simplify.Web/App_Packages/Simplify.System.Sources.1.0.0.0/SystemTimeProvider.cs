using System;

namespace Simplify.Web.App_Packages.Simplify.System.Sources._1._0._0._0
{
	/// <summary>
	/// System time provider, returns the actual DateTime.Now, DateTime.UtcNow, DateTime.Today data
	/// </summary>
	internal sealed class SystemTimeProvider : ITimeProvider
	{
		/// <summary>
		/// Gets the current UTC time.
		/// </summary>
		/// <value>
		/// The current UTC time.
		/// </value>
		public DateTime UtcNow
		{
			get { return DateTime.UtcNow; }
		}

		/// <summary>
		/// Gets the current time.
		/// </summary>
		/// <value>
		/// The current time.
		/// </value>
		public DateTime Now
		{
			get { return DateTime.Now; }
		}

		/// <summary>
		/// Gets the today date without time.
		/// </summary>
		/// <value>
		/// The today date without time.
		/// </value>
		public DateTime Today
		{
			get { return DateTime.Today; }
		}
	}
}
