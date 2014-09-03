using System.Collections.Generic;

namespace AcspNet.Routing
{
	/// <summary>
	/// Represent parsed controller path
	/// </summary>
	public interface IControllerPath
	{
		/// <summary>
		/// Gets the controller path items.
		/// </summary>
		/// <value>
		/// The controller path items.
		/// </value>
		IList<PathItem> Items { get; }
	}
}