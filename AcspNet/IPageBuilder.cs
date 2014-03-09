using System.Collections.Generic;

namespace AcspNet
{
	/// <summary>
	/// Represent a web page HTML code builder (combiner)
	/// </summary>
	public interface IPageBuilder : IHideObjectMembers
	{
		/// <summary>
		/// Buids a web page
		/// </summary>
		/// <param name="dataItems">The data items which should be inserted into master template file.</param>
		/// <returns></returns>
		string Buid(IDictionary<string, string> dataItems);
	}
}