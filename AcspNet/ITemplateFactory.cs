using ApplicationHelper;
using ApplicationHelper.Templates;

namespace AcspNet
{
	public interface ITemplateFactory : IHideObjectMembers
	{
		/// <summary>
		/// Load template from a file
		/// </summary>
		/// <param name="fileName">Template file name</param>
		/// <returns>Template class with loaded template</returns>
		Template Load(string fileName);
	}
}