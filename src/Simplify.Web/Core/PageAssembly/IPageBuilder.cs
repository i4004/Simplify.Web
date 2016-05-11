using Simplify.DI;

namespace Simplify.Web.Core.PageAssembly
{
	/// <summary>
	/// Represent web-page builder
	/// </summary>
	public interface IPageBuilder
	{
		/// <summary>
		/// Buids a web page
		/// </summary>
		/// <param name="containerProvider">The DI container provider.</param>
		/// <returns></returns>
		string Build(IDIContainerProvider containerProvider);
	}
}