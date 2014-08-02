using Simplify.Templates;

namespace AcspNet.Modules
{
	/// <summary>
	/// Represent web-site text templates loader
	/// </summary>
	public interface ITemplateFactory : IHideObjectMembers
	{
		/// <summary>
		/// Load template from a file
		/// </summary>
		/// <param name="fileName">Template file name</param>
		/// <returns>Template class with loaded template</returns>
		ITemplate Load(string fileName);
	}
}